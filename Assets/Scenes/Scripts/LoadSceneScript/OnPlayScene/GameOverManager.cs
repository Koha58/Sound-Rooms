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
    public int LifeCount;  // �v���C���[�̎c�胉�C�t��
    [SerializeField] GameObject Life1;  // ���C�t1��UI�I�u�W�F�N�g
    [SerializeField] GameObject Life2;  // ���C�t2��UI�I�u�W�F�N�g
    [SerializeField] GameObject Life3;  // ���C�t3��UI�I�u�W�F�N�g
    [SerializeField] GameObject Life4;  // ���C�t4��UI�I�u�W�F�N�g
    [SerializeField] GameObject Life5;  // ���C�t5��UI�I�u�W�F�N�g
    [SerializeField] GameObject LostLife1;  // ����ꂽ���C�t1��UI�I�u�W�F�N�g
    [SerializeField] GameObject LostLife2;  // ����ꂽ���C�t2��UI�I�u�W�F�N�g
    [SerializeField] GameObject LostLife3;  // ����ꂽ���C�t3��UI�I�u�W�F�N�g
    [SerializeField] GameObject LostLife4;  // ����ꂽ���C�t4��UI�I�u�W�F�N�g
    [SerializeField] GameObject LostLife5;  // ����ꂽ���C�t5��UI�I�u�W�F�N�g

    [SerializeField] AudioClip damageSound;  // �_���[�WSE��AudioClip
    private AudioSource audioSource;  // �v���C���[��AudioSource

    private float damageCooldown = 2.0f; // �N�[���^�C���̎��� (�b)
    private float lastDamageTime = -1.0f; // �Ō�Ƀ_���[�W���󂯂�����
    private Renderer[] playerRenderers; // �v���C���[��Renderer
                                        // �_�ł̐ݒ�
    private float blinkInterval = 0.2f;  // �_�ŊԊu (�b)
    private int blinkCount = 10;  // �_�ŉ�

    private Color32 darkgray = new Color32(255, 204, 204, 255);

    private string currentScene;  // ���݂̃V�[����

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
        Life1.GetComponent<Image>().enabled = true;
        Life2.GetComponent<Image>().enabled = true;
        Life3.GetComponent<Image>().enabled = true;
        Life4.GetComponent<Image>().enabled = true;
        Life5.GetComponent<Image>().enabled = true;

        // ����ꂽ���C�t��UI�͍ŏ��͔�\��
        LostLife1.GetComponent<Image>().enabled = false;
        LostLife2.GetComponent<Image>().enabled = false;
        LostLife3.GetComponent<Image>().enabled = false;
        LostLife4.GetComponent<Image>().enabled = false;
        LostLife5.GetComponent<Image>().enabled = false;

        // ���C�t����5�ɐݒ�
        LifeCount = 5;

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
        PlayerSeen PS;
        GameObject gobj = GameObject.Find("Player");  // �v���C���[�I�u�W�F�N�g��T��
        PS = gobj.GetComponent<PlayerSeen>();  // PlayerSeen�X�N���v�g���擾

        if (other.gameObject.tag == "Enemy" && Time.time - lastDamageTime >= damageCooldown)
        {
            // �v���C���[�������Ă����ԁionoff == 1�j�̂Ƃ��������C�t������
            if (PS.onoff == 1)
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
        if (LifeCount == 0)
        {
            Life1.GetComponent<Image>().enabled = false;  // 1�Ԗڂ̃��C�t�A�C�R�����\��
            LostLife1.GetComponent<Image>().enabled = true;  // ����ꂽ���C�t�A�C�R����\��

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
        for (int i = 0; i < blinkCount; i++)
        {
            // �v���C���[��Ԃ�
            for (int j = 0; j < playerRenderers.Length; j++)
            {
                playerRenderers[j].material.color = darkgray;
            }

            yield return new WaitForSeconds(blinkInterval);

            // ���̐F�ɖ߂�
            for (int j = 0; j < playerRenderers.Length; j++)
            {
                playerRenderers[j].material.color = originalColors[j];
            }

            yield return new WaitForSeconds(blinkInterval);
        }
    }

    // ���C�t���ɉ�����UI���X�V
    private void UpdateLifeUI()
    {
        if (LifeCount == 4)
        {
            Life5.GetComponent<Image>().enabled = false;  // ���C�t5���\��
            LostLife5.GetComponent<Image>().enabled = true;  // ����ꂽ���C�t5��\��
        }
        else if (LifeCount == 3)
        {
            Life4.GetComponent<Image>().enabled = false;  // ���C�t4���\��
            LostLife4.GetComponent<Image>().enabled = true;  // ����ꂽ���C�t4��\��
        }
        else if (LifeCount == 2)
        {
            Life3.GetComponent<Image>().enabled = false;  // ���C�t3���\��
            LostLife3.GetComponent<Image>().enabled = true;  // ����ꂽ���C�t3��\��
        }
        else if (LifeCount == 1)
        {
            Life2.GetComponent<Image>().enabled = false;  // ���C�t2���\��
            LostLife2.GetComponent<Image>().enabled = true;  // ����ꂽ���C�t2��\��
        }
    }
}
