using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using static InputDeviceManager;
using UnityEngine.UI;

// 鍵の獲得やボックスの移動を管理するスクリプト
public class ImpactOnObjects : MonoBehaviour
{
    // 鍵の獲得数を表示するためのUIテキスト
    public TextMeshProUGUI keyCountText;
    // 鍵の獲得数
    public int count;

    // 鍵を拾う際の効果音
    [SerializeField] AudioSource PickupSound;

    // コントローラーまたはキーボードのUI
    [SerializeField] GameObject ControllerKeyUI;
    [SerializeField] GameObject KeyboardKeyUI;

    // 鍵オブジェクトと鍵チェック用のオブジェクト
    GameObject Key;
    GameObject KeyCheck;

    // 入力デバイスの種類を判定するフラグ
    bool deviceCheck;

    void Start()
    {
        // 鍵のカウントを初期化し、UIに反映
        count = 0;
        SetCountText();

        // 鍵を拾った時の効果音を設定
        PickupSound = GetComponent<AudioSource>();

        // 鍵とそのチェック用のオブジェクトを探して取得
        Key = GameObject.FindGameObjectWithTag("Key");
        KeyCheck = GameObject.FindGameObjectWithTag("KeyCheck");

        // 入力デバイスの種類を確認し、フラグを設定
        if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Xbox)
        {
            deviceCheck = true; // コントローラーが使用されている
        }
        else if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Keyboard)
        {
            deviceCheck = false; // キーボードが使用されている
        }

        // 初期状態でUIは非表示
        ControllerKeyUI.GetComponent<Image>().enabled = false;
        KeyboardKeyUI.GetComponent<Image>().enabled = false;
    }

    private void Update()
    {
        // 入力デバイスの種類をリアルタイムで確認してフラグを更新
        if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Xbox)
        {
            deviceCheck = true; // コントローラーが使用されている
        }
        else if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Keyboard)
        {
            deviceCheck = false; // キーボードが使用されている
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Box"))
        {
            // Boxタグが付けられたオブジェクトに接触した場合、Rigidbodyを取得
            var rb = other.GetComponent<Rigidbody>();

            // Boxを移動・回転させるために制約を解除
            rb.constraints = RigidbodyConstraints.None;

            // オブジェクトに前方へ力を加えて移動
            rb.AddForce(transform.forward * 500.0f, ForceMode.Force);
        }
        else if (other.CompareTag("KeyCheck"))
        {
            // 入力デバイスに応じてUIの表示を切り替え
            if (deviceCheck)
            {
                // コントローラー使用時
                ControllerKeyUI.GetComponent<Image>().enabled = true;
                KeyboardKeyUI.GetComponent<Image>().enabled = false;
            }
            else
            {
                // キーボード使用時
                KeyboardKeyUI.GetComponent<Image>().enabled = true;
                ControllerKeyUI.GetComponent<Image>().enabled = false;
            }

            // 鍵を拾う操作を判定
            if (Input.GetMouseButtonUp(0) || Input.GetKeyUp("joystick button 0"))
            {
                // UIを非表示にする
                KeyboardKeyUI.GetComponent<Image>().enabled = false;
                ControllerKeyUI.GetComponent<Image>().enabled = false;

                // 鍵オブジェクトを非表示にして鍵を獲得
                Key.SetActive(false);
                KeyCheck.SetActive(false);

                // 効果音を再生
                PickupSound.PlayOneShot(PickupSound.clip);

                // 鍵のカウントを増加
                count++;

                // UIのカウントを更新
                SetCountText();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // UIを非表示にする
        KeyboardKeyUI.GetComponent<Image>().enabled = false;
        ControllerKeyUI.GetComponent<Image>().enabled = false;
    }

    // 鍵のカウントをUIに反映させる
    public void SetCountText()
    {
        keyCountText.text = count.ToString();
    }
}
