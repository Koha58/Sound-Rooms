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
    // 定数の定義

    /// <summary>
    /// ゲーム内で最大のライフ数
    /// </summary>
    private const int MAX_LIFE_COUNT = 5;

    /// <summary>
    /// ゲーム開始時の初期ライフ数（最大ライフ数と同じ）
    /// </summary>
    private const int INITIAL_LIFE_COUNT = MAX_LIFE_COUNT;

    /// <summary>
    /// ダメージクールダウン時間（秒）
    /// </summary>
    private const float DAMAGE_COOLDOWN = 2.0f;

    /// <summary>
    /// プレイヤーが点滅する間隔（秒）
    /// </summary>
    private const float BLINK_INTERVAL = 0.2f;

    /// <summary>
    /// プレイヤーの点滅回数
    /// </summary>
    private const int BLINK_COUNT = 5;

    /// <summary>
    /// ダメージ判定における距離の閾値（メートル）
    /// </summary>
    private const float DAMAGE_DISTANCE_THRESHOLD = 1f;

    // ライフUIのインデックス
    private const int FIRST_LIFE_INDEX = 0; // 1番目のライフUIインデックス（0ベース）
    private const int FIFTH_LIFE_INDEX = 4; // 5番目のライフUIインデックス（0ベース）

    // ライフがゼロになった状態を表す定数
    private const int NO_LIFE = 0;

    // プレイヤーの残りライフ数
    public int LifeCount;

    // ライフUIオブジェクト（最大ライフ数分の配列）
    [SerializeField] private GameObject[] Life = new GameObject[MAX_LIFE_COUNT];

    // 失われたライフのUIオブジェクト（最大ライフ数分の配列）
    [SerializeField] private GameObject[] LostLife = new GameObject[MAX_LIFE_COUNT];

    // ダメージ音のAudioClip
    [SerializeField] private AudioClip damageSound;

    // プレイヤーのAudioSource
    private AudioSource audioSource;

    // 最後にダメージを受けた時間（クールダウン用）
    private float lastDamageTime = -1.0f;

    // プレイヤーのRenderer
    private Renderer[] playerRenderers;

    // 点滅時の色
    private Color32 red = new Color32(255, 204, 204, 255);

    // 現在のシーン名
    private string currentScene;

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
        for (int i = FIRST_LIFE_INDEX; i <= FIFTH_LIFE_INDEX; i++)
        {
            Life[i].GetComponent<Image>().enabled = true;
            LostLife[i].GetComponent<Image>().enabled = false;
        }

        // ライフ数を初期値に設定
        LifeCount = INITIAL_LIFE_COUNT;

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
        // プレイヤーの位置から指定した距離以内かどうかチェック
        if (Vector3.Distance(other.transform.position, this.transform.position) > DAMAGE_DISTANCE_THRESHOLD)
        {
            return; // 指定距離以上離れている場合、何もしない
        }

        PlayerSeen PS;
        GameObject gobj = GameObject.Find("Player");  // プレイヤーオブジェクトを探す
        PS = gobj.GetComponent<PlayerSeen>();  // PlayerSeenスクリプトを取得

        if (other.gameObject.tag == "Enemy" && Time.time - lastDamageTime >= DAMAGE_COOLDOWN)
        {
            // プレイヤーが見えている状態のときだけライフが減る
            if (PS.isVisible)
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
        if (LifeCount == NO_LIFE)
        {
            Life[FIRST_LIFE_INDEX].GetComponent<Image>().enabled = false;  // 1番目のライフアイコンを非表示
            LostLife[FIRST_LIFE_INDEX].GetComponent<Image>().enabled = true;  // 失われたライフアイコンを表示

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
        for (int i = 0; i < BLINK_COUNT; i++)
        {
            // プレイヤーを赤く
            for (int j = 0; j < playerRenderers.Length; j++)
            {
                playerRenderers[j].material.color = red;
            }

            yield return new WaitForSeconds(BLINK_INTERVAL);

            // 元の色に戻す
            for (int j = 0; j < playerRenderers.Length; j++)
            {
                playerRenderers[j].material.color = originalColors[j];
            }

            yield return new WaitForSeconds(BLINK_INTERVAL);
        }
    }

    // ライフ数に応じてUIを更新
    private void UpdateLifeUI()
    {
        // すべてのライフUIを非表示に
        for (int i = FIRST_LIFE_INDEX; i <= FIFTH_LIFE_INDEX; i++)
        {
            Life[i].GetComponent<Image>().enabled = false;
            LostLife[i].GetComponent<Image>().enabled = false;
        }

        // 残りライフ数に応じてUIを更新
        for (int i = FIRST_LIFE_INDEX; i < LifeCount; i++)
        {
            Life[i].GetComponent<Image>().enabled = true;  // 残っているライフは表示
            LostLife[i].GetComponent<Image>().enabled = false;  // 失われたライフは非表示
        }

        // 失われたライフのUIを表示
        for (int i = LifeCount; i < MAX_LIFE_COUNT; i++)
        {
            Life[i].GetComponent<Image>().enabled = false;  // 失われたライフは非表示
            LostLife[i].GetComponent<Image>().enabled = true;  // 失われたライフは表示
        }
    }
}
