using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JumpOverGoomba : MonoBehaviour
{
    public Transform enemyLocation;
    public TextMeshProUGUI scoreText;
    private bool onGroundState;

    [System.NonSerialized]
    public int score = 0; // we don't want this to show up in the inspector

    private bool countScoreState = false;
    public Vector3 boxSize;
    public float maxDistance;
    public Vector3 kickboxSize;
    public float kickmaxDistance;
    public LayerMask layerMask;
    public LayerMask kicklayerMask;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        kickCheck();

    }

    void FixedUpdate()
    {
        // mario jumps
        if (Input.GetKeyDown("space") && onGroundCheck())
        {
            onGroundState = false;
            countScoreState = true;
            Debug.Log("oooooooo");
        }

        // when jumping, and Goomba is near Mario and we haven't registered our score
        if (!onGroundState && countScoreState)
        {
            if (Mathf.Abs(transform.position.x - enemyLocation.position.x) < 0.5f)
            {
                countScoreState = false;
                score++;
                scoreText.text = "Score: " + score.ToString();
                Debug.Log(score);
            }
            Debug.Log("hkasjhda");
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground")) onGroundState = true;
    }


    private bool onGroundCheck()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, maxDistance, layerMask))
        {
            Debug.Log("on ground");
            return true;
        }
        else
        {
            Debug.Log("not on ground");
            return false;
        }
    }
    private bool kickCheck()
    {
        if (Physics2D.BoxCast(transform.position, kickboxSize, 0, transform.right, kickmaxDistance, kicklayerMask))
        {
            Debug.Log(Physics2D.BoxCast(transform.position, kickboxSize, 0, transform.right, kickmaxDistance, kicklayerMask));
            return true;
        }
        else
        {
            Debug.Log("cannotkick");
            return false;
        }
    }
    // helper
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(transform.position - transform.up * maxDistance, boxSize);
        Gizmos.DrawCube(transform.position + transform.right * kickmaxDistance, kickboxSize);
    }

}
