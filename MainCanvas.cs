using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvas : MonoBehaviour
{
    public GameObject titleUI;
    public GameObject menu;
    public GameObject selectUI;
    public GameObject levelSelectUI;

    public Animation cameraAnim;

    private void Start()
    {
        StartCoroutine(Title());
    }

    public void StartButton()
    {
        menu.SetActive(false);
        //selectUI.SetActive(true);
        levelSelectUI.SetActive(true);
    }

    IEnumerator Title()
    {
        cameraAnim.Play();
        yield return new WaitForSeconds(4.5f);
        titleUI.SetActive(false);
        menu.SetActive(true);
    }
}
