using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleController : MonoBehaviour
{
    public Text highScoreText;
    public GameObject canvas;
    public GameObject sendRankingCanvas;
    public Database database;
    public GameObject scorePrefab;
    public InputField input;
    public Transform scorePanel;
    public Text scoreText;

    public void Start()
    {
        // ハイスコアを表示
        highScoreText.text = "HighScore : " + PlayerPrefs.GetInt("HighScore") + "m";
        sendRankingCanvas.SetActive(false);
    }
    public void OnStartButtonClicked()
    {
        SceneManager.LoadScene("Main");
    }

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
        StartCoroutine(database.SendScore());
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
