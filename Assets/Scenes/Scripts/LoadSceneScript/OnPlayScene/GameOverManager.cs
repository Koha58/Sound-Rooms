using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// プレイヤーのライフ管理クラス
/// シーンに応じて遷移先を変更
/// </summary>
public class GameOverManager : MonoBehaviour
{
    public int LifeCount;  // プレイヤーの残りライフ数
    [SerializeField] GameObject Life1;  // ライフ1のUIオブジェクト
    [SerializeField] GameObject Life2;  // ライフ2のUIオブジェクト
    [SerializeField] GameObject Life3;  // ライフ3のUIオブジェクト
    [SerializeField] GameObject Life4;  // ライフ4のUIオブジェクト
    [SerializeField] GameObject Life5;  // ライフ5のUIオブジェクト
    [SerializeField] GameObject LostLife1;  // 失われたライフ1のUIオブジェクト
    [SerializeField] GameObject LostLife2;  // 失われたライフ2のUIオブジェクト
    [SerializeField] GameObject LostLife3;  // 失われたライフ3のUIオブジェクト
    [SerializeField] GameObject LostLife4;  // 失われたライフ4のUIオブジェクト
    [SerializeField] GameObject LostLife5;  // 失われたライフ5のUIオブジェクト

    [SerializeField] AudioClip damageSound;  // ダメージSEのAudioClip
    private AudioSource audioSource;  // プレイヤーのAudioSource

    private float damageCooldown = 2.0f; // クールタイムの時間 (秒)
    private float lastDamageTime = -1.0f; // 最後にダメージを受けた時間
    private Renderer[] playerRenderers; // プレイヤーのRenderer
                                        // 点滅の設定
    private float blinkInterval = 0.2f;  // 点滅間隔 (秒)
    private int blinkCount = 10;  // 点滅回数

    private Color32 darkgray = new Color32(255, 204, 204, 255);

    private string currentScene;  // 現在のシーン名

    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;  // 現在のシーンを取得

        // "PlayerParts" タグを持つすべてのオブジェクトのRendererを取得
        GameObject[] playerParts = GameObject.FindGameObjectsWithTag("PlayerParts");
        playerRenderers = new Renderer[playerParts.Length];

        // 各オブジェクトのRendererを取得
        for (int i = 0; i < playerParts.Length; i++)
        {
            playerRenderers[i] = playerParts[i].GetComponent<Renderer>();
        }

        // 初期設定として、ライフのUIを全て表示
        Life1.GetComponent<Image>().enabled = true;
        Life2.GetComponent<Image>().enabled = true;
        Life3.GetComponent<Image>().enabled = true;
        Life4.GetComponent<Image>().enabled = true;
        Life5.GetComponent<Image>().enabled = true;

        // 失われたライフのUIは最初は非表示
        LostLife1.GetComponent<Image>().enabled = false;
        LostLife2.GetComponent<Image>().enabled = false;
        LostLife3.GetComponent<Image>().enabled = false;
        LostLife4.GetComponent<Image>().enabled = false;
        LostLife5.GetComponent<Image>().enabled = false;

        // ライフ数を5に設定
        LifeCount = 5;

        audioSource = GetComponent<AudioSource>();  // AudioSourceをこのオブジェクトから取得
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is not attached to this object!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // ライフ数に応じてUIを更新
        UpdateLifeUI();
    }

    // プレイヤーが衝突したときに呼ばれる
    private void OnTriggerEnter(Collider other)
    {
        PlayerSeen PS;
        GameObject gobj = GameObject.Find("Player");  // プレイヤーオブジェクトを探す
        PS = gobj.GetComponent<PlayerSeen>();  // PlayerSeenスクリプトを取得

        if (other.gameObject.tag == "Enemy" && Time.time - lastDamageTime >= damageCooldown)
        {
            // プレイヤーが見えている状態（onoff == 1）のときだけライフが減る
            if (PS.onoff == 1)
            {
                // クールタイムが過ぎていればダメージを与える
                LifeCount--;
                lastDamageTime = Time.time;  // ダメージを受けた時間を記録

                // プレイヤーを赤く点滅させる
                StartCoroutine(BlinkPlayer());

                // ダメージSEを再生
                if (audioSource != null && damageSound != null)
                {
                    Debug.Log("Playing damage sound!");
                    audioSource.PlayOneShot(damageSound);
                }
                else
                {
                    if (audioSource == null)
                    {
                        Debug.LogWarning("AudioSource is null!");
                    }
                    if (damageSound == null)
                    {
                        Debug.LogWarning("damageSound is null!");
                    }
                }
            }
        }

        // ライフ数が0になった場合、シーンごとにゲームオーバー画面に遷移
        if (LifeCount == 0)
        {
            Life1.GetComponent<Image>().enabled = false;  // 1番目のライフアイコンを非表示
            LostLife1.GetComponent<Image>().enabled = true;  // 失われたライフアイコンを表示

            // プレイヤーのライフが0になったとき、現在のシーン名を保存してGameOverSceneに遷移
            PlayerPrefs.SetString("PreviousScene", currentScene);  // 現在のシーン名を保存
            PlayerPrefs.Save();  // 変更を保存

            // GameOverSceneに遷移
            SceneManager.LoadScene("GameOverScene");
        }
    }

    // プレイヤーが赤く点滅するコルーチン
    private IEnumerator BlinkPlayer()
    {
        // プレイヤーの元の色を保存
        Color[] originalColors = new Color[playerRenderers.Length];
        for (int i = 0; i < playerRenderers.Length; i++)
        {
            originalColors[i] = playerRenderers[i].material.color;
        }

        // 点滅処理
        for (int i = 0; i < blinkCount; i++)
        {
            // プレイヤーを赤く
            for (int j = 0; j < playerRenderers.Length; j++)
            {
                playerRenderers[j].material.color = darkgray;
            }

            yield return new WaitForSeconds(blinkInterval);

            // 元の色に戻す
            for (int j = 0; j < playerRenderers.Length; j++)
            {
                playerRenderers[j].material.color = originalColors[j];
            }

            yield return new WaitForSeconds(blinkInterval);
        }
    }

    // ライフ数に応じてUIを更新
    private void UpdateLifeUI()
    {
        if (LifeCount == 4)
        {
            Life5.GetComponent<Image>().enabled = false;  // ライフ5を非表示
            LostLife5.GetComponent<Image>().enabled = true;  // 失われたライフ5を表示
        }
        else if (LifeCount == 3)
        {
            Life4.GetComponent<Image>().enabled = false;  // ライフ4を非表示
            LostLife4.GetComponent<Image>().enabled = true;  // 失われたライフ4を表示
        }
        else if (LifeCount == 2)
        {
            Life3.GetComponent<Image>().enabled = false;  // ライフ3を非表示
            LostLife3.GetComponent<Image>().enabled = true;  // 失われたライフ3を表示
        }
        else if (LifeCount == 1)
        {
            Life2.GetComponent<Image>().enabled = false;  // ライフ2を非表示
            LostLife2.GetComponent<Image>().enabled = true;  // 失われたライフ2を表示
        }
    }
}
