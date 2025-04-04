using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// �v���C���[�̃��C�t�Ǘ��N���X
/// �V�[���ɉ����đJ�ڐ��ύX
/// </summary>
public class GameOverManager : MonoBehaviour
{
    // �萔�̒�`

    /// <summary>
    /// �Q�[�����ōő�̃��C�t��
    /// </summary>
    private const int MAX_LIFE_COUNT = 5;

    /// <summary>
    /// �Q�[���J�n���̏������C�t���i�ő僉�C�t���Ɠ����j
    /// </summary>
    private const int INITIAL_LIFE_COUNT = MAX_LIFE_COUNT;

    /// <summary>
    /// �_���[�W�N�[���_�E�����ԁi�b�j
    /// </summary>
    private const float DAMAGE_COOLDOWN = 2.0f;

    /// <summary>
    /// �v���C���[���_�ł���Ԋu�i�b�j
    /// </summary>
    private const float BLINK_INTERVAL = 0.2f;

    /// <summary>
    /// �v���C���[�̓_�ŉ�
    /// </summary>
    private const int BLINK_COUNT = 5;

    /// <summary>
    /// �_���[�W����ɂ����鋗����臒l�i���[�g���j
    /// </summary>
    private const float DAMAGE_DISTANCE_THRESHOLD = 1f;

    // ���C�tUI�̃C���f�b�N�X
    private const int FIRST_LIFE_INDEX = 0; // 1�Ԗڂ̃��C�tUI�C���f�b�N�X�i0�x�[�X�j
    private const int FIFTH_LIFE_INDEX = 4; // 5�Ԗڂ̃��C�tUI�C���f�b�N�X�i0�x�[�X�j

    // ���C�t���[���ɂȂ�����Ԃ�\���萔
    private const int NO_LIFE = 0;

    // �v���C���[�̎c�胉�C�t��
    public int LifeCount;

    // ���C�tUI�I�u�W�F�N�g�i�ő僉�C�t�����̔z��j
    [SerializeField] private GameObject[] Life = new GameObject[MAX_LIFE_COUNT];

    // ����ꂽ���C�t��UI�I�u�W�F�N�g�i�ő僉�C�t�����̔z��j
    [SerializeField] private GameObject[] LostLife = new GameObject[MAX_LIFE_COUNT];

    // �_���[�W����AudioClip
    [SerializeField] private AudioClip damageSound;

    // �v���C���[��AudioSource
    private AudioSource audioSource;

    // �Ō�Ƀ_���[�W���󂯂����ԁi�N�[���_�E���p�j
    private float lastDamageTime = -1.0f;

    // �v���C���[��Renderer
    private Renderer[] playerRenderers;

    // �_�Ŏ��̐F
    private Color32 red = new Color32(255, 204, 204, 255);

    // ���݂̃V�[����
    private string currentScene;

    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;  // ���݂̃V�[�����擾

        // "PlayerParts" �^�O�������ׂẴI�u�W�F�N�g��Renderer���擾
        GameObject[] playerParts = GameObject.FindGameObjectsWithTag("PlayerParts");
        playerRenderers = new Renderer[playerParts.Length];

        // �e�I�u�W�F�N�g��Renderer���擾
        for (int i = 0; i < playerParts.Length; i++)
        {
            playerRenderers[i] = playerParts[i].GetComponent<Renderer>();
        }

        // �����ݒ�Ƃ��āA���C�t��UI��S�ĕ\��
        for (int i = FIRST_LIFE_INDEX; i <= FIFTH_LIFE_INDEX; i++)
        {
            Life[i].GetComponent<Image>().enabled = true;
            LostLife[i].GetComponent<Image>().enabled = false;
        }

        // ���C�t���������l�ɐݒ�
        LifeCount = INITIAL_LIFE_COUNT;

