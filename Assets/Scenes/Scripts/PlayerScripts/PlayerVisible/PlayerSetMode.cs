using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[���f���̓����A�s������Ԃ��Ǘ�����N���X
/// </summary>
public class PlayerSetMode : MonoBehaviour
{
    Renderer rend;  // �v���C���[��Renderer�R���|�[�l���g
    private float targetAlpha;  // �}�e���A���̖ڕW�A���t�@�l�i�����x�j
    private float fadeSpeed = 2.0f;  // �����x���ω����鑬��

    // Start�͍ŏ��̃t���[���̑O�ɌĂ΂��
    void Start()
    {
        rend = GetComponent<Renderer>();  // Renderer�R���|�[�l���g���擾
        targetAlpha = 0.2f;  // ������Ԃł͓����i�A���t�@�l0.2�j
    }

    // Update�͖��t���[���Ă΂��
    void Update()
    {
        // "Player"�Ƃ������O�̃I�u�W�F�N�g������
        GameObject obj = GameObject.Find("Player");

        // Player�I�u�W�F�N�g����PlayerSeen�X�N���v�g���擾
        PlayerSeen PS = obj.GetComponent<PlayerSeen>();

        // Player�̉���Ԃɂ���ē����x��ύX
        if (PS.onoff == 0)  // �v���C���[�������Ȃ����
        {
            targetAlpha = 0.15f;  // �����x��Ⴍ�ݒ�i�A���t�@0.15�j
        }
        else  // �v���C���[����������
        {
            targetAlpha = 1f;  // ���S�ɕs�����ɐݒ�i�A���t�@1�j
        }

        // �����x��ڕW�A���t�@�l�Ɍ������ď��X�ɕω�������
        for (int i = 0; i < rend.materials.Length; i++)  // �����̃}�e���A��������ꍇ�ɑΉ�
        {
            Material material = rend.materials[i];  // �e�}�e���A�����擾
            float currentAlpha = material.GetColor("_Color").a;  // ���݂̃A���t�@�l���擾

            // �ڕW�A���t�@�Ɍ������ăA���t�@�l�����X�ɕύX
            float newAlpha = Mathf.MoveTowards(currentAlpha, targetAlpha, fadeSpeed * Time.deltaTime);

            // �V�����A���t�@�l���g���ĐF���X�V
            Color newColor = material.GetColor("_Color");
            newColor.a = newAlpha;  // �A���t�@�l��ύX
            material.SetColor("_Color", newColor);  // �V�����F���}�e���A���ɐݒ�

            // �������[�h�̏ꍇ�A�u�����h�ݒ��ύX
            if (targetAlpha < 1f)  // �����̏ꍇ
            {
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);  // �\�[�X�̃A���t�@���g�p
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);  // �t�̃A���t�@���g�p
                material.SetInt("_ZWrite", 0);  // ZWrite���I�t�ɂ��āA���ɂ���I�u�W�F�N�g�ɉB��Ȃ��悤�ɂ���
                material.DisableKeyword("_ALPHATEST_ON");  // �A���t�@�e�X�g������
                material.EnableKeyword("_ALPHABLEND_ON");  // �A���t�@�u�����h�L����
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");  // �A���t�@�v���}���`������
                material.renderQueue = 3000;  // �����I�u�W�F�N�g�̃����_�����O������ݒ�
            }
            else  // �s�����̏ꍇ
            {
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);  // �\�[�X�̐F�����̂܂܎g�p
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);  // �ړI�n�̐F�͎g�p���Ȃ�
                material.SetInt("_ZWrite", 1);  // ZWrite���I���ɂ��āA�O�ɂ���I�u�W�F�N�g���B���悤�ɂ���
                material.DisableKeyword("_ALPHATEST_ON");  // �A���t�@�e�X�g������
                material.DisableKeyword("_ALPHABLEND_ON");  // �A���t�@�u�����h������
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");  // �A���t�@�v���}���`������
                material.renderQueue = -1;  // �s�����I�u�W�F�N�g�̃����_�����O�������f�t�H���g�ɖ߂�
            }
        }
    }
}