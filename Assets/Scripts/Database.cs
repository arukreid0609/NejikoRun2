using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Database : MonoBehaviour
{
    public User[] users;
    public GameObject scorePrefab;

    private void Start()
    {
        StartCoroutine(GetRanking());
    }

    // データベースにスコアを送信
    IEnumerator SendScore(string name, int score)
    {
        string url = "http://localhost/nejikorun/sendscore.py";
        WWWForm form = new WWWForm();

        form.AddField("name", name);
        form.AddField("score", score);

        using (UnityWebRequest uwr = UnityWebRequest.Post(url, form))
        {
            yield return uwr.SendWebRequest();
            if (uwr.isNetworkError || uwr.isHttpError)
            {
                Debug.LogError("Error: " + uwr.error);
            }
            else
            {
                string responseText = uwr.downloadHandler.text;
                Debug.Log(responseText);
            }
        }

    }

    // データベースからランキング取得
    IEnumerator GetRanking()
    {
        string url = "http://localhost/nejikorun/getranking.py";
        WWWForm form = new WWWForm();

        form.AddField("name", "nejiko");
        form.AddField("record", 9999);

        using (UnityWebRequest uwr = UnityWebRequest.Post(url, form))
        {
            yield return uwr.SendWebRequest();
            if (uwr.isNetworkError || uwr.isHttpError)
            {
                Debug.LogError("Error: " + uwr.error);
            }
            else
            {
                string responseText = uwr.downloadHandler.text;
                Ranking result = JsonUtility.FromJson<Ranking>(responseText);
                users = result.result;
            }
        }

        // 取得したランキング情報を表示
        for (int i = 0; i < users.Length; i++)
        {
            User user = users[i];
            // 画面表示用のテキストプレハブから生成
            GameObject score = Instantiate(scorePrefab, this.transform);
            // score.transform.SetParent(this.transform);

            // Textコンポーネント取得、ランキングのスコア表示
            Text scoreText = score.GetComponent<Text>();
            scoreText.text = $"{i + 1:000}位 {user.name}:{user.score}m";
        }
    }

}

// [Serializable]
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