using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class Database : MonoBehaviour
{
    public User[] users;

    private void Start()
    {
        StartCoroutine(GetRanking());
    }

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
    }
}

[Serializable]
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