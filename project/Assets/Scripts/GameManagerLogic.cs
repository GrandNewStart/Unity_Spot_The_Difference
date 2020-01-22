using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerLogic : MonoBehaviour
{
    [Header("Gameplay Elements")]
    public float total;
    public float time;
    public int stage;
    public bool pauseEnabled = true;
    public bool paused = false;

    [Header("UI Elements")]
    public Canvas HUD;
    public Canvas stageClear;
    public Canvas stageFail;
    public Canvas pauseMenu;
    public Text total_Text;
    public Text remainingShots_Text;
    public Text totalShots_Text;
    public Text time_Text;
    public Player player;
    private float remainingTime;
    private int count = 0;

    private void Start()
    {
        if (stage == 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        
        if (HUD != null)
        {
            HUD.gameObject.SetActive(true);
        }
        if (stageClear != null)
        {
            stageClear.gameObject.SetActive(false);
        }
        if (stageFail != null)
        {
            stageFail.gameObject.SetActive(false);
        }
        if (pauseMenu != null)
        {
            pauseMenu.gameObject.SetActive(false);
        }

        remainingTime = time;
        CountDown();
    }

    private void Update()
    {
        if (total_Text != null)
        {
            total_Text.text = total.ToString();
        }
        
        if (remainingShots_Text != null)
        {
            remainingShots_Text.text = player.shots.ToString();
        }
        
        if (time_Text != null)
        {
            time_Text.text = remainingTime.ToString();
        }

        if (total == 0)
        {
            StageClear();
        }

        if (++count % 60 == 0)
        {
            remainingTime--;
        }
    }

    public void CountDown()
    {
        if (stage % 2 == 1)
        {
            Invoke("NextStage", time);
        }
    }

    public void PauseMenu()
    {        
        if (pauseEnabled)
        {
            if (paused)
            {
                Time.timeScale = 1f;
                pauseMenu.gameObject.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                paused = false;
                player.fireEnabled = true;
            }
            else
            {
                Time.timeScale = 0f;
                pauseMenu.gameObject.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                paused = true;
                player.fireEnabled = false;
            }
        }
    }

    public void StageClear()
    {
        if (HUD != null)
        {
            if (stage == 2)
            {
                HUD.gameObject.SetActive(false);
                stageClear.gameObject.SetActive(true);
                Invoke("BackToMenu", 3.0f);
            }
            else
            {
                HUD.gameObject.SetActive(false);
                stageClear.gameObject.SetActive(true);
                Invoke("NextStage", 3.0f);
            }
        }
    }

    public void NextStage()
    {
        SceneManager.LoadScene(stage + 1);
    }

    public void ReloadStage()
    {
        SceneManager.LoadScene(stage);
    }

    public void NoMoreShots()
    {
        if (total != 0)
        {
            stageFail.gameObject.SetActive(true);
            Invoke("BackToMenu", 3.0f);
        }
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
