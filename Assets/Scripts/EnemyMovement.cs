using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    private float originalX;
    private float maxOffset = 5.0f;
    private float enemyPatroltime = 2.0f;
    private int moveRight = -1;
    private Vector2 velocity;

    private Rigidbody2D enemyBody;
    public bool restartTry = false;
    public bool restartTry2 = false;

    public Vector3 startPosition = new Vector3(0.0f, 0.0f, 0.0f);
    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        // get the starting position
        originalX = transform.position.x;
        ComputeVelocity();
    }
    void ComputeVelocity()
    {
        velocity = new Vector2((moveRight) * maxOffset / enemyPatroltime, 0);
    }
    void Movegoomba()
    {
        if (enemyBody.bodyType == RigidbodyType2D.Kinematic) enemyBody.velocity = Vector2.zero;
        enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);
    }

    void Update()
    {
        if (Mathf.Abs(enemyBody.position.x - originalX) < maxOffset)
        {// move goomba
            if (enemyBody.bodyType == RigidbodyType2D.Kinematic) Movegoomba();
        }
        else
        {
            // change direction
            moveRight *= -1;
            ComputeVelocity();
            if (enemyBody.bodyType == RigidbodyType2D.Kinematic) Movegoomba();
        }
        if (restartTry2)
        {
            enemyBody.velocity = Vector2.zero;
            enemyBody.rotation = 0;
            transform.localPosition = startPosition;
            restartTry = false;
            restartTry2 = false;
            enemyBody.bodyType = RigidbodyType2D.Kinematic;
            Debug.Log("try2");
            Debug.Log(Time.frameCount);
        }
        if (restartTry)
        {
            enemyBody.velocity = Vector2.zero;
            transform.localPosition = startPosition;
            restartTry = false;
            restartTry2 = true;
            Debug.Log("try1");
            Debug.Log(Time.frameCount);
            enemyBody.bodyType = RigidbodyType2D.Static;
        }
    }
    void FixedUpdate()
    {
        if (enemyBody.bodyType == RigidbodyType2D.Kinematic) enemyBody.velocity = Vector2.zero;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name);
    }
}