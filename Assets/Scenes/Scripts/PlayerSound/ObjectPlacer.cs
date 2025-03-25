using UnityEngine;

/// <summary>
/// Player�����W�I��ݒu����̂��Ǘ�����N���X
/// </summary>
public class ObjectPlacer : MonoBehaviour
{
    // �v���C���[�̈ʒu�ɉ��Z���鍂���I�t�Z�b�g�i�I�u�W�F�N�g��������������j
    private const float PlacementHeightOffset = 0.2f;

    // ����\�ȋ����̂������l�i�v���C���[���I�u�W�F�N�g��������鋗����臒l�j
    private const float PickupDistanceThreshold = 1.0f;

    // �p�[�e�B�N���̉�]�p�x�i�ݒu���̃p�[�e�B�N������]������p�x�j
    private const float ParticleRotationAngle = 90f;

    // �ݒu����I�u�W�F�N�g�̃v���n�u
    [SerializeField] private GameObject Recorder;

    // �ݒu����I�u�W�F�N�g�̃v���n�u�i���ۂɔz�u����I�u�W�F�N�g�j
    [SerializeField] private GameObject objectPrefab;

    // �ݒu����Ă���I�u�W�F�N�g
    private GameObject placedObject = null;

    // �p�[�e�B�N���̃v���n�u�i�ݒu���Ƀp�[�e�B�N���𐶐��j
    [SerializeField] private GameObject particlePrefab;

    // �v���C���[��Transform�i�v���C���[�̈ʒu���擾���邽�߁j
    [SerializeField] private Transform player;

    // �I�u�W�F�N�g������ł���ő勗���i�v���C���[������ł��鋗���j
    [SerializeField] private float maxPickupDistance = PickupDistanceThreshold;

    // �uSettingPoint�v�ɐݒu����Ă��邩�ǂ���
    public bool isOnSettingPoint = false;

    // RecordManager �̎Q�Ɓi�����Ǘ��p�j
    private RecordManager recordManager;

    // �ݒu���ꂽ�p�[�e�B�N��
    private GameObject placedParticle = null;

    // �ݒu���ꂽ�I�u�W�F�N�g��AudioSource�i�������Đ����邽�߁j
    private AudioSource placedAudioSource;

    //InputSystem���擾
    private GameInputSystem inputActions;

    private bool isSpaceClickHeld, isEClickHeld, isXButtonHeld, isYButtonHeld;

    private void Awake()
    {
        // Input System�̃C���X�^���X���쐬
        inputActions = new GameInputSystem();

        //�X�y�[�X�̓��͂�o�^
        inputActions.Player.SpaceClick.performed += ctx => isSpaceClickHeld = true;
        inputActions.Player.SpaceClick.canceled += ctx => isSpaceClickHeld = false;

        //E�L�[�̓��͂�o�^
        inputActions.Player.EClick.performed += ctx => isEClickHeld = true;
        inputActions.Player.EClick.canceled += ctx => isEClickHeld = false;

        //X�̓��͂�o�^
        inputActions.Player.XButton.performed += ctx => isXButtonHeld = true;
        inputActions.Player.XButton.canceled += ctx => isXButtonHeld = false;

        //Y�̓��͂�o�^
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
        // Recorder�I�u�W�F�N�g�̎擾�ƗL����
        Recorder = GameObject.Find("PanalinaGR100-VintageRadio");
        Recorder.SetActive(true);

        // RecordManager �̎Q�Ƃ��擾
        recordManager = FindObjectOfType<RecordManager>();
    }

