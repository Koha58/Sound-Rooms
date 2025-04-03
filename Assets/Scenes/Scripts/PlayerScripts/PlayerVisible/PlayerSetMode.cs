using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーモデルの透明、不透明状態を管理するクラス
/// </summary>
public class PlayerSetMode : MonoBehaviour
{
    // プレイヤーのRendererコンポーネント
    Renderer rend;
    // マテリアルの目標アルファ値（透明度）
    private float targetAlpha;
    // 透明度が変化する速さ
    private float fadeSpeed = 2.0f;

    // 定数の定義
    private const float INITIAL_ALPHA = 0.2f;  // 初期状態の透明度
    private const float VISIBLE_ALPHA = 1f;  // プレイヤーが見える状態の透明度（不透明）
    private const float INVISIBLE_ALPHA = 0.15f;  // プレイヤーが見えない状態の透明度
    private const int TRANSPARENT_RENDER_QUEUE = 3000;  // 透明オブジェクトのレンダリング順序
    private const int OPAQUE_RENDER_QUEUE = -1;  // 不透明オブジェクトのレンダリング順序
    private const int SRC_BLEND_SRC_ALPHA = (int)UnityEngine.Rendering.BlendMode.SrcAlpha;  // ソースアルファ
    private const int DST_BLEND_ONE_MINUS_SRC_ALPHA = (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha;  // 逆のアルファ
    private const int SRC_BLEND_ONE = (int)UnityEngine.Rendering.BlendMode.One;  // ソースの色をそのまま使用
    private const int DST_BLEND_ZERO = (int)UnityEngine.Rendering.BlendMode.Zero;  // 目的地の色は使用しない

    // Startは最初のフレームの前に呼ばれる
    void Start()
    {
        rend = GetComponent<Renderer>();  // Rendererコンポーネントを取得
        targetAlpha = INITIAL_ALPHA;  // 初期状態では透明（アルファ値0.2）
    }

    // Updateは毎フレーム呼ばれる
    void Update()
    {
        // "Player"という名前のオブジェクトを検索
        GameObject obj = GameObject.Find("Player");

        // PlayerオブジェクトからPlayerSeenスクリプトを取得
        PlayerSeen PS = obj.GetComponent<PlayerSeen>();

        // Playerの可視状態によって透明度を変更
        if (!PS.isVisible)  // プレイヤーが見えない状態
        {
            targetAlpha = INVISIBLE_ALPHA;  // 透明度を低く設定（アルファ0.15）
        }
        else  // プレイヤーが見える状態
        {
            targetAlpha = VISIBLE_ALPHA;  // 完全に不透明に設定（アルファ1）
        }

        // 透明度を目標アルファ値に向かって徐々に変化させる
        for (int i = 0; i < rend.materials.Length; i++)  // 複数のマテリアルがある場合に対応
        {
            Material material = rend.materials[i];  // 各マテリアルを取得
            float currentAlpha = material.GetColor("_Color").a;  // 現在のアルファ値を取得

            // 目標アルファに向かってアルファ値を徐々に変更
            float newAlpha = Mathf.MoveTowards(currentAlpha, targetAlpha, fadeSpeed * Time.deltaTime);

            // 新しいアルファ値を使って色を更新
            Color newColor = material.GetColor("_Color");
            newColor.a = newAlpha;  // アルファ値を変更
            material.SetColor("_Color", newColor);  // 新しい色をマテリアルに設定

            // 透明モードの場合、ブレンド設定を変更
            if (targetAlpha < VISIBLE_ALPHA)  // 透明の場合
            {
                material.SetInt("_SrcBlend", SRC_BLEND_SRC_ALPHA);  // ソースのアルファを使用
                material.SetInt("_DstBlend", DST_BLEND_ONE_MINUS_SRC_ALPHA);  // 逆のアルファを使用
                material.SetInt("_ZWrite", 0);  // ZWriteをオフにして、後ろにあるオブジェクトに隠れないようにする
                material.DisableKeyword("_ALPHATEST_ON");  // アルファテスト無効化
                material.EnableKeyword("_ALPHABLEND_ON");  // アルファブレンド有効化
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");  // アルファプレマルチ無効化
                material.renderQueue = TRANSPARENT_RENDER_QUEUE;  // 透明オブジェクトのレンダリング順序を設定
            }
            else  // 不透明の場合
            {
                material.SetInt("_SrcBlend", SRC_BLEND_ONE);  // ソースの色をそのまま使用
                material.SetInt("_DstBlend", DST_BLEND_ZERO);  // 目的地の色は使用しない
                material.SetInt("_ZWrite", 1);  // ZWriteをオンにして、前にあるオブジェクトが隠れるようにする
                material.DisableKeyword("_ALPHATEST_ON");  // アルファテスト無効化
                material.DisableKeyword("_ALPHABLEND_ON");  // アルファブレンド無効化
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");  // アルファプレマルチ無効化
                material.renderQueue = OPAQUE_RENDER_QUEUE;  // 不透明オブジェクトのレンダリング順序をデフォルトに戻す
            }
        }
    }
}
