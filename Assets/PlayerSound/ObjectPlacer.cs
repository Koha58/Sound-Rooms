using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    public GameObject Recorder;
    public GameObject objectPrefab;  // 設置するオブジェクトのプレハブ
    private GameObject placedObject = null;  // 設置されているオブジェクト

    public Transform player;  // プレイヤーのTransform
    public float maxPickupDistance = 1.0f;  // オブジェクトを回収できる最大距離

    public ClickToRecordAndVisualize clickToRecordAndVisualize;

    public bool isOnSettingPoint = false;  // 「SettingPoint」に設置されているかどうか

    private void Start()
    {
        Recorder = GameObject.Find("PanalinaGR100-VintageRadio");
        Recorder.SetActive(true);
        clickToRecordAndVisualize.GetComponent<ClickToRecordAndVisualize>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))  // 左クリックを検出
        {
            // プレイヤーの位置を取得
            Vector3 playerPosition = player.position;

            // プレイヤー位置から0.19上にオブジェクトを配置
            Vector3 placementPosition = new Vector3(playerPosition.x, playerPosition.y + 0.19f, playerPosition.z);

            // オブジェクトがまだ設置されていない場合、新しく設置
            if (placedObject == null && clickToRecordAndVisualize.isRecording == false)
            {
                // 新しいオブジェクトを設置
                placedObject = Instantiate(objectPrefab, placementPosition, Quaternion.identity);
                Recorder.SetActive(false);

                // 「SettingPoint」に接触しているかチェック
                CheckIfOnSettingPoint();
            }
            else
            {
                // すでにオブジェクトが配置されている場合、そのオブジェクトがプレイヤーに近ければ回収
                if (Vector3.Distance(placedObject.transform.position, playerPosition) < maxPickupDistance)
                {
                    Destroy(placedObject);  // オブジェクトを回収
                    placedObject = null;  // 置かれているオブジェクトをリセット
                    Recorder.SetActive(true);
                    isOnSettingPoint = false;  // 設置状態リセット
                }
            }
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
