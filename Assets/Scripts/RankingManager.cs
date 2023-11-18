using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingManager : MonoBehaviour
{
    public GameObject sendRankingCanvas;
    public Transform content;
    public Text scoreText;
    public InputField input;
    public Database database;
    public void Start()
    {
        sendRankingCanvas.SetActive(false);
        scoreText.text = $"Score:{PlayerPrefs.GetInt("HighScore")}m";
        StartCoroutine(database.GetRanking());
    }

    // ランキング用のキャンバス有効化
    public void ActiveSwitchCanvas()
    {
        bool active = sendRankingCanvas.activeSelf;
        sendRankingCanvas.SetActive(!active);
    }

    // スコア送信
    public void SendScore()
    {
        sendRankingCanvas.SetActive(false);
        StartCoroutine(database.SendScore(input));
    }

    // ランキングのスコア一覧削除
    public void DeleteScores()
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
    }
}
