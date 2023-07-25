using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class CheatConsole : MonoBehaviour
{
    // player related
    private Transform PlayerTransform;

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
    public TextMeshProUGUI ResetMessage;
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

    // Start is called before the first frame update
    void Start()
    {
        // get objects
        PlayerTransform = GetComponent<Transform>();
        DirectionalLight = DirectionalLightObject.GetComponent<Light>();

        // Store the default skybox material before any modifications occur
        daySkybox = UnityEngine.RenderSettings.skybox;

        // turn off cheats when game start
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
            // show cheat switch message when player is in designed area and game is not paused
            CheatSwitchMessage.text = "Cheat mode switch\nPress \"E\" to open the map light";
            CheatSwitchMessage.enabled = true;
            if (openLight == true || GameStatus.pause == true)
            {
                CheatSwitchMessage.enabled = false;
            }

            // open light when press E
            if (Input.GetKeyDown(KeyCode.E))
            {
                OpenLight();
            }
        }
        else if (PlayerCollision.onSwitch == "ChangeCamera")
        {
            // show cheat switch message when player is in designed area and game is not paused and camera hasn't been changed
            CheatSwitchMessage.text = "Cheat mode switch\nPress \"E\" to change the camera to top-down view";
            CheatSwitchMessage.enabled = true;
            if (PlayerViewCamera.enabled == false || GameStatus.pause == true)
            {
                CheatSwitchMessage.enabled = false;
            }

            // change camera when press E (not change if already activated)
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (Time.timeScale != 0.0f)
                {
                    ChangeCamera();
                }
            }

            // re-show reset message when game resumed
            if (PlayerViewCamera.enabled == false && GameStatus.pause == false)
            {
                ResetMessage.enabled = true;
            }
        }
        else if (PlayerCollision.onSwitch == "ShowMap")
        {
            // show cheat switch message when player is in designed area and game is not paused
            CheatSwitchMessage.text = "Cheat mode switch\nPress \"E\" to show the maze map";
            CheatSwitchMessage.enabled = true;
            if (MapImage.activeSelf == true || GameStatus.pause == true)
            {
                CheatSwitchMessage.enabled = false;
            }

            // show image when press E (not show if already activated)
            if (Input.GetKeyDown(KeyCode.E) || showMap == true)
            {
                if (Time.timeScale != 0.0f)
                {
                    ShowMap();
                }
            }

            // start time if image is hidden and game is not paused
            if (MapImage.activeSelf == false && GameStatus.pause == false)
            {
                Time.timeScale = 1.0f;
                showMap = false;
            }

            // stop time if image is shown
            if (MapImage.activeSelf == true)
            {
                Time.timeScale = 0.0f;
            }
        }
        else if (PlayerCollision.onSwitch == "ShowPath")
        {
            // show cheat switch message when player is in designed area and game is not paused and function hasen't been activated
            CheatSwitchMessage.text = "Cheat mode switch\nPress \"E\" to show the solution path";
            CheatSwitchMessage.enabled = true;
            if (showPath == true || GameStatus.pause == true)
            {
                CheatSwitchMessage.enabled = false;
            }

            // show path when press E
            if (Input.GetKeyDown(KeyCode.E))
            {
                ShowPath();
            }
        }
        else if (PlayerCollision.onSwitch == "ReadHint")
        {
            // show cheat switch message when player is in designed area and game is not paused
            CheatSwitchMessage.text = "Press \"E\" to read";
            CheatSwitchMessage.enabled = true;
            if (HintImage.activeSelf == true || GameStatus.pause == true)
            {
                CheatSwitchMessage.enabled = false;
            }

            // show image when press E (not show if already activated)
            if (Input.GetKeyDown(KeyCode.E) || showHint == true)
            {
                if (Time.timeScale != 0.0f)
                {
                    ReadHint();
                }
            }

            // start time if image is hidden and game is not paused
            if (HintImage.activeSelf == false && GameStatus.pause == false)
            {
                Time.timeScale = 1.0f;
                showHint = false;
            }

            // stop time if image is shown
            if (HintImage.activeSelf == true)
            {
                Time.timeScale = 0.0f;
            }
        }
        else
        {
            CheatSwitchMessage.enabled = false;
        }

        // turn off all cheats
        if (CheatMessage.enabled == true)
        {
            if (Input.GetKeyDown(KeyCode.C) && GameStatus.pause == false)
            {
                TurnOffCheat();
            }
        }
    }

    void MonitorClose()
    {
        if (HintImage.activeSelf == true)
        {
            // hide image when press E or pause game
            if (Input.GetKeyDown(KeyCode.E) || GameStatus.pause == true)
            {
                CloseMessage.enabled = false;
                HintImage.SetActive(false);
            }
        }
        if (MapImage.activeSelf == true)
        {
            // hide image when press E or pause game
            if (Input.GetKeyDown(KeyCode.E) || GameStatus.pause == true)
            {
                CloseMessage.enabled = false;
                MapImage.SetActive(false);
            }
        }
        if (changeCamera == true)
        {
            // reset camera when press R
            if (Input.GetKeyDown(KeyCode.R) && GameStatus.pause == false)
            {
                ResetMessage.enabled = false;
                TopDownCamera.enabled = false;
                PlayerViewCamera.enabled = true;
                changeCamera = false;
            }
            else if (GameStatus.pause == true)
            {
                ResetMessage.enabled = false;
            }
        }
    }

    void ReadHint()
    {
        // update indicator
        showHint = true;
        
        // show message
        CloseMessage.enabled = true;

        // show image
        HintImage.SetActive(true);
    }

    void OpenLight()
    {
        // update indocator
        openLight = true;

        // show message
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
        // update indicators
        changeCamera = true;

        // show messages
        CheatMessage.enabled = true;
        ResetMessage.enabled = true;

        // update object status
        TopDownCamera.enabled = true;
        PlayerViewCamera.enabled = false;

        // reset player rotate angle
        Vector3 currentRotation = PlayerTransform.eulerAngles;
        currentRotation.y = -90.0f;
        PlayerTransform.eulerAngles = currentRotation;

        // reset camera rotate angle
        currentRotation = TopDownCamera.transform.eulerAngles;
        currentRotation.y = 90.0f;
        TopDownCamera.transform.eulerAngles = currentRotation;
    }

    void ShowMap()
    {
        // update indocator
        showMap = true;

        // show messages
        CheatMessage.enabled = true;
        CloseMessage.enabled = true;

        // show image
        MapImage.SetActive(true);

        Time.timeScale = 0.0f;
    }

    void ShowPath()
    {
        // update indocator
        showPath = true;

        // show message
        CheatMessage.enabled = true;

        // show path
        Path.SetActive(true);
    }

    void TurnOffCheat()
    {
        // reset light
        UnityEngine.RenderSettings.skybox = nightSkybox;
        UnityEngine.RenderSettings.reflectionIntensity = 0.01f;
        UnityEngine.RenderSettings.ambientIntensity = 0.0f;
        DirectionalLight.intensity = 0f;
        Flashlight.SetActive(true);

        // reset camera
        TopDownCamera.enabled = false;
        PlayerViewCamera.enabled = true;

        // hide map
        MapImage.SetActive(false);
        
        // hide path
        Path.SetActive(false);

        // hide cheat message
        CheatMessage.enabled = false;
        ResetMessage.enabled = false;

        // reset cheat function indicators
        openLight = false;
        changeCamera = false;
        showMap = false;
        showPath = false;
    }
}
