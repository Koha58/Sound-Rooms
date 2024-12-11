using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseUIRight : MonoBehaviour
{
    public Image targetImage; // 点滅させたいImageコンポーネント
    public Color color1 = Color.red; // 赤色
    public Color color2 = Color.white; // 白色
    public float blinkDuration = 1.0f; // 点滅の間隔（秒）

    private bool isColor1 = true; // 現在の色状態
    private float timer = 0f; // タイマー

    void Update()
    {
        if (targetImage == null) return;

        if (EnemyController1.ImageOn)
        {
            // 時間を更新
            timer += Time.deltaTime;

            // 指定された間隔を超えた場合に色を切り替える
            if (timer >= blinkDuration)
            {
                isColor1 = !isColor1; // 色状態を切り替える
                targetImage.color = isColor1 ? color1 : color2; // 色を変更
                timer = 0f; // タイマーをリセット
            }
        }
    }
}
