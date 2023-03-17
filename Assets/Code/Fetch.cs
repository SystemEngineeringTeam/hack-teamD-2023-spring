using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Threading.Tasks;

public class Fetch
{
public async Task<string[]> AsyncQ(string[] q)
{
    string url = AddressBarManager.adress;
    using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
    {
        webRequest.timeout = 60; // タイムアウト時間を設定する（単位は秒）

        try
        {
            // ネットワーク接続状態を確認する
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                Debug.LogError("ネットワーク接続がありません。");
                return null;
            }

            var operation = webRequest.SendWebRequest();

            while (!operation.isDone)
            {
                await Task.Yield();
            }

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(webRequest.error);
                return null;
            }
            else
            {
                Debug.Log(webRequest.downloadHandler.text);
                string returnStr = webRequest.downloadHandler.text.Replace("[", "").Replace("\"", "").Replace("]", "");
                return returnStr.Split(',');
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"例外が発生しました: {ex.Message}");
            return new string[]{"例外"};
        }
    }
}
}