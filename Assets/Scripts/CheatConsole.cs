using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class CheatConsole : MonoBehaviour
{
    private Transform PlayerTransform;

    // cheat function indicators
    public static bool openLight = false;
    public static bool changeCamera = false;
    public static bool showMap = false;
    public static bool showPath = false;

    // cheat messages
    public TextMeshProUGUI CheatSwitchMessage;
    public TextMeshProUGUI CheatMessage;

    // open light related
    public GameObject DirectionalLightObject;
    public Light DirectionalLight;
    public Material daySkybox;
    public Material nightSkybox;

    // change camera related
    public Camera TopDownCamera;
    public Camera PlayerViewCamera;

    // show map related
    // public GameObject Map;

    // show path related
    public GameObject Path;

    void Awake()
    {
        // Store the default skybox material before any modifications occur
        daySkybox = UnityEngine.RenderSettings.skybox;
    }

    // Start is called before the first frame update
    void Start()
    {
        // get objects
        PlayerTransform = GetComponent<Transform>();
        DirectionalLight = DirectionalLightObject.GetComponent<Light>();

        // turn off cheat when game start
        TurnOffCheat();
        CheatSwitchMessage.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        MonitorSwitch();
    }

    void MonitorSwitch()
    {
        if (PlayerCollision.onSwitch == "OpenLight")
        {
            CheatSwitchMessage.text = "Cheat mode switch\nPress \"E\" to open the map light";
            CheatSwitchMessage.enabled = true;

            if (Input.GetKeyDown(KeyCode.E))
            {
                OpenLight();
            }
        }
        else if (PlayerCollision.onSwitch == "ChangeCamera")
        {
            CheatSwitchMessage.text = "Cheat mode switch\nPress \"E\" to change the camera to top-down view";
            CheatSwitchMessage.enabled = true;
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                ChangeCamera();
            }
        }
        else if (PlayerCollision.onSwitch == "ShowMap")
        {
            CheatSwitchMessage.text = "Cheat mode switch\nPress \"E\" to show the maze map";
            CheatSwitchMessage.enabled = true;

            if (Input.GetKeyDown(KeyCode.E))
            {
                ShowMap();
            }
        }
        else if (PlayerCollision.onSwitch == "ShowPath")
        {
            CheatSwitchMessage.text = "Cheat mode switch\nPress \"E\" to show the solution path";
            CheatSwitchMessage.enabled = true;

            if (Input.GetKeyDown(KeyCode.E))
            {
                ShowPath();
            }
        }
        else
        {
            CheatSwitchMessage.enabled = false;
        }

        if (CheatMessage.enabled == true)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                TurnOffCheat();
            }
        }
    }

    void OpenLight()
    {
        openLight = true;
        CheatMessage.enabled = true;

        UnityEngine.RenderSettings.skybox = daySkybox;
        DirectionalLight.intensity = 1.0f;
    }

    void ChangeCamera()
    {
        changeCamera = true;
        CheatMessage.enabled = true;

        TopDownCamera.enabled = true;
        PlayerViewCamera.enabled = false;

        Vector3 currentRotation = PlayerTransform.eulerAngles;
        currentRotation.y = -90.0f;
        PlayerTransform.eulerAngles = currentRotation;

        currentRotation = TopDownCamera.transform.eulerAngles;
        currentRotation.y = 90.0f;
        TopDownCamera.transform.eulerAngles = currentRotation;
    }

    void ShowMap()
    {
        showMap = true;
        CheatMessage.enabled = true;
    }

    void ShowPath()
    {
        showPath = true;
        CheatMessage.enabled = true;

        Path.SetActive(true);
    }

    void TurnOffCheat()
    {
        // reset light
        UnityEngine.RenderSettings.skybox = nightSkybox;
        DirectionalLight.intensity = 0.1f;

        // reset camera
        TopDownCamera.enabled = false;
        PlayerViewCamera.enabled = true;
        
        // hide path
        Path.SetActive(false);

        // hide cheat message
        CheatMessage.enabled = false;

        // reset cheat function indicators
        openLight = false;
        changeCamera = false;
        showMap = false;
        showPath = false;
}
}
