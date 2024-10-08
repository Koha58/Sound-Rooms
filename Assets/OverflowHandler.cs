using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverflowHandler : MonoBehaviour
{
    public void OnBufferOverflow(uint overflow)
    {
        // オーバーフロー時に実行する処理をここに記述
        Debug.Log("スピーカーが接続されていません ");

        // 追加の処理（UIの更新、再初期化など）
        // Example: UIを更新する、警告音を鳴らす、状態をリセットするなど
    }
}
