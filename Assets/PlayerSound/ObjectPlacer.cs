using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectPlacer : MonoBehaviour
{
    GameObject Recorder;
    public GameObject objectPrefab;  // 設置するオブジェクトのプレハブ
    private GameObject placedObject = null;  // 設置されているオブジェクト

    public Transform player;  // プレイヤーのTransform
    public float maxPickupDistance = 1.0f;  // オブジェクトを回収できる最大距離

    public ClickToRecordAndVisualize clickToRecordAndVisualize;

    private void Start()
    {
        Recorder = GameObject.Find("PanalinaGR100-VintageRadio");
        Recorder.SetActive(true);
        clickToRecordAndVisualize.GetComponent<ClickToRecordAndVisualize>();

    }

    void Update()
    {
        if (clickToRecordAndVisualize.IsPointerOverUI()==false)
        {
            if (Input.GetMouseButtonDown(0))  // 左クリックを検出
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

                }
                else
                {
                    // すでにオブジェクトが配置されている場合、そのオブジェクトがプレイヤーに近ければ回収
                    if (Vector3.Distance(placedObject.transform.position, playerPosition) < maxPickupDistance)
                    {
                        Destroy(placedObject);  // オブジェクトを回収
                        placedObject = null;  // 置かれているオブジェクトをリセット
                        Recorder.SetActive(true);
                    }
                }

            }
        }
    }
}
