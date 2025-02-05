using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ConfettiScript : MonoBehaviour
{
    public GameObject confetti;
    public AudioSource win;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("confetti");
            for (int i = 0; i < 20; i++)
            {
                GameObject confettiInstance = Instantiate(confetti, new Vector2(-12, 1), Quaternion.identity);
                confettiInstance.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1.0f, 6.0f), Random.Range(10f, 30.0f)), ForceMode2D.Impulse);
            }
            win.Play();
        }
    }
}
