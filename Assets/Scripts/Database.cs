using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Database : MonoBehaviour
{
    public User[] users;
    public GameObject scorePrefab;
    public Transform scorePanel;

    // データベースにスコアを送信
    public IEnumerator SendScore(InputField input)
    {
        // リクエスト先URL
        string url = "http://localhost/nejikorun/sendscore.py";

        // リクエストパラメータを追加
        WWWForm form = new WWWForm();
        form.AddField("name", input.text);
        form.AddField("score", PlayerPrefs.GetInt("HighScore"));

        // Postリクエスト送信
        using (UnityWebRequest uwr = UnityWebRequest.Post(url, form))
        {
            yield return uwr.SendWebRequest();
            switch (uwr.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError("Error: " + uwr.error);
                    break;
            }
        }
    }

    // データベースからランキング取得
    public IEnumerator GetRanking()
    {
        // リクエスト先URL
        string url = "http://localhost/nejikorun/getranking.py";

        // GETリクエスト送信
        using (UnityWebRequest uwr = UnityWebRequest.Get(url))
        {
            yield return uwr.SendWebRequest();
            switch (uwr.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError("Error: " + uwr.error);
                    break;
                default:
                    // レスポンス内容のJSONからRankingクラスのインスタンスに変換
                    string responseText = uwr.downloadHandler.text;
                    Ranking ranking = JsonUtility.FromJson<Ranking>(responseText);
                    users = ranking.result;
                    break;
            }
        }

        ShowRanking();
    }

    // 取得したランキング情報を表示
    void ShowRanking()
    {
        for (int i = 0; i < users.Length; i++)
        {
            // ランキングのユーザーデータを1件取得
            User user = users[i];
            // 画面表示用のテキストプレハブから生成
            GameObject score = Instantiate(scorePrefab, scorePanel.transform);

            // Textコンポーネント取得、ランキングのスコア表示
            Text scoreText = score.GetComponent<Text>();
            scoreText.text = $"{i + 1:000}位 {user.name}:{user.score}m";
        }
    }
}

// データ格納用クラス
public class Ranking
{
    public User[] result;
}

[Serializable]
public class User
{
    public int id;
    public string name;
    public int score;
}