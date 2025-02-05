using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 50;
    public float maxSpeed = 20;
    public float upSpeed = 40;
    private bool onGroundState = true;
    private Rigidbody2D marioBody;
    private SpriteRenderer marioSprite;
    private bool faceRightState = true;
    public TextMeshProUGUI scoreText;
    public GameObject enemies;
    public JumpOverGoomba jumpOverGoomba;
    public BoxCollider2D marioCollider;
    public Sprite deathSprite;
    public Sprite normalSprite;
    public TextMeshProUGUI scoreDeath;
    public GameObject HUD;
    public GameObject endScreen;
    private bool dead = false;
    public AudioSource deathSound;
    public AudioSource audioKick;
    public GameObject kickCollider;
    public KickScript kickScript;
    public AudioSource bgm;

    public float kickSpeed = 40;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
        marioCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a") && faceRightState)
        {
            faceRightState = false;
            marioSprite.flipX = true;
            kickCollider.GetComponent<BoxCollider2D>().offset = new Vector2(-1, 0);
        }

        if (Input.GetKeyDown("d") && !faceRightState)
        {
            faceRightState = true;
            marioSprite.flipX = false;
            kickCollider.GetComponent<BoxCollider2D>().offset = new Vector2(1, 0);
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground")) onGroundState = true;
    }
    // FixedUpdate is called 50 times a second
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        if (Mathf.Abs(moveHorizontal) > 0)
        {
            Vector2 movement = new Vector2(moveHorizontal, 0);
            // check if it doesn't go beyond maxSpeed
            if (marioBody.velocity.magnitude < maxSpeed)
                marioBody.AddForce(movement * speed);
        }

        // stop
        if (Input.GetKeyUp("a") || Input.GetKeyUp("d"))
        {
            // stop
            marioBody.velocity = Vector2.zero;
        }
        if (Input.GetKeyDown("space") && onGroundState)
        {
            marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            onGroundState = false;

        }
        if (Input.GetKeyDown("k") && kickScript.canKick)
        {
            kickScript.kickTarget.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            if (faceRightState)
            {
                kickScript.kickTarget.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 2) * kickSpeed, ForceMode2D.Impulse);
                GetComponent<Animator>().SetTrigger("New Trigger");
            }
            else
            {
                kickScript.kickTarget.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 2) * kickSpeed, ForceMode2D.Impulse);
                GetComponent<Animator>().SetTrigger("Trigger2");
            }
            kickScript.kickTarget.GetComponent<CircleCollider2D>().isTrigger = false;
            audioKick.Play();
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (!dead)
            {
                Debug.Log("Collided with goomba!");
                marioBody.AddForce(Vector2.up * upSpeed * 1.5f, ForceMode2D.Impulse);
                marioCollider.isTrigger = true;
                marioSprite.sprite = deathSprite;
                dead = true;
                scoreDeath.text = scoreText.text;
                HUD.SetActive(false);
                endScreen.SetActive(true);
                deathSound.Play();
                bgm.Stop();
            }
        }
    }

    public void RestartButtonCallback(int input)
    {
        Debug.Log("Restart!");
        // reset everything
        ResetGame();
        // resume time
        //Time.timeScale = 1.0f;
    }

    private void ResetGame()
    {
        // reset position
        marioBody.transform.position = new Vector3(-5.33f, -4.69f, 0.0f);
        // reset sprite direction
        faceRightState = true;
        marioSprite.flipX = false;
        // reset score
        scoreText.text = "Score: 0";
        // reset Goomba
        foreach (Transform eachChild in enemies.transform)
        {
            eachChild.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            eachChild.transform.localPosition = eachChild.GetComponent<EnemyMovement>().startPosition;
            eachChild.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            eachChild.gameObject.GetComponent<Rigidbody2D>().totalForce = Vector2.zero;
            eachChild.gameObject.GetComponent<Rigidbody2D>().simulated = false;
            eachChild.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            eachChild.gameObject.GetComponent<Rigidbody2D>().simulated = true;
            eachChild.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            eachChild.GetComponent<EnemyMovement>().restartTry = true;
            eachChild.GetComponent<CircleCollider2D>().isTrigger = true;
            Debug.Log("restrys");
            Debug.Log(eachChild.GetComponent<EnemyMovement>().restartTry);

            //eachChild.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        }
        jumpOverGoomba.score = 0;
        marioSprite.sprite = normalSprite;
        marioCollider.isTrigger = false;
        dead = false;
        HUD.SetActive(true);
        endScreen.SetActive(false);
        bgm.Play();
    }
}
