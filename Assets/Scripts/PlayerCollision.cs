using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    // trigger indicator (for cheat console)
    public static string onSwitch = "";

    // Start is called before the first frame update
    void Start()
    {
        onSwitch = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        // start timer when entering the maze
        if (collision.collider.CompareTag("Start"))
        {
            GameStatus.start = true;
            collision.collider.gameObject.SetActive(false);
        }
        // stop timer when reaching the exit
        else if (collision.collider.CompareTag("End"))
        {
            GameStatus.win = true;
            collision.collider.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // identify trigger type
        if (other.CompareTag("OpenLight"))
        {
            onSwitch = "OpenLight";
        }
        else if (other.CompareTag("ChangeCamera"))
        {
            onSwitch = "ChangeCamera";
        }
        if (other.CompareTag("ShowMap"))
        {
            onSwitch = "ShowMap";
        }
        if (other.CompareTag("ShowPath"))
        {
            onSwitch = "ShowPath";
        }
        if (other.CompareTag("ReadHint"))
        {
            onSwitch = "ReadHint";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        onSwitch = "";
    }
}
