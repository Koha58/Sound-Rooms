using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player�̕��̓����x��ω�������N���X
/// </summary>
public class PlayerClothesSetMode : MonoBehaviour
{
    Renderer rend;  // Renderer�R���|�[�l���g�̎Q�Ɓi�I�u�W�F�N�g�̃}�e���A����ύX���邽�߂Ɏg�p�j
    private float targetAlpha;  // �ڕW�ƂȂ�A���t�@�l�i�����x�j
    private float fadeSpeed = 2.0f;  // �����x���ω����鑬��

    // Start�͍ŏ��̃t���[���̑O�ɌĂ΂��
    void Start()
    {
        rend = GetComponent<Renderer>();  // ���̃Q�[���I�u�W�F�N�g��Renderer�R���|�[�l���g���擾
        targetAlpha = 0.2f;  // ������Ԃł͓����x��0.2�ɐݒ�i�قړ����j
    }

    // Update�͖��t���[���Ă΂��
    void Update()
    {
        GameObject obj = GameObject.Find("Player");  // Player�I�u�W�F�N�g��T��
        PlayerSeen PS = obj.GetComponent<PlayerSeen>();  // PlayerSeen�X�N���v�g���擾

        // PlayerSeen�X�N���v�g�́uonoff�v�ϐ��̒l�ɂ���ē����x��ύX
        if (PS.onoff == 0)
        {
            targetAlpha = 0.5f;  // �uonoff�v��0(Player������)�Ȃ瓧���x��0.5�ɐݒ�i�������j
        }
        else
        {
            targetAlpha = 1f;  // �uonoff�v��0(Player������)�łȂ��ꍇ�A���S�ɕs�����ɐݒ�i�����x1�j
        }

        // ���ׂẴ}�e���A���ɂ��āA���݂̓����x��ڕW�����x�Ɍ������ď��X�ɕύX����
        for (int i = 0; i < rend.materials.Length; i++)
        {
            Material material = rend.materials[i];  // ���݂̃}�e���A�����擾
            float currentAlpha = material.GetColor("_Color").a;  // ���݂̃A���t�@�i�����x�j���擾

            // �ڕW�����x�Ɍ����ē����x��ω�������
            float newAlpha = Mathf.MoveTowards(currentAlpha, targetAlpha, fadeSpeed * Time.deltaTime);

            // �V�����A���t�@�l�Ń}�e���A���̐F���X�V
            Color newColor = material.GetColor("_Color");
            newColor.a = newAlpha;
            material.SetColor("_Color", newColor);

            // �������[�h�̏ꍇ�A�K�؂ȃu�����h�ݒ��ύX
            if (targetAlpha < 1f)
            {
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);  // �\�[�X�u�����h��ݒ�
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);  // �f�X�e�B�l�[�V�����u�����h��ݒ�
                material.SetInt("_ZWrite", 0);  // ZWrite�i�[�x�������݁j���I�t�ɂ���
                material.DisableKeyword("_ALPHATEST_ON");  // �A���t�@�e�X�g�𖳌��ɂ���
                material.EnableKeyword("_ALPHABLEND_ON");  // �A���t�@�u�����h��L���ɂ���
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");  // �A���t�@�̃v���}���`�v���C�𖳌��ɂ���
                material.renderQueue = 3000;  // �����ȃI�u�W�F�N�g�̃����_�����O����ݒ�i3000�͓����I�u�W�F�N�g�̈�ʓI�Ȓl�j
            }
            else
            {
                // �s�������[�h�̏ꍇ�A�u�����h�ݒ�����ɖ߂�
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);  // �\�[�X�u�����h��ݒ�
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);  // �f�X�e�B�l�[�V�����u�����h��ݒ�
                material.SetInt("_ZWrite", 1);  // ZWrite�i�[�x�������݁j���I���ɂ���
                material.DisableKeyword("_ALPHATEST_ON");  // �A���t�@�e�X�g�𖳌��ɂ���
                material.DisableKeyword("_ALPHABLEND_ON");  // �A���t�@�u�����h�𖳌��ɂ���
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");  // �A���t�@�̃v���}���`�v���C�𖳌��ɂ���
                material.renderQueue = -1;  // �s�����ȃI�u�W�F�N�g�̃����_�����O����ݒ�i�ʏ��-1�j
            }
        }
    }
}