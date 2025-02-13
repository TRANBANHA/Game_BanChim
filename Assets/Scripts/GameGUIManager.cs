using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameGUIManager : Singleton<GameGUIManager>
{
    public GameObject homeGUI;
    public GameObject gameGUI;

    public Dialog gameDialog;
    public Dialog pauseDialog;
  //  public Dialog resumeDialog;

    public Image fireRateFilled;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI killedCountingText;

    Dialog m_curDialog;

    public Dialog CurDialog { get => m_curDialog; set => m_curDialog = value; }

    public override void Awake()
    {
        MakeSingleton(false);
    }


    public void ShowGameGUI(bool isShow)
    {
        if (gameGUI)
            gameGUI.SetActive(isShow);
        if (homeGUI)
        {
            homeGUI.SetActive(!isShow);
        }
    }

    public void UpdateTimer(string time)
    {
        if(timerText)
            timerText.text= time; 
    }

    public void UpdateKilledCounting(int killed)
    {
        if (killedCountingText)
            killedCountingText.text = "x"+killed.ToString();
    }

    public void UpdateFireRate(float rate)
    {
        if(fireRateFilled)
            fireRateFilled.fillAmount= rate;
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;

        if (pauseDialog)
        {
            pauseDialog.Show(true);
            pauseDialog.UpdateDialog("PAUSE", "BEST KILLED: x" + Prefs.bestScore);
            m_curDialog = pauseDialog;
        }
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        if (m_curDialog)
        {
            m_curDialog.Show(false);
        }
    }
    public void BackHome()
    {
        ResumeGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Replay()
    {
        if(m_curDialog)
            m_curDialog.Show(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        GameManager.Ins.PlayGame();
    }
    public void ExitGame()
    {
        ResumeGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        Application.Quit();
    }
}
