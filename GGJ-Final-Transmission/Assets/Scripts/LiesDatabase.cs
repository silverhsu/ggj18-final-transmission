using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class GetRandomMessages_Result
{
    public string MessageId;
    public string Text;
}

[System.Serializable]
public class JsonArray<T>
{
    public T[] array;
}

public class LiesDatabase : MonoBehaviour {
    
    private const string SERVER_URL = "http://youlietome-dev.us-east-2.elasticbeanstalk.com";

    public string templateId = "test";
    public int level = 0;
    public Text output = null;

    public static T ParseJsonObject<T>(string json)
    {
        return JsonUtility.FromJson<T>(json);
    }

    public static T[] ParseJsonArray<T>(string json)
    {
        string wrappedJson = "{\"array\":" + json + "}";
        JsonArray<T> wrapper = JsonUtility.FromJson<JsonArray<T>>(wrappedJson);
        return wrapper.array;
    }

    private IEnumerator GetRandomMessages_Coroutine(System.Action<GetRandomMessages_Result[]> callback)
    {
        string url = string.Format(
            "{0}/Message/GetRandomMessages?templateId={1}&level={2}",
            SERVER_URL,
            WWW.EscapeURL(templateId),
            level
        );
        Debug.Log(url);
        WWW request = new WWW(url);
        yield return request;

        try
        {
            string json = request.text;
            Debug.Log(json);

            var list = ParseJsonArray<GetRandomMessages_Result>(json);
            if (callback != null)
            {
                callback(list);
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogWarning("Failed to get random messages");
            Debug.LogWarning(ex);
        }
    }

    public void GetRandomMessages(System.Action<GetRandomMessages_Result[]> callback)
    {
        StartCoroutine(GetRandomMessages_Coroutine(callback));
    }

    private IEnumerator InsertMessage_Coroutine(string text)
    {
        string url = string.Format(
            "{0}/Message/InsertMessage?templateId={1}&level={2}&text={3}",
            SERVER_URL,
            WWW.EscapeURL(templateId),
            level,
            WWW.EscapeURL(text)
        );
        Debug.Log(url);
        WWW request = new WWW(url, new WWWForm());
        yield return request;
    }

    public void InsertMessage(string text)
    {
        StartCoroutine(InsertMessage_Coroutine(text));
    }

    private IEnumerator InsertDeaths_Coroutine(string messageId, int deaths)
    {
        string url = string.Format(
            "{0}/Message/InsertDeathes?templateId={1}&level={2}&messageId={3}&deaths={4}",
            SERVER_URL,
            WWW.EscapeURL(templateId),
            level,
            WWW.EscapeURL(messageId.ToString()),
            deaths
        );
        Debug.Log(url);
        WWW request = new WWW(url, new WWWForm());
        yield return request;
    }

    public void InsertDeaths(string messageId, int deaths)
    {
        StartCoroutine(InsertDeaths_Coroutine(messageId, deaths));
    }

    public void TestRandomMessages()
    {
        StartCoroutine(GetRandomMessages_Coroutine(null));
    }

    public void TestInsertMessage()
    {
        StartCoroutine(InsertMessage_Coroutine("wasd"));
    }

    public void TestInsertDeaths()
    {
        StartCoroutine(InsertDeaths_Coroutine("6828f8af-2e86-4518-9002-0fbfd366e9c6", 1));
    }

}
