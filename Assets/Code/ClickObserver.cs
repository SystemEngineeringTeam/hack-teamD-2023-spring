using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

//矢印押されたことのみを監視するクラス
public class ClickObserver : MonoBehaviour{
    ObjManager objManagerIns;
    GameObject clickedGameObject;
    bool isPushing = false;

    void Start(){
        objManagerIns = FindObjectOfType<ObjManager>();
    }

    void Update () {
        //クリックの取得
        if (Input.GetMouseButtonDown(0) && isPushing == false){
            isPushing = true;
            clickedGameObject = null;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);
            if (hit) {
                clickedGameObject = hit.transform.gameObject;
            }
            WhenClicked(clickedGameObject);
        }else{
            isPushing = false;
        }
    }

    void WhenClicked(GameObject clickedGameObject){
        if(clickedGameObject.name == "line"){
            WhenClickedLine(clickedGameObject);
        }else if(clickedGameObject.name == "otherText1" || clickedGameObject.name == "otherText2"){
            WhenClickedOtherText(clickedGameObject);
        }else if(clickedGameObject.name == "reset"){
            WhenClickedReset(clickedGameObject);
        }
    }

    void WhenClickedLine(GameObject clickedGameObject){
        Debug.Log("クリックされました:line");
        //親オブジェクト取得
        GameObject parentObj = clickedGameObject.transform.parent.gameObject;

        //テキストが入力されていなければテキストを入力して下さいと入力して終了
        SuggestObjManager clickedObjManager = parentObj.GetComponent<SuggestObjManager>();
        string enteredText = clickedObjManager.inputField.text;
        if(enteredText == ""){
            clickedObjManager.inputField.text = "テキストを入力してください";
            return;
        }

        //親オブジェクトの名前から自分が何個目かを取得
        string numStr = parentObj.name;
        numStr = numStr.Remove(0, 13);

        //次のオブジェクトを生成と今までの入力の取得
        List<string> inputs = new List<string>();
        foreach(GameObject obj in objManagerIns.suggestObjList){
            SuggestObjManager temp = obj.GetComponent<SuggestObjManager>();
            inputs.Add(temp.inputField.text);
        }
        objManagerIns.MakeSuggestionAsync(int.Parse(numStr) + 1, inputs.ToArray());
        
        //矢印をラインにする
        objManagerIns.ArrowToLineSprite();
    }

    void WhenClickedOtherText(GameObject clickedGameObject){
        Debug.Log("クリックされました:" + clickedGameObject.name);
        
        //InputFieldと入れ替える
        TextMeshProUGUI clickedText = clickedGameObject.GetComponent<TextMeshProUGUI>();
        GameObject parentObj = clickedGameObject.transform.parent.gameObject;
        SuggestObjManager parentObjManager = parentObj.GetComponent<SuggestObjManager>();

        string temp = parentObjManager.inputField.text;
        parentObjManager.inputField.text = clickedText.text;
        clickedText.text = temp;
        
        //親オブジェクトの名前から自分が何個目かを取得
        string numStr = parentObj.name;
        numStr = numStr.Remove(0, 13);

        //入れ替えた先のオブジェクト全削除
        objManagerIns.ClearObj(int.Parse(numStr) + 1);
    }

    void WhenClickedReset(GameObject clickedGameObject){
        Debug.Log("クリックされました:" + clickedGameObject.name);
        objManagerIns.Reset();
    }
}