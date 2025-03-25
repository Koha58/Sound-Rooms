using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// GameSceneのエリア表示用アニメーション管理クラス
/// </summary>
public class SlideUIControll : MonoBehaviour
{
    // UIの状態を表す列挙型。Initial = 初期状態、SlideIn = スライドイン、SlideOut = スライドアウト
    public enum State
    {
        Initial = 0, // 初期状態
        SlideIn = 1, // スライドイン状態
        SlideOut = 2 // スライドアウト状態
    }

    // 現在の状態
    public State state = State.Initial;

    // UIがスライドアウトした後に戻るかどうかを決定するフラグ
    [SerializeField] private bool loop = false;

    // スライドインおよびスライドアウトの位置座標を設定
    [Header("Image")]
    [SerializeField] private Vector3 outPos01; // スライドイン前の初期位置
    [SerializeField] private Vector3 inPos;    // スライドイン後の表示位置
    [SerializeField] private Vector3 outPos02; // スライドアウト後の最終位置

    // スライドの速度を設定
    [Header("Speed")]
    [SerializeField] private float slideSpeedIn = DefaultSlideSpeed;  // スライドイン時の速度
    [SerializeField] private float slideSpeedOut = DefaultSlideSpeed; // スライドアウト時の速度

    // 定数
    private const float SlideCompletionThreshold = 0.1f;  // スライド完了の判定閾値
    private const float DefaultSlideSpeed = 10.0f;       // デフォルトのスライド速度

    void Update()
    {
        // 初期状態のUI位置（スライドアウトする前の位置）
        if (state == State.Initial)
        {
            // 現在の位置が outPos01 と異なっていたら、位置を outPos01 に設定
            if (transform.localPosition != outPos01)
                transform.localPosition = outPos01;
        }
        // スライドIN（UIが画面にスライドインしてくる）
        else if (state == State.SlideIn)
        {
            // 現在の位置が目的地（inPos）に十分近ければ、その位置に設定
            if (Vector3.Distance(transform.localPosition, inPos) < SlideCompletionThreshold)
            {
                transform.localPosition = inPos;
            }
            else
            {
                // 現在の位置から目的地（inPos）に向けてスライド
                transform.localPosition = Vector3.Lerp(transform.localPosition, inPos, slideSpeedIn * Time.unscaledDeltaTime);
            }
        }
        // スライドOUT（UIが画面からスライドアウトする）
        else if (state == State.SlideOut)
        {
            // 現在の位置が目的地（outPos02）に十分近ければ、その位置に設定
            if (Vector3.Distance(transform.localPosition, outPos02) < SlideCompletionThreshold)
            {
                transform.localPosition = outPos02;
            }
            else
            {
                // 現在の位置から目的地（outPos02）に向けてスライド
                transform.localPosition = Vector3.Lerp(transform.localPosition, outPos02, slideSpeedOut * Time.unscaledDeltaTime);
            }

            // スライドアウト後に初期状態（outPos02）に到達した場合
            if (Vector3.Distance(transform.localPosition, outPos02) < SlideCompletionThreshold)
            {
                // 'loop'がtrueなら、UIを初期位置に戻す
                if (loop)
                {
                    state = State.Initial; // 初期状態に戻す
                }
            }
        }
    }
}