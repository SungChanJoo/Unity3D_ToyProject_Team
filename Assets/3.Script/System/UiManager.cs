using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{

    [SerializeField] private GameObject _gameOverUi;

    [SerializeField] private TextMeshProUGUI _scoreTxt;

    private float _startTime;

    private bool _isGameOver = false;

    private void Update()
    {
        if (_isGameOver) return;

        _scoreTxt.text = $"Score : {(int)(Time.time - _startTime)}";
    }

    public void OnEnable()
    {
        _startTime = Time.time;
        _gameOverUi.SetActive(false);
    }

    public void HandleGameOver()
    {
        _gameOverUi.SetActive(true);
        _isGameOver = true;
    }
}
