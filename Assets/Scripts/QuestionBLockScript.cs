using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBLockScript : MonoBehaviour
{
    public Animator questionBlockAnimator;
    public Animator coinAnimator;
    public Rigidbody2D questionBlockRB2D;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        questionBlockAnimator.SetFloat("speed", questionBlockRB2D.velocity.y);
        coinAnimator.SetFloat("speed", questionBlockRB2D.velocity.y);
    }
}
