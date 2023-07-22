using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // player object's transform'
    public Transform PlayerTransform;
    public Transform CameraTransform;

    // relative position between camera and player
    public Vector3 cameraOffset;

    // smooth factor of moving camera and following player
    [Range(0.01f, 1.0f)]
    public float smoothFactor = 0.5f;

    // camera zoom related
    public Transform Obstruction;
    public float zoomSpeed = 1.0f;
    public float defaultDistance;
    public float minDistance = 1.0f;

    // smooth zoom parameters
    //public float lerpDuration = 0.5f;
    //private float lerpTimer = 0f;
    //private bool zoomProgress = false;

    // Start is called before the first frame update
    void Start()
    {
        CameraTransform = GetComponent<Transform>();

        // record relative position between camera and player
        this.cameraOffset = CameraTransform.position - PlayerTransform.position;
        // set default obstruction
        Obstruction = PlayerTransform;
        // change player transform to focus point child element (if change directly in unity there will be bugs)
        // PlayerTransform = PlayerTransform.Find("Focus");
        // calculate default distance from camera to player
        defaultDistance = Vector3.Distance(PlayerTransform.position, CameraTransform.position);
    }

    // Update is called once per frame
    void Update()
    {
        // let camera follow player
        Vector3 newPosition = PlayerTransform.position + this.cameraOffset;
        CameraTransform.position = Vector3.Slerp(CameraTransform.position, newPosition, smoothFactor);

        // let camera focus on character
        this.transform.LookAt(PlayerTransform.position);

        // zoom camera if obstructed
        viewObstructed();
    }

    public static void rotateCamera(GameObject Camera, float horizontalInput, float rotationSpeed)
    {
        float rotationAmount = horizontalInput * rotationSpeed;
        Vector3 rotation = Camera.transform.rotation.eulerAngles + new Vector3(0f, rotationAmount, 0f);
        Camera.transform.rotation = Quaternion.Euler(rotation);

        //Vector3 rotation = new Vector3(0f, rotationAmount, 0f);
        //Camera.transform.rotation = Quaternion.Lerp(Camera.transform.rotation, Quaternion.Euler(rotation), smoothFactor);
    }

    public void viewObstructed()
    {
        RaycastHit hit;

        // there's some obstruction between player and camera
        if (Physics.Raycast(PlayerTransform.position, CameraTransform.position - PlayerTransform.position, out hit, defaultDistance))
        {
            if (hit.collider.gameObject.tag == "Map")
            {
                Obstruction = hit.transform;
                // set obstructing object to transparent (need to find the right render or this won't work) (may not need at all)
                // Obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;

                if (Vector3.Distance(CameraTransform.position, PlayerTransform.position) > minDistance)
                {
                    // calculate direction and distance of zooming in
                    Vector3 direction = cameraOffset.normalized;
                    float distance = Vector3.Distance(PlayerTransform.position, CameraTransform.position) - hit.distance + 0.2f;

                    // zoom in
                    CameraTransform.position += CameraTransform.forward * distance;

                    // smoot camera zooming (need further debugging)
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
        // there's no obstruction between player and camera
        else
        {
            // set obstructing object back to normal (need to find the right render or this won't work) (may not need at all)
            // if (Obstruction.gameObject.tag != "Player") {
            //     Obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
            // }

            if (Vector3.Distance(CameraTransform.position, PlayerTransform.position) < defaultDistance)
            {
                // calculate direction and distance of zooming out
                Vector3 direction = cameraOffset.normalized;
                float distance = Vector3.Distance(PlayerTransform.position, CameraTransform.position) - hit.distance + 0.2f;

                // zoom out
                CameraTransform.position -= CameraTransform.forward * distance;

                // smoot camera zooming (need further debugging)
                //lerpTimer += Time.deltaTime;
                //float t = Mathf.Clamp01(lerpTimer / lerpDuration);
                //CameraTransform.position = Vector3.Lerp(CameraTransform.position, CameraTransform.position - CameraTransform.forward * distance, t);
            }
        }
    }
}
