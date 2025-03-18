using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// 視点移動を制御するクラス
/// </summary>
public class CameraRotator : MonoBehaviour
{
    // カメラのTransform（位置、回転、スケール）を格納する変数
    Transform cameraTrans;

    // プレイヤーのTransform（カメラが追従する対象）
    [SerializeField] Transform playerTrans;

    // カメラの相対位置を定義するベクトル
    // 例: Vector3(0, 1, -1) のようにカメラがプレイヤーの後ろ1m、上1mの位置に配置される
    [SerializeField] Vector3 cameraVec;

    // カメラの初期回転を設定するベクトル（角度）
    // 例: Vector3(45, 0, 0) はカメラの向きを45度上に傾ける
    [SerializeField] Vector3 cameraRot;

    // スクリプトが開始されるときに最初に呼ばれる
    void Awake()
    {
        // このスクリプトが付いているオブジェクト（カメラ）のTransformを取得
        cameraTrans = transform;

        // カメラの回転を指定した角度に設定（カメラが初めに向く方向を設定）
        cameraTrans.rotation = Quaternion.Euler(cameraRot);
    }

    // FixedUpdateは物理演算に基づく処理を毎フレーム実行する
    private void FixedUpdate()
    {
        // プレイヤーの位置にカメラの位置を相対的に設定
        // 例: プレイヤー位置 + カメラの相対位置ベクトル
        cameraTrans.position = playerTrans.position + cameraVec;
    }
}