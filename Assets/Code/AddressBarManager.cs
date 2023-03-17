using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AddressBarManager : MonoBehaviour
{
    public TMP_InputField inputField;
    public static string adress = "";

    private void Awake(){
        // InputField(TMP)のコンポーネントを取得
        inputField = GetComponent<TMP_InputField>();
        // InputField(TMP)のコールバックにイベントリスナーを登録
        inputField.onEndEdit.AddListener(OnEndEdit);
    }

    private void OnEndEdit(string text){
        Debug.Log("アドレスが更新されました");
        adress = text;
    }
}
