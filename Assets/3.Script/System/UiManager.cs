using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverUi;

    [SerializeField] private TextMeshProUGUI _scoreTxt;

    [SerializeField] private List<TextMeshProUGUI> _rankingScoreTxts;

    [SerializeField] private AudioSource _audioSource;

    //[SerializeField] private AudioClip _mainClip;
    [SerializeField] private AudioClip _gameOverClip;
    [SerializeField] private AudioClip _gameStartClip;

    [SerializeField] private TextMeshProUGUI _nameTxt;

    [SerializeField] private GameObject _rankingNameInput;
    [SerializeField] private GameObject _rankingResult;


    private float _startTime;

    private int _curScore = -1;
    private int _coinScore = 0;

    private bool _isGameOver = false;

    private void Update()
    {
        if (_isGameOver) return;

        _curScore = (int)(Time.time - _startTime) + _coinScore;
        _scoreTxt.text = $"Score : {_curScore}";
    }

    public void AddCoinScore(int count)
    {
        _coinScore += count;
    }

    public void OnEnable()
    {
        _audioSource.PlayOneShot(_gameStartClip);

        _startTime = Time.time;
        _gameOverUi.SetActive(false);
        _rankingNameInput.SetActive(true);
        _rankingResult.SetActive(false);
    }

    public void EnterUserName()
    {
        if (_nameTxt.text.Length <= 1) return;

        _rankingNameInput.SetActive(false);
        _rankingResult.SetActive(true);
    }

    public void HandleGameOver()
    {
        _audioSource.Stop();
        _audioSource.PlayOneShot(_gameOverClip);

        _gameOverUi.SetActive(true);
        _isGameOver = true;

        UpdateScoreRanking();
        ShowScoreRanking();
    }

    private void UpdateScoreRanking()
    {
        List<int> rankingScores = FetchScoreHistory();

        if (rankingScores.Count.Equals(0))
        {
            // RankingScore0�� 0�� ������ ����
            PlayerPrefs.SetInt($"RankingScore0", _curScore);
            return;
        }

        bool isCurScoreStored = false;

        int temp = -1;

        for (int i = 0; i < rankingScores.Count; i++)
        {
            if (isCurScoreStored)
            {
                PlayerPrefs.SetInt($"RankingScore{i}", temp);
                temp = rankingScores[i];

                continue;
            }

            if (_curScore > rankingScores[i])
            {
                temp = rankingScores[i];
                PlayerPrefs.SetInt($"RankingScore{i}", _curScore);

                isCurScoreStored = true;
            }
        }

        if (rankingScores.Count < _rankingScoreTxts.Count)
            PlayerPrefs.SetInt($"RankingScore{rankingScores.Count}", isCurScoreStored ? temp : _curScore);

        //if (rankingScores.Count < _rankingScoreTxts.Count
        //    && !isCurScoreStored)
        //    PlayerPrefs.SetInt($"RankingScore{rankingScores.Count}", _curScore);
    }

    private void ShowScoreRanking()
    {
        List<int> scores = FetchScoreHistory();

        for (int i = 0; i < scores.Count; i++)
            _rankingScoreTxts[i].text = scores[i].ToString();
    }

    private List<int> FetchScoreHistory()
    {
        List<int> scores = new List<int>();

        for(int i =0; i < _rankingScoreTxts.Count; i++)
        {
            int score = PlayerPrefs.GetInt($"RankingScore{i}", -1);

            // ��ŷ ������ ���� �������� ����
            if (score.Equals(-1)) break;

            scores.Add(score);
        }

        return scores;
    }
}
