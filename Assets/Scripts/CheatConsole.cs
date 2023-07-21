using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CheatConsole : MonoBehaviour
{
    // cheat function indicators
    private static bool openLight = false;
    private static bool changeCamera = false;
    private static bool showMap = false;
    private static bool showPath = false;

    // cheat messages
    public TextMeshProUGUI CheatSwitchMessage;
    public TextMeshProUGUI CheatMessage;

    // cheat objects
    // public GameObject Map;
    public GameObject Path;

    // Start is called before the first frame update
    void Start()
    {
        CheatSwitchMessage.enabled = false;
        CheatMessage.enabled = false;
        Path.SetActive(false);
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
                CheatMessage.enabled = false;
            }
        }
    }

    void OpenLight()
    {
        CheatMessage.enabled = true;
    }

    void ChangeCamera()
    {
        CheatMessage.enabled = true;
    }

    void ShowMap()
    {
        CheatMessage.enabled = true;
    }

    void ShowPath()
    {
        Path.SetActive(true);
        CheatMessage.enabled = true;
    }

    void TurnOffCheat()
    {
        Path.SetActive(false);
    }
}
