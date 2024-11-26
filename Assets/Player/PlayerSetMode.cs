using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Playerの透明、不透明の状態変化
public class PlayerSetMode : MonoBehaviour
{
    Renderer rend;
    private float targetAlpha; // マテリアルの目標アルファ値
    private float fadeSpeed = 2.0f; // 透明度が変化する速さ

    // Startは最初のフレームの前に呼ばれる
    void Start()
    {
        rend = GetComponent<Renderer>(); // Rendererコンポーネントを取得
        targetAlpha = 0.2f; // 初期状態では透明
    }

    // Updateは毎フレーム呼ばれる
    void Update()
    {
        GameObject obj = GameObject.Find("Player");  // Playerオブジェクトを探す
        PlayerSeen PS = obj.GetComponent<PlayerSeen>();  // PlayerSeenスクリプトを取得

        if (PS.onoff == 0)
        {
            targetAlpha = 0.2f; // 透明にする（アルファ0.05）
        }
        else
        {
            targetAlpha = 1f; // 不透明に戻す（アルファ1）
        }

        // 透明度を目標値に向かって徐々に変更する
        for (int i = 0; i < rend.materials.Length; i++)
        {
            Material material = rend.materials[i];
            float currentAlpha = material.GetColor("_Color").a; // 現在のアルファ値を取得

            // 目標アルファに向かって徐々に変化させる
            float newAlpha = Mathf.MoveTowards(currentAlpha, targetAlpha, fadeSpeed * Time.deltaTime);

            // 新しいアルファ値で色を更新
            Color newColor = material.GetColor("_Color");
            newColor.a = newAlpha;
            material.SetColor("_Color", newColor);

            // 透明モードの場合、ブレンド設定を変更
            if (targetAlpha < 1f)
            {
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                material.SetInt("_ZWrite", 0); // ZWriteをオフにする
                material.DisableKeyword("_ALPHATEST_ON");
                material.EnableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = 3000; // 透明度のレンダリング順を設定
            }
            else
            {
                // 不透明モードの場合、ブレンド設定を戻す
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                material.SetInt("_ZWrite", 1); // ZWriteをオンにする
                material.DisableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = -1; // 不透明のレンダリング順を設定
            }
        }
    }
}
