using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Playerの下着の透明度を変化させるクラス
/// </summary>
public class UnderwareSet : MonoBehaviour
{
    Renderer rend;

    // 定数の定義
    private const float FULL_ALPHA = 1f;  // 完全に不透明な状態（アルファ値1）
    private const float TRANSPARENT_ALPHA = 0f;  // 完全に透明な状態（アルファ値0）
    private const int RENDER_QUEUE_TRANSPARENT = 3000;  // 透明オブジェクトのレンダリング順
    private const int RENDER_QUEUE_OPAQUE = -1;  // 不透明オブジェクトのレンダリング順
    private const int SRC_BLEND_SRC_ALPHA = (int)UnityEngine.Rendering.BlendMode.SrcAlpha;  // ソースアルファ
    private const int DST_BLEND_ONE_MINUS_SRC_ALPHA = (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha;  // 逆のアルファ
    private const int SRC_BLEND_ONE = (int)UnityEngine.Rendering.BlendMode.One;  // ソースの色をそのまま使用
    private const int DST_BLEND_ZERO = (int)UnityEngine.Rendering.BlendMode.Zero;  // 目的地の色は使用しない
    private const int ZWRITE_ON = 1;  // ZWrite（深度書き込み）をオンにする
    private const int ZWRITE_OFF = 0;  // ZWrite（深度書き込み）をオフにする

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();  // Rendererコンポーネントを取得
    }

    // Update is called once per frame
    void Update()
    {
        GameObject obj = GameObject.Find("Player");  // Playerオブジェクトを探す
        PlayerSeen PS = obj.GetComponent<PlayerSeen>();  // PlayerSeenスクリプトを取得

        if (rend != null && !PS.isVisible)
        {
            // プレイヤーが見えない場合、透明に設定
            SetMaterialTransparency(TRANSPARENT_ALPHA, RENDER_QUEUE_TRANSPARENT, SRC_BLEND_SRC_ALPHA, DST_BLEND_ONE_MINUS_SRC_ALPHA, ZWRITE_OFF);
        }
        else
        {
            // プレイヤーが見える場合、不透明に設定
            SetMaterialTransparency(FULL_ALPHA, RENDER_QUEUE_OPAQUE, SRC_BLEND_ONE, DST_BLEND_ZERO, ZWRITE_ON);
        }
    }

    // マテリアルの透明度やブレンド設定を変更するメソッド
    private void SetMaterialTransparency(float alpha, int renderQueue, int srcBlend, int dstBlend, int zWrite)
    {
        for (int i = 0; i < rend.materials.Length; i++)
        {
            Material material = rend.materials[i];

            // ブレンド設定を変更
            material.SetInt("_SrcBlend", srcBlend);
            material.SetInt("_DstBlend", dstBlend);
            material.SetInt("_ZWrite", zWrite);
            material.DisableKeyword("_ALPHATEST_ON");
            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            material.renderQueue = renderQueue;

            // 透明度を設定
            material.SetColor("_Color", new Color(1, 1, 1, alpha));  // アルファ値を設定
            if (alpha < FULL_ALPHA)
            {
                material.EnableKeyword("_ALPHABLEND_ON");  // 透明状態ならアルファブレンドを有効化
            }
            else
            {
                material.DisableKeyword("_ALPHABLEND_ON");  // 不透明状態ならアルファブレンドを無効化
            }
        }
    }
}
