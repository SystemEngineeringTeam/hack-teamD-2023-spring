using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;
using UnityEngine;

public class OperateObj : MonoBehaviour{
    public Transform baseObject;
    ObjManager objManagerIns;

    void Start(){
        objManagerIns = FindObjectOfType<ObjManager>();
    }

    //吹き出しを作る関数
    public async Task<GameObject> MakeSuggestionAsync(int index, string[] inputs){
        //プレハブフォルダからプレハブ生成(isrightがtrueなら右向き)
        string prefabName = "";
        Vector3 pos = new Vector3(27f, 200f - index * 25, 0f);

        //インデックスが奇数なら右向き
        if(index % 2 == 1){
            prefabName = "SuggestionObj_right";
            pos.x *= -1;
        }
        else prefabName = "SuggestionObj_left";
        GameObject prefabObj = (GameObject)Resources.Load("Prefabs/" + prefabName);
        Instantiate(prefabObj, baseObject);

        //生成されたオブジェクトを取得
        GameObject returnObj = GameObject.Find(prefabName + "(Clone)");
        
        returnObj.name = "SuggestionObj" + index.ToString();
        returnObj.transform.position = pos + baseObject.transform.position;

        //test用重い処理
        TestHeavyClass testHeavy = new TestHeavyClass();
        string[] suggestTexts;
        suggestTexts = await Task.Run(() => testHeavy.TestHeavyProcess(inputs));

        //inputfieldとotherTextのテキストを変更する
        SuggestObjManager returnObjManagerIns = returnObj.GetComponent<SuggestObjManager>();
        returnObjManagerIns.inputField.text = suggestTexts[0];
        returnObjManagerIns.otherText1.text = suggestTexts[1];
        returnObjManagerIns.otherText2.text = suggestTexts[2];

        //吹き出しオブジェクトのリストに追加
        objManagerIns.suggestObjList.Add(returnObj);
        
        //矢印リセット
        objManagerIns.ResetLineSprite();

        Debug.Log("Generated Object!\n  name : " + prefabName);
        return returnObj;
    }
    //最初の一個目用オーバーロード
    public GameObject MakeSuggestionAsync(int index){
        //プレハブフォルダからプレハブ生成(isrightがtrueなら右向き)
        Vector3 pos = new Vector3(27f, 200f - index * 25, 0f);
        string prefabName = "Origin";
        pos.x *= -1;
        GameObject prefabObj = (GameObject)Resources.Load("Prefabs/" + prefabName);
        Instantiate(prefabObj, baseObject);

        //生成されたオブジェクトを取得
        GameObject returnObj = GameObject.Find(prefabName + "(Clone)");
        
        returnObj.name = "SuggestionObj" + index.ToString();
        returnObj.transform.position = pos + baseObject.transform.position;

        //吹き出しオブジェクトのリストに追加
        objManagerIns.suggestObjList.Add(returnObj);

        Debug.Log("Generated Object!\n  name : " + prefabName);
        return returnObj;
    }
}