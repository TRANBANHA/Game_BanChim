using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    public Birds[] birdsFrb;
    public float spawnTime;
    public int TimeLimit;


    int m_curTimeLimit;
    int m_birdKill;
    bool m_isGameOver;

    public bool IsGameOver { get => m_isGameOver; set => m_isGameOver = value; }
    public int BirdKill { get => m_birdKill; set => m_birdKill = value; }

    public override void Awake()
    {
        MakeSingleton(false);
        m_curTimeLimit= TimeLimit;
       // PlayerPrefs.DeleteAll();  //Xóa toàn bộ dữ liệu người chơi
    }

    public override void Start()
    {
        GameGUIManager.Ins.ShowGameGUI(false);
        GameGUIManager.Ins.UpdateKilledCounting(m_birdKill);

    }

    public void PlayGame()
    {
        StartCoroutine(GameSpawn());
        StartCoroutine(TimeCountDown());
        GameGUIManager.Ins.ShowGameGUI(true);
    }
    IEnumerator GameSpawn()
    {
        while(!m_isGameOver)
        {
            SpawnBird();
            yield return new WaitForSeconds(spawnTime);
        }
    }
    IEnumerator TimeCountDown()
    {
        while(m_curTimeLimit> 0)
        {
            yield return new WaitForSeconds(1f);
            m_curTimeLimit--;
            if (m_curTimeLimit <= 0)
            {

                m_isGameOver = true;


                if (m_birdKill > Prefs.bestScore)
                {
                    GameGUIManager.Ins.gameDialog.UpdateDialog("NEW BEST", "BEST KILLED: x" + m_birdKill);
                }
                else if(m_birdKill <= Prefs.bestScore)
                {
                    GameGUIManager.Ins.gameDialog.UpdateDialog("YOUR BEST", "BEST KILLED: x" + Prefs.bestScore);
                }
                //Debug.Log(Prefs.bestScore);
                Prefs.bestScore = m_birdKill;
                //  GameGUIManager.Ins.gameDialog.UpdateDialog("YOUR BEST", "BEST KILLED: x" + m_birdKill);
                GameGUIManager.Ins.gameDialog.Show(true);
                GameGUIManager.Ins.CurDialog=GameGUIManager.Ins.gameDialog;


                
            } 
            GameGUIManager.Ins.UpdateTimer(IntToTime(m_curTimeLimit));
        }
    }
    void SpawnBird()
    {
        Vector3 spawnPos = Vector3.zero;

        float randCheck = Random.Range(0f, 1f);

        if (randCheck >= 0.5f)
        {
            spawnPos = new Vector3(12,Random.Range(1.5f, 4f),0);

        }
        else
        {
            spawnPos = new Vector3(-12, Random.Range(1.5f, 4f), 0);

        }

        if(birdsFrb != null && birdsFrb.Length > 0)
        {
            
                int ranInx = Random.Range(0, birdsFrb.Length);

                if (birdsFrb[ranInx] != null)
                {
                    Birds birdsClone = Instantiate(birdsFrb[ranInx],spawnPos, Quaternion.identity);
                }
        }
    }

    string IntToTime(int time)
    {
        float minutes = Mathf.Floor(time / 60); 
        float seconds = Mathf.RoundToInt(time%60);
        return minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}
