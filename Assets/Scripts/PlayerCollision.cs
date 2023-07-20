using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Start"))
        {
            GameStatus.start = true;
            collision.collider.gameObject.SetActive(false);
        }
        else if (collision.collider.CompareTag("End"))
        {
            GameStatus.win = true;
            collision.collider.gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("OpenLight"))
        {

        }
        else if (other.CompareTag("ChangeCamera"))
        {

        }
        if (other.CompareTag("ShowMap"))
        {

        }
        if (other.CompareTag("ShowPath"))
        {

        }
    }
}
