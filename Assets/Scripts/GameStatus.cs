using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStatus : MonoBehaviour
{
    // game status variables
    public static bool start = false;
    public static bool win = false;
    private bool pause = false;
    private float secondsCount = 0.0f;

    // UI texts
    public TextMeshProUGUI PauseMessage;
    public TextMeshProUGUI WinMessage;
    public TextMeshProUGUI Timer;

    // Start is called before the first frame update
    void Start()
    {
        // reset timer
        secondsCount = 0.0f;
        
        // hide UI elements
        PauseMessage.enabled = false;
        WinMessage.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // if start, update timer
        if (start == true)
        {
            secondsCount += Time.deltaTime;
            Timer.text = FormatTime(secondsCount);
        }

        // if win, pause game        
        if (win == true)
        {
            PauseGame("win");
        }

        // manually pause/resume game
        if (Input.GetKeyDown(KeyCode.Escape))
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
            PauseMessage.enabled = true;
        }
        else if (type == "win")
        {
            WinMessage.text = "You win!\nTime used: " + FormatTime(secondsCount) + "\nPress \"R\" to restart";
            WinMessage.enabled = true;
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
        PauseMessage.enabled = false;
        WinMessage.enabled = false;
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
        PauseMessage.enabled = false;
        WinMessage.enabled = false;

        // reload scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
