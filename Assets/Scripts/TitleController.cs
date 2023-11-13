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
    }
    public void SendScore()
    {
        sendRankingCanvas.SetActive(false);
    }
}
