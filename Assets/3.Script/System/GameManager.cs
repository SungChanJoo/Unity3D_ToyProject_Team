using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    private readonly string _mainGameName = "KYS_Test_MainGame"; // MainGame으로 수정 요구
    //private readonly string _mainGameName = "MainGame"; // MainGame으로 수정 요구

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(_mainGameName);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("quit");
    }
}
