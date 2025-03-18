using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// オーバーフロー時にログを表示するクラス
/// </summary>
public class OverflowHandler : MonoBehaviour
{
    public void OnBufferOverflow(uint overflow)
    {
        // オーバーフロー時に実行する処理をここに記述
        Debug.Log("スピーカーが接続されていません ");
    }
}
