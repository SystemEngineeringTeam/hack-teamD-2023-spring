using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Fetch
{
    IEnumerator ASyncQ(string q)
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(url);
        yield return webRequest.SendWebRequest();
        if (webRequest.isNetworkError){
            //通信失敗
            Debug.Log(webRequest.error);
        }
        else
        {
            //通信成功
            //ZipクラスにJSONデータを格納する
            Zip zip = JsonUtility.FromJson<Zip>(webRequest.downloadHandler.text);
            //zipクラスに格納したJSONデータをゲームオブジェクトUI > Textに出力する
            textResult.text = zip.message + "," + zip.results.Length + "," + zip.status;
            foreach (ZipResult zr in zip.results)
            {
                textResult.text += string.Format("\n{0},{1},{2},{3}", zr.address1, zr.address2, zr.address3, zr.prefcode);
            }
        }
    }

    public string[] SyncQ(string[] q)
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(url);
        yield return webRequest.SendWebRequest();
        if (webRequest.isNetworkError){
            //通信失敗
            Debug.Log(webRequest.error);
        }
        else
        {
            //通信成功
            //ZipクラスにJSONデータを格納する
            return JsonUtility.FromJson<string[]>(webRequest.downloadHandler.text);
            //zipクラスに格納したJSONデータをゲームオブジェクトUI > Textに出力する
//            textResult.text = zip.message + "," + zip.results.Length + "," + zip.status;
//            foreach (ZipResult zr in zip.results)
//            {
//                textResult.text += string.Format("\n{0},{1},{2},{3}", zr.address1, zr.address2, zr.address3, zr.prefcode);
//            }
        }
    }
}
