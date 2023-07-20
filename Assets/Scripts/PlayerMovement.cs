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

    // object components
    private Rigidbody PlayerRigidbody;
    private Transform PlayerTransform;

    // Start is called before the first frame update
    void Start()
    {
        PlayerRigidbody = GetComponent<Rigidbody>();
        PlayerTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // get keyboard inputs
        wsInput = Input.GetAxis("Vertical");
        adInput = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
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
        PlayerRigidbody.velocity = heading * inputScale * moveScale;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Start"))
        {
            Debug.Log("Start");
            Destroy(collision.collider.gameObject);
        }
        else if (collision.collider.CompareTag("End"))
        {
            Debug.Log("End");
            Destroy(collision.collider.gameObject);
        }
    }
}