        audioSource = GetComponent<AudioSource>();  // AudioSource�����̃I�u�W�F�N�g����擾
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is not attached to this object!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // ���C�t���ɉ�����UI���X�V
        UpdateLifeUI();
    }

    // �v���C���[���Փ˂����Ƃ��ɌĂ΂��
    private void OnTriggerEnter(Collider other)
    {
        // �v���C���[�̈ʒu����w�肵�������ȓ����ǂ����`�F�b�N
        if (Vector3.Distance(other.transform.position, this.transform.position) > DAMAGE_DISTANCE_THRESHOLD)
        {
            return; // �w�苗���ȏ㗣��Ă���ꍇ�A�������Ȃ�
        }

        PlayerSeen PS;
        GameObject gobj = GameObject.Find("Player");  // �v���C���[�I�u�W�F�N�g��T��
        PS = gobj.GetComponent<PlayerSeen>();  // PlayerSeen�X�N���v�g���擾

        if (other.gameObject.tag == "Enemy" && Time.time - lastDamageTime >= DAMAGE_COOLDOWN)
        {
            // �v���C���[�������Ă����Ԃ̂Ƃ��������C�t������
            if (PS.isVisible)
            {
                // �N�[���^�C�����߂��Ă���΃_���[�W��^����
                LifeCount--;
                lastDamageTime = Time.time;  // �_���[�W���󂯂����Ԃ��L�^

                // �v���C���[��Ԃ��_�ł�����
                StartCoroutine(BlinkPlayer());

                // �_���[�WSE���Đ�
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

        // ���C�t����0�ɂȂ����ꍇ�A�V�[�����ƂɃQ�[���I�[�o�[��ʂɑJ��
        if (LifeCount == NO_LIFE)
        {
            Life[FIRST_LIFE_INDEX].GetComponent<Image>().enabled = false;  // 1�Ԗڂ̃��C�t�A�C�R�����\��
            LostLife[FIRST_LIFE_INDEX].GetComponent<Image>().enabled = true;  // ����ꂽ���C�t�A�C�R����\��

            // �v���C���[�̃��C�t��0�ɂȂ����Ƃ��A���݂̃V�[������ۑ�����GameOverScene�ɑJ��
            PlayerPrefs.SetString("PreviousScene", currentScene);  // ���݂̃V�[������ۑ�
            PlayerPrefs.Save();  // �ύX��ۑ�

            // GameOverScene�ɑJ��
            SceneManager.LoadScene("GameOverScene");
        }
    }

    // �v���C���[���Ԃ��_�ł���R���[�`��
    private IEnumerator BlinkPlayer()
    {
        // �v���C���[�̌��̐F��ۑ�
        Color[] originalColors = new Color[playerRenderers.Length];
        for (int i = 0; i < playerRenderers.Length; i++)
        {
            originalColors[i] = playerRenderers[i].material.color;
        }

        // �_�ŏ���
        for (int i = 0; i < BLINK_COUNT; i++)
        {
            // �v���C���[��Ԃ�
            for (int j = 0; j < playerRenderers.Length; j++)
            {
                playerRenderers[j].material.color = red;
            }

            yield return new WaitForSeconds(BLINK_INTERVAL);

            // ���̐F�ɖ߂�
            for (int j = 0; j < playerRenderers.Length; j++)
            {
                playerRenderers[j].material.color = originalColors[j];
            }

            yield return new WaitForSeconds(BLINK_INTERVAL);
        }
    }

    // ���C�t���ɉ�����UI���X�V
    private void UpdateLifeUI()
    {
        // ���ׂẴ��C�tUI���\����
        for (int i = FIRST_LIFE_INDEX; i <= FIFTH_LIFE_INDEX; i++)
        {
            Life[i].GetComponent<Image>().enabled = false;
            LostLife[i].GetComponent<Image>().enabled = false;
        }

        // �c�胉�C�t���ɉ�����UI���X�V
        for (int i = FIRST_LIFE_INDEX; i < LifeCount; i++)
        {
            Life[i].GetComponent<Image>().enabled = true;  // �c���Ă��郉�C�t�͕\��
            LostLife[i].GetComponent<Image>().enabled = false;  // ����ꂽ���C�t�͔�\��
        }

        // ����ꂽ���C�t��UI��\��
        for (int i = LifeCount; i < MAX_LIFE_COUNT; i++)
        {
            Life[i].GetComponent<Image>().enabled = false;  // ����ꂽ���C�t�͔�\��
            LostLife[i].GetComponent<Image>().enabled = true;  // ����ꂽ���C�t�͕\��
        }
    }
}
