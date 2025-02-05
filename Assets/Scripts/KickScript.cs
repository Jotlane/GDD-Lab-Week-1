using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//TODO add the trigger thing for marioscript to check as a bool then handle the rest of the logic on mario because yes
public class KickScript : MonoBehaviour
{
    // Start is called before the first frame update
    public bool canKick;
    public GameObject kickTarget;
    void Start()
    {
        canKick = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (kickTarget)
        {
            canKick = true;
        }
        else
        {
            canKick = false;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            kickTarget = other.gameObject;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            kickTarget = null;
        }
    }
}