    void Update()
    {
        // �v���C���[��Transform���ݒ肳��Ă��Ȃ��ꍇ
        if (player == null)
        {
            Debug.LogError("Player Transform is not assigned.");
            return;
        }

        // �X�y�[�X�L�[�������ꂽ�Ƃ�
        if (isSpaceClickHeld || isXButtonHeld)  // ���N���b�N�����o
        {
            // �v���C���[�̈ʒu���擾
            Vector3 playerPosition = player.position;

            // �v���C���[�̈ʒu��Y���̃I�t�Z�b�g���������ʒu�ɃI�u�W�F�N�g��z�u
            Vector3 placementPosition = new Vector3(playerPosition.x, playerPosition.y + PlacementHeightOffset, playerPosition.z);

            // �I�u�W�F�N�g���܂��ݒu����Ă��Ȃ��ꍇ�A�V�����ݒu
            if (placedObject == null)
            {
                //�V�����I�u�W�F�N�g��ݒu
               placedObject = Instantiate(objectPrefab, placementPosition, Quaternion.identity);
                Recorder.SetActive(false);  // Recorder���\���ɂ���

                //RecordManager �����݂���Ή�����ݒ�
                if (recordManager != null)
                {
                    recordManager.SetAudioSource(placedObject);  // ������ݒ�
                    placedAudioSource = placedObject.GetComponent<AudioSource>();  // AudioSource���擾
                }

                //�p�[�e�B�N�������̈ʒu�ɐݒu�iX����90�x��]������j
                if (placedParticle == null && particlePrefab != null)
                {
                    placedParticle = Instantiate(particlePrefab, placementPosition, Quaternion.Euler(ParticleRotationAngle, 0f, 0f));  // X����90�x��]
                }

                //�uSettingPoint�v�ɐڐG���Ă��邩�`�F�b�N
                CheckIfOnSettingPoint();
            }
        }

        //E�L�[�������ꂽ��
        if(isEClickHeld || isYButtonHeld)
        {
            // �����I�u�W�F�N�g�����݂��A�v���C���[���w�肵�������ȓ��ɂ���Ȃ���
            if (placedObject != null)
            {
                // �v���C���[�̈ʒu���擾
                Vector3 playerPosition = player.position;

                // �v���C���[�̈ʒu��Y���̃I�t�Z�b�g���������ʒu�ɃI�u�W�F�N�g��z�u
                Vector3 placementPosition = new Vector3(playerPosition.x, playerPosition.y + PlacementHeightOffset, playerPosition.z);

                // ���łɃI�u�W�F�N�g���z�u����Ă���ꍇ�A���̃I�u�W�F�N�g���߂���Ή��
                if (Vector3.Distance(placedObject.transform.position, placementPosition) < maxPickupDistance)
                {
                    Destroy(placedObject);  // �I�u�W�F�N�g�����
                    placedObject = null;  // �u����Ă���I�u�W�F�N�g�����Z�b�g
                    Recorder.SetActive(true);  // Recorder���ĕ\��

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

    // �ݒu���ꂽ�I�u�W�F�N�g���uSettingPoint�v�ɐڐG���Ă��邩�ǂ������`�F�b�N
    private void CheckIfOnSettingPoint()
    {
        // �ݒu���ꂽ�I�u�W�F�N�g�̃R���C�_�[���ӂ�T��
        Collider[] colliders = Physics.OverlapSphere(placedObject.transform.position, 0.5f);  // ���a0.5�͈͓̔��̃R���C�_�[���擾
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("SettingPoint"))  // �uSettingPoint�v�^�O�̃I�u�W�F�N�g�ƐڐG���Ă�����
            {
                isOnSettingPoint = true;  // �ݒu����Ă���t���O�𗧂Ă�
                Debug.Log("Object is on a SettingPoint!");
                return;
            }
        }
        isOnSettingPoint = false;  // �ݒu����Ă��Ȃ��ꍇ�A�t���O�����Z�b�g
    }

    // �f�o�b�O�p�FGizmos���g���Đݒu�ʒu�͈̔͂�����
    private void OnDrawGizmos()
    {
        if (placedObject != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(placedObject.transform.position, 0.5f);  // �ݒu�ʒu�̎���ɔ͈͂�`��
        }
    }
}
