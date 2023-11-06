using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class Database : MonoBehaviour
{
    [Serializable]
    public class Result
    {
        public User[] result;
        [Serializable]
        public class User
        {
            public int id;
            public string name;
            public int record;

        }
    }
    private void Start()
    {
        StartCoroutine(GetRanking());
    }

    IEnumerator GetRanking()
    {
        string url = "http://localhost/nejikorun/sendscore.py";
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
                Debug.Log(responseText);
                // Result result = JsonUtility.FromJson<Result>(responseText);
                // Debug.Log(result.result[0].name);
                // Debug.Log(result.result[0].id);
                // Debug.Log(result.result[0].record);
                // Debug.Log("Response: " + responseText);
            }
        }
    }
}