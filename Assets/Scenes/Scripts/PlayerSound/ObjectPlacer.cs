using UnityEngine;

/// <summary>
/// Playerがラジオを設置するのを管理するクラス
/// </summary>
public class ObjectPlacer : MonoBehaviour
{
    // プレイヤーの位置に加算する高さオフセット（オブジェクトを少し浮かせる）
    private const float PlacementHeightOffset = 0.2f;

    // 回収可能な距離のしきい値（プレイヤーがオブジェクトを回収する距離の閾値）
    private const float PickupDistanceThreshold = 1.0f;

    // パーティクルの回転角度（設置時のパーティクルを回転させる角度）
    private const float ParticleRotationAngle = 90f;

    // 設置するオブジェクトのプレハブ
    [SerializeField] private GameObject Recorder;

    // 設置するオブジェクトのプレハブ（実際に配置するオブジェクト）
    [SerializeField] private GameObject objectPrefab;

    // 設置されているオブジェクト
    private GameObject placedObject = null;

    // パーティクルのプレハブ（設置時にパーティクルを生成）
    [SerializeField] private GameObject particlePrefab;

    // プレイヤーのTransform（プレイヤーの位置を取得するため）
    [SerializeField] private Transform player;

    // オブジェクトを回収できる最大距離（プレイヤーが回収できる距離）
    [SerializeField] private float maxPickupDistance = PickupDistanceThreshold;

    // 「SettingPoint」に設置されているかどうか
    public bool isOnSettingPoint = false;

    // RecordManager の参照（音声管理用）
    private RecordManager recordManager;

    // 設置されたパーティクル
    private GameObject placedParticle = null;

    // 設置されたオブジェクトのAudioSource（音声を再生するため）
    private AudioSource placedAudioSource;

    //InputSystemを取得
    private GameInputSystem inputActions;

    private bool isSpaceClickHeld, isEClickHeld, isXButtonHeld, isYButtonHeld;

    private void Awake()
    {
        // Input Systemのインスタンスを作成
        inputActions = new GameInputSystem();

        //スペースの入力を登録
        inputActions.Player.SpaceClick.performed += ctx => isSpaceClickHeld = true;
        inputActions.Player.SpaceClick.canceled += ctx => isSpaceClickHeld = false;

        //Eキーの入力を登録
        inputActions.Player.EClick.performed += ctx => isEClickHeld = true;
        inputActions.Player.EClick.canceled += ctx => isEClickHeld = false;

        //Xの入力を登録
        inputActions.Player.XButton.performed += ctx => isXButtonHeld = true;
        inputActions.Player.XButton.canceled += ctx => isXButtonHeld = false;

        //Yの入力を登録
        inputActions.Player.YButton.performed += ctx => isYButtonHeld = true;
        inputActions.Player.YButton.canceled += ctx => isYButtonHeld = false;
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Start()
    {
        // Recorderオブジェクトの取得と有効化
        Recorder = GameObject.Find("PanalinaGR100-VintageRadio");
        Recorder.SetActive(true);

        // RecordManager の参照を取得
        recordManager = FindObjectOfType<RecordManager>();
    }

    void Update()
    {
        // プレイヤーのTransformが設定されていない場合
        if (player == null)
        {
            Debug.LogError("Player Transform is not assigned.");
            return;
        }

        // スペースキーが押されたとき
        if (isSpaceClickHeld || isXButtonHeld)  // 左クリックを検出
        {
            // プレイヤーの位置を取得
            Vector3 playerPosition = player.position;

            // プレイヤーの位置にY軸のオフセットを加えた位置にオブジェクトを配置
            Vector3 placementPosition = new Vector3(playerPosition.x, playerPosition.y + PlacementHeightOffset, playerPosition.z);

            // オブジェクトがまだ設置されていない場合、新しく設置
            if (placedObject == null)
            {
                //新しいオブジェクトを設置
               placedObject = Instantiate(objectPrefab, placementPosition, Quaternion.identity);
                Recorder.SetActive(false);  // Recorderを非表示にする

                //RecordManager が存在すれば音源を設定
                if (recordManager != null)
                {
                    recordManager.SetAudioSource(placedObject);  // 音源を設定
                    placedAudioSource = placedObject.GetComponent<AudioSource>();  // AudioSourceを取得
                }

                //パーティクルをその位置に設置（X軸で90度回転させる）
                if (placedParticle == null && particlePrefab != null)
                {
                    placedParticle = Instantiate(particlePrefab, placementPosition, Quaternion.Euler(ParticleRotationAngle, 0f, 0f));  // X軸で90度回転
                }

                //「SettingPoint」に接触しているかチェック
                CheckIfOnSettingPoint();
            }
        }

        //Eキーが押された時
        if(isEClickHeld || isYButtonHeld)
        {
            // もしオブジェクトが存在し、プレイヤーが指定した距離以内にいるなら回収
            if (placedObject != null)
            {
                // プレイヤーの位置を取得
                Vector3 playerPosition = player.position;

                // プレイヤーの位置にY軸のオフセットを加えた位置にオブジェクトを配置
                Vector3 placementPosition = new Vector3(playerPosition.x, playerPosition.y + PlacementHeightOffset, playerPosition.z);

                // すでにオブジェクトが配置されている場合、そのオブジェクトが近ければ回収
                if (Vector3.Distance(placedObject.transform.position, placementPosition) < maxPickupDistance)
                {
                    Destroy(placedObject);  // オブジェクトを回収
                    placedObject = null;  // 置かれているオブジェクトをリセット
                    Recorder.SetActive(true);  // Recorderを再表示

                    // パーティクルも回収
                    if (placedParticle != null)
                    {
                        Destroy(placedParticle);
                        placedParticle = null;
                    }

                    isOnSettingPoint = false;  // 設置状態リセット
                }
            }
        }

        // 音声が終了した場合、パーティクルを非表示にする
        if (placedAudioSource != null && !placedAudioSource.isPlaying && placedParticle != null)
        {
            placedParticle.SetActive(false);  // パーティクルを非表示
        }
    }

    // 設置されたオブジェクトが「SettingPoint」に接触しているかどうかをチェック
    private void CheckIfOnSettingPoint()
    {
        // 設置されたオブジェクトのコライダー周辺を探索
        Collider[] colliders = Physics.OverlapSphere(placedObject.transform.position, 0.5f);  // 半径0.5の範囲内のコライダーを取得
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("SettingPoint"))  // 「SettingPoint」タグのオブジェクトと接触していたら
            {
                isOnSettingPoint = true;  // 設置されているフラグを立てる
                Debug.Log("Object is on a SettingPoint!");
                return;
            }
        }
        isOnSettingPoint = false;  // 設置されていない場合、フラグをリセット
    }

    // デバッグ用：Gizmosを使って設置位置の範囲を可視化
    private void OnDrawGizmos()
    {
        if (placedObject != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(placedObject.transform.position, 0.5f);  // 設置位置の周りに範囲を描画
        }
    }
}
