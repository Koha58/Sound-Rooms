using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �h�A�̑�����Ǘ�����N���X
public class DoorCheck : MonoBehaviour
{
    LevelMeter levelMeter;  // ���ʂ��Ǘ�����LevelMeter�X�N���v�g�̎Q��

    bool OnOff; // �h�A���L�����ǂ������Ǘ�����t���O

    GameObject Rote; // ��]����h�A�I�u�W�F�N�g

    public float rotateAngle; // �h�A�̉�]�p�x
    public float rotateSpeed; // �h�A�̉�]���x

    public bool Right; // �E��]���Ă��邩�ǂ����̃t���O

    [SerializeField] AudioSource RollingDoorSound; // ��]�h�A�̉����Ǘ�����AudioSource

    ParticleSystem EF; // �h�A�̃G�t�F�N�g�i�p�[�e�B�N���j

    void Start()
    {
        // �h�A�̏�����Ԃ�ݒ�
        GetComponent<Collider>().enabled = false; // �R���C�_�[�𖳌������ăh�A�������Ȃ�����
        OnOff = false; // �h�A���\������Ȃ���Ԃɂ���
        Right = false; // �E��]���Ă��Ȃ���Ԃɂ���

        // �h�A�̃p�[�e�B�N���G�t�F�N�g��������
        GameObject RotationDoorEffect = GameObject.Find("Dark Lvl 1");
        EF = RotationDoorEffect.GetComponent<ParticleSystem>();
        EF.Stop(); // �ŏ��̓G�t�F�N�g���~���Ă���
    }

    private void Update()
    {
        // SoundVolume�I�u�W�F�N�g���������A���ʂ��Ǘ����Ă���LevelMeter�X�N���v�g���擾
        GameObject soundobj = GameObject.Find("SoundVolume");
        levelMeter = soundobj.GetComponent<LevelMeter>();

        // �h�A�̃p�[�e�B�N���G�t�F�N�g���X�V
        GameObject RotationDoorEffect = GameObject.Find("Dark Lvl 1");
        EF = RotationDoorEffect.GetComponent<ParticleSystem>();

        // ���ʂ��[�����傫���Ƃ��Ƀh�A����]�\�ɂ���
        if (levelMeter.nowdB > 0.0f)
        {
            GetComponent<Collider>().enabled = true; // �R���C�_�[��L���ɂ��ăh�A����]�\�ɂ���
            OnOff = true; // �h�A����]�\�ɂ���
        }

        // ���ʂ��[���ɖ߂����Ƃ��Ƀh�A����]�s�ɂ��A�G�t�F�N�g�Ɖ����~
        if (OnOff == true)
        {
            if (levelMeter.nowdB == 0.0f)
            {
                GetComponent<Collider>().enabled = false; // �R���C�_�[�𖳌������ăh�A����]�s�ɂ���
                OnOff = false; // �h�A����]�s�ɂ���
                EF.Stop(); // �p�[�e�B�N���G�t�F�N�g���~
                RollingDoorSound.Stop(); // ��]�h�A�̉����~
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // �E�܂��͍��̃h�A���g���K�[���ɓ������Ƃ��ɃG�t�F�N�g�Ɖ����J�n
        GameObject RotationDoorEffect = GameObject.Find("Dark Lvl 1");
        EF = RotationDoorEffect.GetComponent<ParticleSystem>();

        if (other.CompareTag("Right"))//�E��]
        {
            EF.Play(); // �p�[�e�B�N���G�t�F�N�g���Đ�
            RollingDoorSound.PlayOneShot(RollingDoorSound.clip); // ��]�h�A�̉����Đ�
        }
        else if (other.CompareTag("Left") && !Right)//����]
        {
            EF.Play(); // �p�[�e�B�N���G�t�F�N�g���Đ�
            RollingDoorSound.PlayOneShot(RollingDoorSound.clip); // ��]�h�A�̉����Đ�
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Right"))
        {
            // �E�����ɉ�]����h�A�𑀍�
            Rote = other.transform.parent.gameObject;
            Rote.transform.Rotate(0, -rotateAngle * Time.deltaTime * rotateSpeed, 0); // ����]
            Right = true; // �E��]���ł��邱�Ƃ�����
        }
        else if (other.CompareTag("Left") && !Right)
        {
            // �������ɉ�]����h�A�𑀍�
            Rote = other.transform.parent.gameObject;
            Rote.transform.Rotate(0, rotateAngle * Time.deltaTime * rotateSpeed, 0); // �E��]
            Right = false; // ����]���ł��邱�Ƃ�����
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // �g���K�[����o���Ƃ��ɉ�]���~���A�G�t�F�N�g����~
        GameObject RotationDoorEffect = GameObject.Find("Dark Lvl 1");
        EF = RotationDoorEffect.GetComponent<ParticleSystem>();

        if (other.CompareTag("Right"))
        {
            EF.Stop(); // �G�t�F�N�g���~
            Rote = other.transform.parent.gameObject;
            Rote.transform.Rotate(0, -rotateAngle * Time.deltaTime * rotateSpeed, 0); // ��]���~�߂�i���̈ʒu�ɖ߂��j
            Right = false; // �E��]���Ă��Ȃ����Ƃ�����
        }
    }
}
