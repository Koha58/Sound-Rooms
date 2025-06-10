using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundArea : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //GameObject obj = GameObject.Find("Player");         //Playerオブジェクトを探す
            //PlayerSeen PS = obj.GetComponent<PlayerSeen>();     //付いているスクリプト(PlayerSeen)を取得
            //PS.isVisible = true;       // プレイヤーを可視化
            //PS.isVisualization = true; // プレイヤーの可視化をオン
            Debug.Log("プレイヤーが音の範囲に入った → 可視化");
        }
    }
}
