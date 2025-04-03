using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Playerの服の透明度を変化させるクラス
/// </summary>
public class PlayerClothesSetMode : MonoBehaviour
{
    // Rendererコンポーネントの参照（オブジェクトのマテリアルを変更するために使用）
    Renderer rend;
    // 目標となるアルファ値（透明度）
    private float targetAlpha;
    // 透明度が変化する速さ
    private float fadeSpeed = 2.0f;

    // 定数の定義
    private const float INITIAL_ALPHA = 0.2f;  // 初期状態の透明度
    private const float PLAYER_VISIBLE_ALPHA = 1f;  // プレイヤーが見える場合の不透明度（完全不透明）
    private const float PLAYER_INVISIBLE_ALPHA = 0.5f;  // プレイヤーが見えない場合の透明度（半透明）
    private const int TRANSPARENT_RENDER_QUEUE = 3000;  // 透明オブジェクトのレンダリング順序
    private const int OPAQUE_RENDER_QUEUE = -1;  // 不透明オブジェクトのレンダリング順序
    private const int SRC_BLEND_SRC_ALPHA = (int)UnityEngine.Rendering.BlendMode.SrcAlpha;  // ソースアルファ
    private const int DST_BLEND_ONE_MINUS_SRC_ALPHA = (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha;  // 逆のアルファ
    private const int SRC_BLEND_ONE = (int)UnityEngine.Rendering.BlendMode.One;  // ソースの色をそのまま使用
    private const int DST_BLEND_ZERO = (int)UnityEngine.Rendering.BlendMode.Zero;  // 目的地の色は使用しない

    // Startは最初のフレームの前に呼ばれる
    void Start()
    {
        rend = GetComponent<Renderer>();  // このゲームオブジェクトのRendererコンポーネントを取得
        targetAlpha = INITIAL_ALPHA;  // 初期状態では透明度を0.2に設定（ほぼ透明）
    }

    // Updateは毎フレーム呼ばれる
    void Update()
    {
        GameObject obj = GameObject.Find("Player");  // Playerオブジェクトを探す
        PlayerSeen PS = obj.GetComponent<PlayerSeen>();  // PlayerSeenスクリプトを取得

        // PlayerSeenスクリプトの「onoff」変数の値によって透明度を変更
        if (!PS.isVisible)
        {
            targetAlpha = PLAYER_INVISIBLE_ALPHA;  // 「onoff」が0(Playerが透明)なら透明度を0.5に設定（半透明）
        }
        else
        {
            targetAlpha = PLAYER_VISIBLE_ALPHA;  // 「onoff」が0(Playerが透明)でない場合、完全に不透明に設定（透明度1）
        }

        // すべてのマテリアルについて、現在の透明度を目標透明度に向かって徐々に変更する
        for (int i = 0; i < rend.materials.Length; i++)
        {
            Material material = rend.materials[i];  // 現在のマテリアルを取得
            float currentAlpha = material.GetColor("_Color").a;  // 現在のアルファ（透明度）を取得

            // 目標透明度に向けて透明度を変化させる
            float newAlpha = Mathf.MoveTowards(currentAlpha, targetAlpha, fadeSpeed * Time.deltaTime);

            // 新しいアルファ値でマテリアルの色を更新
            Color newColor = material.GetColor("_Color");
            newColor.a = newAlpha;
            material.SetColor("_Color", newColor);

            // 透明モードの場合、適切なブレンド設定を変更
            if (targetAlpha < PLAYER_VISIBLE_ALPHA)  // 透明の場合
            {
                material.SetInt("_SrcBlend", SRC_BLEND_SRC_ALPHA);  // ソースブレンドを設定
                material.SetInt("_DstBlend", DST_BLEND_ONE_MINUS_SRC_ALPHA);  // デスティネーションブレンドを設定
                material.SetInt("_ZWrite", 0);  // ZWrite（深度書き込み）をオフにする
                material.DisableKeyword("_ALPHATEST_ON");  // アルファテストを無効にする
                material.EnableKeyword("_ALPHABLEND_ON");  // アルファブレンドを有効にする
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");  // アルファのプリマルチプライを無効にする
                material.renderQueue = TRANSPARENT_RENDER_QUEUE;  // 透明なオブジェクトのレンダリング順を設定（3000は透明オブジェクトの一般的な値）
            }
            else  // 不透明の場合
            {
                // 不透明モードの場合、ブレンド設定を元に戻す
                material.SetInt("_SrcBlend", SRC_BLEND_ONE);  // ソースブレンドを設定
                material.SetInt("_DstBlend", DST_BLEND_ZERO);  // デスティネーションブレンドを設定
                material.SetInt("_ZWrite", 1);  // ZWrite（深度書き込み）をオンにする
                material.DisableKeyword("_ALPHATEST_ON");  // アルファテストを無効にする
                material.DisableKeyword("_ALPHABLEND_ON");  // アルファブレンドを無効にする
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");  // アルファのプリマルチプライを無効にする
                material.renderQueue = OPAQUE_RENDER_QUEUE;  // 不透明なオブジェクトのレンダリング順を設定（通常は-1）
            }
        }
    }
}
