using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    private void Update()
    {
        // MainGame에 있는 GameOverCanvas gameobject를 참조
    }

    public void StartGame()
    {
        SceneManager.LoadScene("KYS_Test_Dummy"); // MainGame으로수정 요구
    }

    public void HandleGameOver()
    {
        // MainGame에 있는 GameOverCanvas gameobject를 SetActive(true)해줘야함
    }

    public void Quit()
    {
        Application.Quit();
    }
}
