using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 音を発生させ、範囲内の敵に通知を行うクラス
/// </summary>
public class SoundEmitter : MonoBehaviour
{
    public float soundRange = 5f; // 音が届く範囲（半径）

    // Startはゲーム開始時に呼ばれる
    void Start()
    {
        // 初期化処理は特にないので、空のまま
    }

    // Updateは毎フレーム呼ばれる
    void Update()
    {
        // 毎フレーム、音を発生させる
        EmitSound();
    }

    /// <summary>
    /// 音を発生させ、範囲内の敵に通知を行うメソッド
    /// </summary>
    public void EmitSound()
    {
        // OverlapSphereは指定した半径の範囲内にあるColliderを検出する
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, soundRange);

        // 検出されたすべてのColliderに対して処理を行う
        foreach (Collider collider in hitColliders)
        {
            // ColliderにEnemyControllerがアタッチされているか確認
            EnemyController enemy = collider.GetComponent<EnemyController>();

            // EnemyControllerが存在する場合、音を聞いた処理を呼び出す
            if (enemy != null)
            {
                // 敵キャラクターに音が聞こえたことを通知
                enemy.OnSoundHeard(this.transform.position);
            }
        }
    }

    /// <summary>
    /// 音の範囲をシーンビューで可視化するためのメソッド
    /// </summary>
    void OnDrawGizmosSelected()
    {
        // 音の範囲を黄色のワイヤーフレームで可視化
        Gizmos.color = Color.yellow;
        // 音の範囲をワイヤースフィアで描画
        Gizmos.DrawWireSphere(this.transform.position, soundRange);
    }
}
