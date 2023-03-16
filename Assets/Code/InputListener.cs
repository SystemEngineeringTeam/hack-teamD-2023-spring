using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputListener : MonoBehaviour{
    private ObjManager objManagerIns;
    private TMP_InputField inputField;
    private string previousText = "";

    void Start() {
        objManagerIns = FindObjectOfType<ObjManager>();
    }

    private void Awake(){
        // InputField(TMP)のコンポーネントを取得
        inputField = GetComponent<TMP_InputField>();
        // InputField(TMP)のコールバックにイベントリスナーを登録
        inputField.onSelect.AddListener(OnSelect);
        inputField.onEndEdit.AddListener(OnEndEdit);
        inputField.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnSelect(string text){
        // 現在のテキストを保持
        previousText = text;
    }

    private void OnEndEdit(string text){
        Debug.Log("更新されました:" + previousText);
        if(previousText != text){
            //これ以降の吹き出し削除
            //先に親の名前から自分が何個目かを取得
            Debug.Log("テキストが編集されました:" + text);
            GameObject parentObj = gameObject.transform.parent.gameObject;
            string numStr = parentObj.name;
            numStr = numStr.Remove(0, 13);
            objManagerIns.ClearObj(int.Parse(numStr) + 1);
        }
    }

    private void OnValueChanged(string text){
    }
}
