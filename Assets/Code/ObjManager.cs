using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

public class ObjManager : MonoBehaviour{
    OperateObj operateObjIns;
    public List<GameObject> suggestObjList;

    void Start(){
        operateObjIns = FindObjectOfType<OperateObj>();
        suggestObjList = new List<GameObject>();

        //最初のオブジェクトを生成
        operateObjIns.MakeSuggestionAsync(1);
    }

    public void MakeSuggestionAsync(int index, string[] inputs){
        if(index-1 == suggestObjList.Count){
            operateObjIns.MakeSuggestionAsync(index, inputs);
        }
        else Debug.LogError("Error");
    }
    
    public void ResetLineSprite(){
        foreach(GameObject obj in suggestObjList){
            //自分が何個目かを名前から取得
            string numStr = obj.name;
            numStr = numStr.Remove(0, 13);
            if(suggestObjList.Count == int.Parse(numStr)){
                //スプライトを矢印に変更
                SuggestObjManager temp = obj.GetComponent<SuggestObjManager>();
                temp.ChangeLineSprite(true);
            }else{
                //スプライトをlineに変更
                SuggestObjManager temp = obj.GetComponent<SuggestObjManager>();
                temp.ChangeLineSprite(false);
            }
        }
        Debug.Log("sprite updated");
    }
    public void ArrowToLineSprite(){
        foreach(GameObject obj in suggestObjList){
            //スプライトをlineに変更
            SuggestObjManager temp = obj.GetComponent<SuggestObjManager>();
            temp.ChangeLineSprite(false);
        }
        Debug.Log("sprite updated");
    }
    public void ClearObj(int index){
        //index個めを含むその後のオブジェクトを消す
        List<GameObject> clearObj = new List<GameObject>();
        foreach(GameObject obj in suggestObjList){
            //名前から何個目かを取得
            string numStr = obj.name;
            numStr = numStr.Remove(0, 13);
            if(index <= int.Parse(numStr)){
                clearObj.Add(obj);
                Destroy(obj);
            }
        }
        foreach(GameObject obj in clearObj){
            suggestObjList.Remove(obj);
        }
        ResetLineSprite();
    }
    public void Reset(){
        ClearObj(2);
        SuggestObjManager firstObjManager = suggestObjList[0].GetComponent<SuggestObjManager>();
        firstObjManager.inputField.text = "";
    }
}
