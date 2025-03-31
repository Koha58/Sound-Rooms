using UnityEngine;

/// <summary>
/// Playerが持っている物の透明度を変化させるクラス
/// </summary>
public class PlayerHavingObjectsVisible : MonoBehaviour
{
    private const float INITIAL_ALPHA = 0.3f;  // 初期透明度（アルファ値）
    private const float FULL_ALPHA = 1f;      // 完全不透明（アルファ値）
    private const float FADE_SPEED = 2.0f;    // 透明度の変化速度

    // シェーダー設定用の定数
    // _SrcBlend に指定する値: ソースのアルファ値を使う
    private const int SRC_BLEND_SRC_ALPHA = (int)UnityEngine.Rendering.BlendMode.SrcAlpha;

    // _DstBlend に指定する値: ソースのアルファの逆を使う
    private const int DST_BLEND_ONE_MINUS_SRC_ALPHA = (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha;

    private const int ZWRITE_OFF = 0;  // ZWriteをオフ

    private Renderer rend;  // プレイヤーのRendererコンポーネント
    private float targetAlpha;  // マテリアルの目標アルファ値（透明度）

    // Startは最初のフレームの前に呼ばれる
    void Start()
    {
        rend = GetComponent<Renderer>();  // Rendererコンポーネントを取得
        if (rend != null)
        {
            // 透明シェーダーの設定
            rend.material.SetInt("_SrcBlend", SRC_BLEND_SRC_ALPHA);
            rend.material.SetInt("_DstBlend", DST_BLEND_ONE_MINUS_SRC_ALPHA);
            rend.material.SetInt("_ZWrite", ZWRITE_OFF);  // ZWriteをオフ
            rend.material.EnableKeyword("_ALPHABLEND_ON");  // アルファブレンド有効化
        }
        targetAlpha = INITIAL_ALPHA;  // 初期状態では透明（アルファ値0.3）
    }

    // Updateは毎フレーム呼ばれる
    void Update()
    {
        // "Player"という名前のオブジェクトを検索
        GameObject obj = GameObject.Find("Player");

        // PlayerオブジェクトからPlayerSeenスクリプトを取得
        PlayerSeen PS = obj.GetComponent<PlayerSeen>();

        // Playerの可視状態によって透明度を変更
        if (PS.onoff == 0)  // プレイヤーが見えない状態
        {
            targetAlpha = INITIAL_ALPHA;  // 透明度を設定（アルファ0.3）
        }
        else  // プレイヤーが見える状態
        {
            targetAlpha = FULL_ALPHA;  // 完全に不透明に設定（アルファ1）
        }

        // 透明度を目標アルファ値に向かって徐々に変化させる
        Color color = rend.material.color;
        color.a = Mathf.MoveTowards(color.a, targetAlpha, FADE_SPEED * Time.deltaTime);
        rend.material.color = color;  // 色を更新
    }
}
