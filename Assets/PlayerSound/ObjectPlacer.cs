using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectPlacer : MonoBehaviour
{
    GameObject Recorder;
    public GameObject objectPrefab;  // �ݒu����I�u�W�F�N�g�̃v���n�u
    private GameObject placedObject = null;  // �ݒu����Ă���I�u�W�F�N�g

    public Transform player;  // �v���C���[��Transform
    public float maxPickupDistance = 1.0f;  // �I�u�W�F�N�g������ł���ő勗��

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
            if (Input.GetMouseButtonDown(0))  // ���N���b�N�����o
            {
                // �v���C���[�̈ʒu���擾
                Vector3 playerPosition = player.position;

                // �v���C���[�ʒu����0.19��ɃI�u�W�F�N�g��z�u
                Vector3 placementPosition = new Vector3(playerPosition.x, playerPosition.y + 0.19f, playerPosition.z);


                // �I�u�W�F�N�g���܂��ݒu����Ă��Ȃ��ꍇ�A�V�����ݒu
                if (placedObject == null && clickToRecordAndVisualize.isRecording == false)
                {

                    // �V�����I�u�W�F�N�g��ݒu
                    placedObject = Instantiate(objectPrefab, placementPosition, Quaternion.identity);
                    Recorder.SetActive(false);

                }
                else
                {
                    // ���łɃI�u�W�F�N�g���z�u����Ă���ꍇ�A���̃I�u�W�F�N�g���v���C���[�ɋ߂���Ή��
                    if (Vector3.Distance(placedObject.transform.position, playerPosition) < maxPickupDistance)
                    {
                        Destroy(placedObject);  // �I�u�W�F�N�g�����
                        placedObject = null;  // �u����Ă���I�u�W�F�N�g�����Z�b�g
                        Recorder.SetActive(true);
                    }
                }

            }
        }
    }
}
