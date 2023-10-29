using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverUi;

    [SerializeField] private TextMeshProUGUI _scoreTxt;

    [SerializeField] private List<TextMeshProUGUI> _rankingScoreTxts;
    [SerializeField] private List<TextMeshProUGUI> _rankingNameTxts;
    
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

        UpdateScoreRanking();
        ShowScoreRanking();
    }

    public void HandleGameOver()
    {
        _audioSource.Stop();
        _audioSource.PlayOneShot(_gameOverClip);

        _gameOverUi.SetActive(true);
        _isGameOver = true;
    }

    private void UpdateScoreRanking()
    {
        List<Tuple<int, string>> rankingInfo = FetchRankingHistory();

        if (rankingInfo.Count.Equals(0))
        {
            // RankingScore0에 0이 붙음에 주의
            PlayerPrefs.SetInt($"RankingScore0", _curScore);
            PlayerPrefs.SetString($"RankingName0", _nameTxt.text);
            return;
        }

        bool isCurScoreStored = false;

        int tempScore = -1;
        string tempName = string.Empty;

        for (int i = 0; i < rankingInfo.Count; i++)
        {
            if (isCurScoreStored)
            {
                PlayerPrefs.SetInt($"RankingScore{i}", tempScore);
                tempScore = rankingInfo[i].Item1;

                PlayerPrefs.SetString($"RankingName{i}", tempName);
                tempName = rankingInfo[i].Item2;

                continue;
            }

            if (_curScore > rankingInfo[i].Item1)
            {
                tempScore = rankingInfo[i].Item1;
                tempName = rankingInfo[i].Item2;
                PlayerPrefs.SetInt($"RankingScore{i}", _curScore);
                PlayerPrefs.SetString($"RankingName{i}", _nameTxt.text);

                isCurScoreStored = true;
            }
        }

        if (rankingInfo.Count < _rankingScoreTxts.Count)
        {
            PlayerPrefs.SetInt($"RankingScore{rankingInfo.Count}", isCurScoreStored ? tempScore : _curScore);
            PlayerPrefs.SetString($"RankingName{rankingInfo.Count}", isCurScoreStored ? tempName : _nameTxt.text);
        }

        //if (rankingScores.Count < _rankingScoreTxts.Count
        //    && !isCurScoreStored)
        //    PlayerPrefs.SetInt($"RankingScore{rankingScores.Count}", _curScore);
    }

    private void ShowScoreRanking()
    {
        List<Tuple<int, string>> rankingInfo = FetchRankingHistory();

        for (int i = 0; i < rankingInfo.Count; i++)
        {
            _rankingScoreTxts[i].text = rankingInfo[i].Item1.ToString();
            _rankingNameTxts[i].text = rankingInfo[i].Item2;
        }
    }

    private List<Tuple<int, string>> FetchRankingHistory()
    {
        List<Tuple<int, string>> rankingInfo = new List<Tuple<int, string>>();

        for(int i =0; i < _rankingScoreTxts.Count; i++)
        {
            int score = PlayerPrefs.GetInt($"RankingScore{i}", -1);
            string name = PlayerPrefs.GetString($"RankingName{i}", string.Empty);

            // 랭킹 점수가 더는 존재하지 않음
            if (score.Equals(-1)) break;

            rankingInfo.Add(new Tuple<int, string>(score, name));
        }

        return rankingInfo;
    }
}
