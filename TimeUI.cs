using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeUI : MonoBehaviour
{
    public GameManager gameManager;

    public TextMeshProUGUI timerText;

    public GameObject timerUI;
    public GameObject loseUI;

    public GameObject touchUI;

    //패배존 
    public LoseGround loseGround;

    public bool isHell = false;
    public bool isHard = false;
    public bool isNormal = false;

    void Start()
    {
        gameManager = GameManager.instance;

        if (isHell)
            gameManager.timer = gameManager.hellModeStartTime;
        else if (isHard)
            gameManager.timer = gameManager.hardModeStartTIme;
        else if (isNormal)
            gameManager.timer = gameManager.normalModeStartTime;
        else
            gameManager.timer = gameManager.startTime;
    }

    void Update()
    {
        timerText.text = gameManager.timer.ToString("0");

        if (gameManager.timer <= 0)
        {
            GameEnd();
            return;
        }
        gameManager.timer -= Time.deltaTime;
    }

    void GameEnd()
    {
        //Time.timeScale = 0;
        //시간초안에 못이기면 lose
        if (!loseGround.isWin)
        {
            timerUI.SetActive(false);
            touchUI.SetActive(false);
            loseUI.SetActive(true);
        }

    }
}
