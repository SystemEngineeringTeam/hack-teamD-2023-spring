using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.UriBuilder;

public class Fetch
{

    public string[] SyncQ(string[] q)
    {


    private string baseurl = "/";

    var query = System.Web.HttpUtility.ParseQueryString("");
        for(string i in q){
        query.Add("q[]", i);
    }

    UriBuilder ub = new UriBuilder(
        "http", baseurl, 80, "guest", query.ToString());

    private string url = ub.ToString();
    UnityWebRequest webRequest = UnityWebRequest.Get(url);
    webRequest.SendWebRequest();
    if (webRequest.isNetworkError) {
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
