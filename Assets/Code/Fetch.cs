using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;
using System.Linq;

public class Fetch
{
    private string baseurl = "localhost:8080/guest";

    public string[] SyncQ(string[] q)
    {
        var query = string.Join("&", q.Select(x => $"q[]={x}"));

        /*UriBuilder ub = new UriBuilder(baseurl);
        ub.Path = "/";
        ub.Query = query;*/

        string url = "localhost:8080/guest";//ub.ToString();

        UnityWebRequest webRequest = UnityWebRequest.Get(url);
        webRequest.SendWebRequest();

        while (!webRequest.isDone) { } // 通信完了まで待機

        if (webRequest.result != UnityWebRequest.Result.Success)
        {
            //通信失敗
            Debug.Log(webRequest.error);
            return null;
        }
        else
        {
            //通信成功
            //Jsonデータをstring[]に変換して返す
            Debug.Log(webRequest.downloadHandler.text);
            string returnStr = webRequest.downloadHandler.text.Replace("[", "").Replace("\"", "").Replace("]", "");
            return returnStr.Split(',');
        }
    }
}