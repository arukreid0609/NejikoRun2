using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingManager : MonoBehaviour
{
    public GameObject sendRankingCanvas;
    public Database database;
    public GameObject scorePrefab;
    public InputField input;
    public Transform scorePanel;
    public Text scoreText;
    public void Start()
    {
        sendRankingCanvas.SetActive(false);
    }

    // ランキング用のキャンバス有効化
    public void ActiveRankingCanvas()
    {
        sendRankingCanvas.SetActive(true);
        scoreText.text = $"Score:{PlayerPrefs.GetInt("HighScore")}m";
    }

    // ランキング取得
    public void GetRanking()
    {
        StartCoroutine(database.GetRanking());
    }

    // スコア送信
    public void SendScore()
    {
        sendRankingCanvas.SetActive(false);
        StartCoroutine(database.SendScore(input));
    }

    // ランキングのスコア一覧削除
    public void DeleteScores(Transform rankingSortPanel)
    {
        foreach (Transform child in rankingSortPanel)
        {
            Destroy(child.gameObject);
        }
    }
}
