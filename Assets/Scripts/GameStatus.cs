using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStatus : MonoBehaviour
{
    // game status variables
    public static bool welcome = false;
    public static bool start = false;
    public static bool win = false;
    public static bool pause = false;
    private float secondsCount = 0.0f;

    // UI texts
    public GameObject WelcomeMessage;
    public GameObject PauseMessage;
    public GameObject WinMessage;
    public GameObject Timer;
    public Text WelcomeMessageText;
    public Text PauseMessageText;
    public Text WinMessageText;
    public Text TimerText;

    // Start is called before the first frame update
    void Start()
    {
        // reset timer
        secondsCount = 0.0f;

        // get text components
        WelcomeMessageText = WelcomeMessage.GetComponent<Text>();
        PauseMessageText = PauseMessage.GetComponent<Text>();
        WinMessageText = WinMessage.GetComponent<Text>();
        TimerText = Timer.GetComponent<Text>();

        // hide UI elements
        PauseMessage.SetActive(false);
        WinMessage.SetActive(false);
        Timer.SetActive(false);

        // show welcome message
        welcome = true;
        WelcomeMessage.SetActive(true);
        Time.timeScale = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // show welcome message at the beginning
        if (welcome == true)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                welcome = false;
                WelcomeMessage.SetActive(false);
                Timer.SetActive(true);
                Time.timeScale = 1.0f;
            }
        }

        // if start, update timer
        if (start == true)
        {
            secondsCount += Time.deltaTime;
            TimerText.text = FormatTime(secondsCount);
        }

        // if win, pause game        
        if (win == true)
        {
            PauseGame("win");
        }

        // manually pause/resume game
        if (Input.GetKeyDown(KeyCode.P) && welcome == false)
        {
            if (pause == false)
            {
                PauseGame("pause");
            }
            else
            {
                ResumeGame();
            }
        }

        // manually restart game
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (pause == true)
            {
                RestartGame();
            }
        }
    }
    string FormatTime(float secondsCount)
    {
        if (secondsCount <= 0)
        {
            return "00:00";
        }
        return $"{PadInt((int)(secondsCount / 60))}:{PadInt((int)(secondsCount % 60))}";
    }

    public string PadInt(int time)
    {
        if (time < 10)
        {
            return $"0{time}";
        }

        return $"{time}";
    }

    public void PauseGame(string type)
    {
        // stop game time
        Time.timeScale = 0.0f;

        // set status variables
        pause = true;

        // unlock and unhide cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // display game status message
        if (type == "pause")
        {
            PauseMessage.SetActive(true);
        }
        else if (type == "win")
        {
            WinMessageText.text = "You win!\n\nTime used: " + FormatTime(secondsCount) + "\nPress \"R\" to restart";
            WinMessage.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        // start gane time
        Time.timeScale = 1.0f;
        
        // reset status variables
        pause = false;

        // lock and hide cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // hide UI elements
        PauseMessage.SetActive(false);
        WinMessage.SetActive(false);
    }

    public void RestartGame()
    {
        // start game time
        Time.timeScale = 1.0f;

        // reset status variables
        start = false;
        win = false;
        pause = false;
        secondsCount = 0.0f;

        // lock and hide cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // hide UI elements
        PauseMessage.SetActive(false);
        WinMessage.SetActive(false);

        // reload scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
