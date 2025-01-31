using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    private const float PlacementHeightOffset = 0.2f;  // �v���C���[�̈ʒu�ɉ��Z���鍂���I�t�Z�b�g
    private const float PickupDistanceThreshold = 1.0f;  // ����\�ȋ����̂������l
    private const float ParticleRotationAngle = 90f;  // �p�[�e�B�N���̉�]�p�x

    [SerializeField] private GameObject Recorder;
    [SerializeField] private GameObject objectPrefab;  // �ݒu����I�u�W�F�N�g�̃v���n�u
    private GameObject placedObject = null;  // �ݒu����Ă���I�u�W�F�N�g

    [SerializeField] private GameObject particlePrefab;  // �p�[�e�B�N���̃v���n�u

    [SerializeField] private Transform player;  // �v���C���[��Transform

    [SerializeField] private float maxPickupDistance = PickupDistanceThreshold;  // �I�u�W�F�N�g������ł���ő勗��

    public bool isOnSettingPoint = false;  // �uSettingPoint�v�ɐݒu����Ă��邩�ǂ���

    private RecordManager recordManager;  // RecordManager �̎Q��
    private GameObject placedParticle = null;  // �ݒu���ꂽ�p�[�e�B�N��
    private AudioSource placedAudioSource;  // �ݒu���ꂽ�I�u�W�F�N�g��AudioSource

    private void Start()
    {
        Recorder = GameObject.Find("PanalinaGR100-VintageRadio");
        Recorder.SetActive(true);

        // RecordManager �̎Q�Ƃ��擾
        recordManager = FindObjectOfType<RecordManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))  // ���N���b�N�����o
        {
            // �v���C���[�̈ʒu���擾
            Vector3 playerPosition = player.position;

            // �v���C���[�̈ʒu����Y���ɃI�t�Z�b�g���������ʒu�ɃI�u�W�F�N�g��z�u
            Vector3 placementPosition = new Vector3(playerPosition.x, playerPosition.y + PlacementHeightOffset, playerPosition.z);

            // �I�u�W�F�N�g���܂��ݒu����Ă��Ȃ��ꍇ�A�V�����ݒu
            if (placedObject == null)
            {
                // �V�����I�u�W�F�N�g��ݒu
                placedObject = Instantiate(objectPrefab, placementPosition, Quaternion.identity);
                Recorder.SetActive(false);

                // ���������̈ʒu�ɐݒ�
                if (recordManager != null)
                {
                    recordManager.SetAudioSource(placedObject);
                    placedAudioSource = placedObject.GetComponent<AudioSource>();  // AudioSource ���擾
                }

                // �p�[�e�B�N�������̈ʒu�ɐݒu�iX����90�x��]������j
                if (placedParticle == null && particlePrefab != null)
                {
                    placedParticle = Instantiate(particlePrefab, placementPosition, Quaternion.Euler(ParticleRotationAngle, 0f, 0f));  // X����90�x��]
                }

                // �uSettingPoint�v�ɐڐG���Ă��邩�`�F�b�N
                CheckIfOnSettingPoint();
            }
            else
            {
                // ���łɃI�u�W�F�N�g���z�u����Ă���ꍇ�A���̃I�u�W�F�N�g���߂���Ή��
                if (Vector3.Distance(placedObject.transform.position, placementPosition) < maxPickupDistance)
                {
                    Destroy(placedObject);  // �I�u�W�F�N�g�����
                    placedObject = null;  // �u����Ă���I�u�W�F�N�g�����Z�b�g
                    Recorder.SetActive(true);

                    // �p�[�e�B�N�������
                    if (placedParticle != null)
                    {
                        Destroy(placedParticle);
                        placedParticle = null;
                    }

                    isOnSettingPoint = false;  // �ݒu��ԃ��Z�b�g
                }
            }
        }

        // �������I�������ꍇ�A�p�[�e�B�N�����\���ɂ���
        if (placedAudioSource != null && !placedAudioSource.isPlaying && placedParticle != null)
        {
            placedParticle.SetActive(false);  // �p�[�e�B�N�����\��
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
