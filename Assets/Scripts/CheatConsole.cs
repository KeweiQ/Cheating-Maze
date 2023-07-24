using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class CheatConsole : MonoBehaviour
{
    private Transform PlayerTransform;
    private int cameraIndicator = 1;

    // cheat function indicators
    public static bool showHint = false;
    public static bool openLight = false;
    public static bool changeCamera = false;
    public static bool showMap = false;
    public static bool showPath = false;

    // cheat messages
    public TextMeshProUGUI CheatSwitchMessage;
    public TextMeshProUGUI CheatMessage;
    public TextMeshProUGUI CloseMessage;
    public GameObject HintImage;

    // open light related
    public GameObject DirectionalLightObject;
    public GameObject Flashlight;
    public Light DirectionalLight;
    public Material daySkybox;
    public Material nightSkybox;

    // change camera related
    public Camera TopDownCamera;
    public Camera PlayerViewCamera;

    // show map related
    public GameObject MapImage;

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
        CloseMessage.enabled = false;
        HintImage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        MonitorClose();
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
            if (PlayerViewCamera.enabled == true)
            {
                CheatSwitchMessage.enabled = true;
            }
            else
            {
                CheatSwitchMessage.enabled = false;
            }
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (cameraIndicator == 1)
                {
                    ChangeCamera();
                }
            }

            if (cameraIndicator == 2 && changeCamera == false)
            {
                cameraIndicator = 1;
            }
        }
        else if (PlayerCollision.onSwitch == "ShowMap")
        {
            CheatSwitchMessage.text = "Cheat mode switch\nPress \"E\" to show the maze map";
            CheatSwitchMessage.enabled = true;
            if (MapImage.activeSelf == true)
            {
                CheatSwitchMessage.enabled = false;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (Time.timeScale != 0.0f)
                {
                    ShowMap();
                }
            }

            if (showMap == false)
            {
                Time.timeScale = 1.0f;
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
        else if (PlayerCollision.onSwitch == "ReadHint")
        {
            CheatSwitchMessage.text = "Press \"E\" to read";
            CheatSwitchMessage.enabled = true;
            if (HintImage.activeSelf == true)
            {
                CheatSwitchMessage.enabled = false;
            }

                if (Input.GetKeyDown(KeyCode.E))
            {
                if (Time.timeScale != 0.0f)
                {
                    ReadHint();
                }
            }

            if (HintImage.activeSelf == false)
            {
                Time.timeScale = 1.0f;
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

    void MonitorClose()
    {
        if (HintImage.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                CloseMessage.enabled = false;
                HintImage.SetActive(false);
            }
        }
        if (MapImage.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                CloseMessage.enabled = false;
                MapImage.SetActive(false);
                showMap = false;
            }
        }
        if (changeCamera == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                CloseMessage.enabled = false;
                TopDownCamera.enabled = false;
                PlayerViewCamera.enabled = true;
                changeCamera = false;
            }
        }
    }

    void ReadHint()
    {
        CloseMessage.text = "Press \"E\" to close";
        CloseMessage.enabled = true;
        HintImage.SetActive(true);

        Time.timeScale = 0.0f;
    }

    void OpenLight()
    {
        openLight = true;
        CheatMessage.enabled = true;

        // open flashlight
        Flashlight.SetActive(false);

        // set skybox
        UnityEngine.RenderSettings.skybox = daySkybox;
        UnityEngine.RenderSettings.reflectionIntensity = 1.0f;
        UnityEngine.RenderSettings.ambientIntensity = 1.0f;
        DirectionalLight.intensity = 1.0f;
    }

    void ChangeCamera()
    {
        CloseMessage.text = "Press \"E\" to disable top-down camera";
        CloseMessage.enabled = true;

        changeCamera = true;
        cameraIndicator = 2;
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

        CloseMessage.enabled = true;
        MapImage.SetActive(true);

        Time.timeScale = 0.0f;
    }

    void ShowPath()
    {
        showPath = true;
        CloseMessage.text = "Press \"E\" to close";
        CheatMessage.enabled = true;

        Path.SetActive(true);
    }

    void TurnOffCheat()
    {
        // reset light
        UnityEngine.RenderSettings.skybox = nightSkybox;
        UnityEngine.RenderSettings.reflectionIntensity = 0.05f;
        UnityEngine.RenderSettings.ambientIntensity = 0.0f;
        DirectionalLight.intensity = 0f;
        Flashlight.SetActive(true);

        // reset camera
        TopDownCamera.enabled = false;
        PlayerViewCamera.enabled = true;
        cameraIndicator = 1;

        // hide map
        MapImage.SetActive(false);
        
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
