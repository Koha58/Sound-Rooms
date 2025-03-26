using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// ���_�ړ��𐧌䂷��N���X
/// </summary>
public class CameraRotator : MonoBehaviour
{
    // �J������Transform�i�ʒu�A��]�A�X�P�[���j���i�[����ϐ�
    Transform cameraTrans;

    // �v���C���[��Transform�i�J�������Ǐ]����Ώہj
    [SerializeField] Transform playerTrans;

    // �J�����̑��Έʒu���`����x�N�g��
    // ��: Vector3(0, 1, -1) �̂悤�ɃJ�������v���C���[�̌��1m�A��1m�̈ʒu�ɔz�u�����
    [SerializeField] Vector3 cameraVec;

    // �J�����̏�����]��ݒ肷��x�N�g���i�p�x�j
    // ��: Vector3(45, 0, 0) �̓J�����̌�����45�x��ɌX����
    [SerializeField] Vector3 cameraRot;

    // �X�N���v�g���J�n�����Ƃ��ɍŏ��ɌĂ΂��
    void Awake()
    {
        // ���̃X�N���v�g���t���Ă���I�u�W�F�N�g�i�J�����j��Transform���擾
        cameraTrans = transform;

        // �J�����̉�]���w�肵���p�x�ɐݒ�i�J���������߂Ɍ���������ݒ�j
        cameraTrans.rotation = Quaternion.Euler(cameraRot);
    }

    // FixedUpdate�͕������Z�Ɋ�Â������𖈃t���[�����s����
    private void FixedUpdate()
    {
        // �v���C���[�̈ʒu�ɃJ�����̈ʒu�𑊑ΓI�ɐݒ�
        // ��: �v���C���[�ʒu + �J�����̑��Έʒu�x�N�g��
        cameraTrans.position = playerTrans.position + cameraVec;
    }
}