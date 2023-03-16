using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SuggestObjManager : MonoBehaviour{
    public GameObject arrowToNext;
    public Sprite spriteArrow;
    public Sprite line;
    public Vector3 lineScale;
    public TMP_InputField inputField;
    public TextMeshProUGUI otherText1;
    public TextMeshProUGUI otherText2;
    
    public void ChangeLineSprite(bool isArrow){
        if(isArrow){
            //画像を矢印に変更後サイズも変更
            arrowToNext.GetComponent<SpriteRenderer>().sprite = spriteArrow;
            arrowToNext.transform.localScale = lineScale * 10;
        }else{
            //サイズを変更後画像も矢印に変更
            arrowToNext.transform.localScale = lineScale;
            arrowToNext.GetComponent<SpriteRenderer>().sprite = line;
        }
    }
}
