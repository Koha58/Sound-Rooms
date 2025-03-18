using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    private const float PlacementHeightOffset = 0.2f;  // プレイヤーの位置に加算する高さオフセット
    private const float PickupDistanceThreshold = 1.0f;  // 回収可能な距離のしきい値
    private const float ParticleRotationAngle = 90f;  // パーティクルの回転角度

    [SerializeField] private GameObject Recorder;
    [SerializeField] private GameObject objectPrefab;  // 設置するオブジェクトのプレハブ
    private GameObject placedObject = null;  // 設置されているオブジェクト

    [SerializeField] private GameObject particlePrefab;  // パーティクルのプレハブ

    [SerializeField] private Transform player;  // プレイヤーのTransform

    [SerializeField] private float maxPickupDistance = PickupDistanceThreshold;  // オブジェクトを回収できる最大距離

    public bool isOnSettingPoint = false;  // 「SettingPoint」に設置されているかどうか

    private RecordManager recordManager;  // RecordManager の参照
    private GameObject placedParticle = null;  // 設置されたパーティクル
    private AudioSource placedAudioSource;  // 設置されたオブジェクトのAudioSource

    private void Start()
    {
        Recorder = GameObject.Find("PanalinaGR100-VintageRadio");
        Recorder.SetActive(true);

        // RecordManager の参照を取得
        recordManager = FindObjectOfType<RecordManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))  // 左クリックを検出
        {
            // プレイヤーの位置を取得
            Vector3 playerPosition = player.position;

            // プレイヤーの位置からY軸にオフセットを加えた位置にオブジェクトを配置
            Vector3 placementPosition = new Vector3(playerPosition.x, playerPosition.y + PlacementHeightOffset, playerPosition.z);

            // オブジェクトがまだ設置されていない場合、新しく設置
            if (placedObject == null)
            {
                // 新しいオブジェクトを設置
                placedObject = Instantiate(objectPrefab, placementPosition, Quaternion.identity);
                Recorder.SetActive(false);

                // 音源をその位置に設定
                if (recordManager != null)
                {
                    recordManager.SetAudioSource(placedObject);
                    placedAudioSource = placedObject.GetComponent<AudioSource>();  // AudioSource を取得
                }

                // パーティクルをその位置に設置（X軸で90度回転させる）
                if (placedParticle == null && particlePrefab != null)
                {
                    placedParticle = Instantiate(particlePrefab, placementPosition, Quaternion.Euler(ParticleRotationAngle, 0f, 0f));  // X軸で90度回転
                }

                // 「SettingPoint」に接触しているかチェック
                CheckIfOnSettingPoint();
            }
            else
            {
                // すでにオブジェクトが配置されている場合、そのオブジェクトが近ければ回収
                if (Vector3.Distance(placedObject.transform.position, placementPosition) < maxPickupDistance)
                {
                    Destroy(placedObject);  // オブジェクトを回収
                    placedObject = null;  // 置かれているオブジェクトをリセット
                    Recorder.SetActive(true);

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

    private void CheckIfOnSettingPoint()
    {
        // 設置されたオブジェクトのコライダー周辺を探索
        Collider[] colliders = Physics.OverlapSphere(placedObject.transform.position, 0.5f);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("SettingPoint"))
            {
                isOnSettingPoint = true;
                Debug.Log("Object is on a SettingPoint!");
                return;
            }
        }
        isOnSettingPoint = false;
    }

    private void OnDrawGizmos()
    {
        // デバッグ用：範囲を可視化
        if (placedObject != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(placedObject.transform.position, 0.5f);
        }
    }
}
