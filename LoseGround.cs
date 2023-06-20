using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseGround : MonoBehaviour
{
    //
    public GameObject winUI;
    public GameObject loseUI;
    public GameObject thePlayer;
    public GameObject touchUI;

    //enemy 카운트
    public int winScore;
    private int destroyScore;

    //win판정
    [HideInInspector]
    public bool isWin = false;

    private void OnTriggerEnter(Collider other)
    {
        //enemy파괴, 카운트에 맞게 파괴시 win
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(other.gameObject);
            destroyScore++;
            if (winScore == destroyScore)
            {
                winUI.SetActive(true);
                touchUI.SetActive(false);
                thePlayer.GetComponent<Animator>().SetTrigger("isWin");
                isWin = true;
            }
        }
        //player가 시간초 안에 떨어지면 lose
        else if(other.gameObject.tag == "Player")
        {
            if (!isWin)
            {
                loseUI.SetActive(true);
                touchUI.SetActive(false);
                thePlayer.GetComponent<Animator>().SetTrigger("isLose");
            }
        }
    }
}
