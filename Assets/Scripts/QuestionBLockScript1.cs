using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBLockScript1 : MonoBehaviour
{
    public Animator coinAnimator;
    public Rigidbody2D questionBlockRB2D;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        coinAnimator.SetFloat("speed", questionBlockRB2D.velocity.y);
    }
}
