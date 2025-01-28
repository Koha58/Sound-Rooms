using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    public GameObject Recorder;
    public GameObject objectPrefab;  // �ݒu����I�u�W�F�N�g�̃v���n�u
    private GameObject placedObject = null;  // �ݒu����Ă���I�u�W�F�N�g

    public Transform player;  // �v���C���[��Transform
    public float maxPickupDistance = 1.0f;  // �I�u�W�F�N�g������ł���ő勗��

    public ClickToRecordAndVisualize clickToRecordAndVisualize;

    public bool isOnSettingPoint = false;  // �uSettingPoint�v�ɐݒu����Ă��邩�ǂ���

    private void Start()
    {
        Recorder = GameObject.Find("PanalinaGR100-VintageRadio");
        Recorder.SetActive(true);
        clickToRecordAndVisualize.GetComponent<ClickToRecordAndVisualize>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))  // ���N���b�N�����o
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

                // �uSettingPoint�v�ɐڐG���Ă��邩�`�F�b�N
                CheckIfOnSettingPoint();
            }
            else
            {
                // ���łɃI�u�W�F�N�g���z�u����Ă���ꍇ�A���̃I�u�W�F�N�g���v���C���[�ɋ߂���Ή��
                if (Vector3.Distance(placedObject.transform.position, playerPosition) < maxPickupDistance)
                {
                    Destroy(placedObject);  // �I�u�W�F�N�g�����
                    placedObject = null;  // �u����Ă���I�u�W�F�N�g�����Z�b�g
                    Recorder.SetActive(true);
                    isOnSettingPoint = false;  // �ݒu��ԃ��Z�b�g
                }
            }
        }
    }

    private void CheckIfOnSettingPoint()
    {
        // �ݒu���ꂽ�I�u�W�F�N�g�̃R���C�_�[���ӂ�T��
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
        // �f�o�b�O�p�F�͈͂�����
        if (placedObject != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(placedObject.transform.position, 0.5f);
        }
    }
}
