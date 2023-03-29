using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    public GameObject panel;

    void Start()
    {
        StartCoroutine(AppearPanel(1));
        StartCoroutine(ChangeScene(12));
    }

    IEnumerator AppearPanel(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        panel.SetActive(true);
    }

    IEnumerator ChangeScene(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        LoadingScreen.instance.LoadLevel("Game");
    }
}
