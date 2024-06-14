using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverHeadMsg : MonoBehaviour
{
    //メッセージを表示するゲームオブジェクトを代入するためのフィールド
    //代入されたオブジェクトをメッセージが追従する
    public Transform targetTran;
    //public Renderer item;

    //ワールド座標（3Dオブジェクトの座標）からスクリーンの座標に変換して、そこに移動する
    //第１引数にカメラオブジェクト、第２引数にメッセージを表示したいオブジェクトの座標を渡す(今回は頭上に表示したいのでY軸方向に１上げた場所を指定)
    //Vector3.upはnew Vector3(0, 1, 0)を同じ(ex.上に0.5上げたければ、vector3.up * 0.5とすればいい)
    void Update()
    {
        //if (item.enabled == true)
        //{
            transform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, targetTran.position + Vector3.up);
       // }
    }
}
