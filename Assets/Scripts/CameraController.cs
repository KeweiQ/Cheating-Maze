using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // player object's transform'
    public Transform PlayerTransform;
    public Transform CameraTransform;

    // smooth factor of moving camera and following player
    [Range(0.01f, 1.0f)]
    public float smoothFactor = 0.5f;

    // camera zoom related
    private RaycastHit rayHit;
    public float zoomSpeed = 1.0f;
    public float defaultDistance;
    public float minDistance = 1.0f;

    // smooth zooming parameters
    //public float lerpDuration = 0.5f;
    //private float lerpTimer = 0f;
    //private bool zoomProgress = false;

    // Start is called before the first frame update
    void Start()
    {
        // get objects
        CameraTransform = GetComponent<Transform>();

        // calculate default distance from camera to player
        defaultDistance = Vector3.Distance(PlayerTransform.position, CameraTransform.position);

        // change player transform to focus point child element (if change directly in unity there will be bugs)
        // PlayerTransform = PlayerTransform.Find("Focus");
    }

    // Update is called once per frame
    void Update()
    {
        // THE FOLLOWING CODE ARE THIRD-PERSON VIEW RELATED AND CURRENTLY NOT USED IN THIS GAME
        // let camera follow player
        //Vector3 newPosition = PlayerTransform.position + this.cameraOffset;
        //CameraTransform.position = Vector3.Slerp(CameraTransform.position, newPosition, smoothFactor);

        // let camera focus on character
        //CameraTransform.LookAt(PlayerTransform.position);

        // zoom camera if obstructed
        // ViewObstructed();
    }

    public static void RotateCamera(GameObject Camera, float horizontalInput, float rotationSpeed)
    {
        float rotationAmount = horizontalInput * rotationSpeed;
        Vector3 rotation = Camera.transform.rotation.eulerAngles + new Vector3(0f, rotationAmount, 0f);
        Camera.transform.rotation = Quaternion.Euler(rotation);
    }

    public void ViewObstructed()
    {
        RaycastHit hit;

        // some obstruction between player and camera
        if (Physics.Raycast(PlayerTransform.position, CameraTransform.position - PlayerTransform.position, out hit, defaultDistance + 0.1f))
        {
            rayHit = hit;

            if (hit.collider.gameObject.tag == "Map")
            {
                // set obstructing object to transparent (need further debug)
                // hit.collider.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;

                if (Vector3.Distance(CameraTransform.position, PlayerTransform.position) > minDistance)
                {
                    // calculate direction and distance of zooming
                    float distance = Vector3.Distance(PlayerTransform.position, CameraTransform.position) - hit.distance;

                    // zoom
                    CameraTransform.position += distance * (PlayerTransform.position - CameraTransform.position).normalized; // CameraTransform.forward.normalized;

                    // smooth camera zooming (need further debugging)
                    //lerpTimer += Time.deltaTime;
                    //float t = Mathf.Clamp01(lerpTimer / lerpDuration);

                    //Vector3 destination = CameraTransform.position + CameraTransform.forward * distance;
                    //if (CameraTransform.position != destination)
                    //{
                    //    CameraTransform.position = Vector3.Lerp(CameraTransform.position, destination, t);
                    //}

                    //if (CameraTransform.position != destination)
                    //{
                    //    zoomProgress = true;
                    //}
                    //else
                    //{
                    //    zoomProgress = false;
                    //}

                    //if (zoomProgress == false)
                    //{
                    //    lerpTimer = 0f;
                    //}
                }
            }
        }
        // no obstruction between player and camera
        else
        {
            // set obstructing object back to normal (need further debug)
            // rayHit.collider.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        }
    }
}
