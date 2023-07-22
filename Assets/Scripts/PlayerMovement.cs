using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // user inputs
    private float wsInput;
    private float adInput;
    private float inputScale;

    // player movement
    private float moveScale = 20.0f;
    private Vector3 heading;
    private Vector3 movement;

    // object components
    private Transform PlayerTransform;
    private Rigidbody PlayerRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        PlayerTransform = GetComponent<Transform>();
        PlayerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // get keyboard inputs
        wsInput = Input.GetAxis("Vertical");
        adInput = Input.GetAxis("Horizontal");
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if (CheatConsole.changeCamera == false)
        {
            // move 90 degrees right (press only "D" or "D" + "W" + "S")
            if ((Input.GetKey("d") && !Input.GetKey("w") && !Input.GetKey("s")) || (Input.GetKey("d") && Input.GetKey("w") && Input.GetKey("s")))
            {
                heading = new Vector3(0, 0, -1);
                inputScale = Mathf.Abs(adInput);
            }
            // move 90 degrees left (press only "A" or "A" + "W" + "S")
            else if ((Input.GetKey("a") && !Input.GetKey("w") && !Input.GetKey("s")) || (Input.GetKey("a") && Input.GetKey("w") && Input.GetKey("s")))
            {
                heading = new Vector3(0, 0, 1);
                inputScale = Mathf.Abs(adInput);
            }
            // move 0 degree forward (press only "W" or "W" + "A" + "D")
            else if ((Input.GetKey("w") && !Input.GetKey("a") && !Input.GetKey("d")) || (Input.GetKey("w") && Input.GetKey("a") && Input.GetKey("d")))
            {
                heading = new Vector3(1, 0, 0);
                inputScale = Mathf.Abs(wsInput);
            }
            // move 180 degrees backward (press only "S" or "S" + "A" + "D")
            else if ((Input.GetKey("s") && !Input.GetKey("a") && !Input.GetKey("d")) || (Input.GetKey("s") && Input.GetKey("a") && Input.GetKey("d")))
            {
                heading = new Vector3(-1, 0, 0);
                inputScale = Mathf.Abs(wsInput);
            }
            // move 45 degrees right (press "W" + "D")
            else if (Input.GetKey("w") && Input.GetKey("d"))
            {
                heading = new Vector3(1, 0, -1);
                heading = heading.normalized;
                inputScale = (Mathf.Abs(wsInput) + Mathf.Abs(adInput)) / 2.0f;
            }
            // move 45 degrees left (press "W" + "A")
            else if (Input.GetKey("w") && Input.GetKey("a"))
            {
                heading = new Vector3(1, 0, 1);
                heading = heading.normalized;
                inputScale = (Mathf.Abs(wsInput) + Mathf.Abs(adInput)) / 2.0f;
            }
            // move 135 degrees right (press "S" + "D")
            else if (Input.GetKey("s") && Input.GetKey("d"))
            {
                heading = new Vector3(-1, 0, -1);
                heading = heading.normalized;
                inputScale = (Mathf.Abs(wsInput) + Mathf.Abs(adInput)) / 2.0f;
            }
            // move 135 degrees left (press "S" + "A")
            else if (Input.GetKey("s") && Input.GetKey("a"))
            {
                heading = new Vector3(-1, 0, 1);
                heading = heading.normalized;
                inputScale = (Mathf.Abs(wsInput) + Mathf.Abs(adInput)) / 2.0f;
            }
            // stand still
            else
            {
                heading = new Vector3(0, 0, 0);
                inputScale = 0;
            }

            // let the character go forward
            movement = heading * inputScale * moveScale;
            PlayerRigidbody.velocity = new Vector3(movement[0], PlayerRigidbody.velocity[1], movement[2]);
        }
        else
        {

        }
    }
}
