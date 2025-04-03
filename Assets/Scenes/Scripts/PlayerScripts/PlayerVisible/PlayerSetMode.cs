using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[���f���̓����A�s������Ԃ��Ǘ�����N���X
/// </summary>
public class PlayerSetMode : MonoBehaviour
{
    // �v���C���[��Renderer�R���|�[�l���g
    Renderer rend;
    // �}�e���A���̖ڕW�A���t�@�l�i�����x�j
    private float targetAlpha;
    // �����x���ω����鑬��
    private float fadeSpeed = 2.0f;

    // �萔�̒�`
    private const float INITIAL_ALPHA = 0.2f;  // ������Ԃ̓����x
    private const float VISIBLE_ALPHA = 1f;  // �v���C���[���������Ԃ̓����x�i�s�����j
    private const float INVISIBLE_ALPHA = 0.15f;  // �v���C���[�������Ȃ���Ԃ̓����x
    private const int TRANSPARENT_RENDER_QUEUE = 3000;  // �����I�u�W�F�N�g�̃����_�����O����
    private const int OPAQUE_RENDER_QUEUE = -1;  // �s�����I�u�W�F�N�g�̃����_�����O����
    private const int SRC_BLEND_SRC_ALPHA = (int)UnityEngine.Rendering.BlendMode.SrcAlpha;  // �\�[�X�A���t�@
    private const int DST_BLEND_ONE_MINUS_SRC_ALPHA = (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha;  // �t�̃A���t�@
    private const int SRC_BLEND_ONE = (int)UnityEngine.Rendering.BlendMode.One;  // �\�[�X�̐F�����̂܂܎g�p
    private const int DST_BLEND_ZERO = (int)UnityEngine.Rendering.BlendMode.Zero;  // �ړI�n�̐F�͎g�p���Ȃ�

    // Start�͍ŏ��̃t���[���̑O�ɌĂ΂��
    void Start()
    {
        rend = GetComponent<Renderer>();  // Renderer�R���|�[�l���g���擾
        targetAlpha = INITIAL_ALPHA;  // ������Ԃł͓����i�A���t�@�l0.2�j
    }

    // Update�͖��t���[���Ă΂��
    void Update()
    {
        // "Player"�Ƃ������O�̃I�u�W�F�N�g������
        GameObject obj = GameObject.Find("Player");

        // Player�I�u�W�F�N�g����PlayerSeen�X�N���v�g���擾
        PlayerSeen PS = obj.GetComponent<PlayerSeen>();

        // Player�̉���Ԃɂ���ē����x��ύX
        if (!PS.isVisible)  // �v���C���[�������Ȃ����
        {
            targetAlpha = INVISIBLE_ALPHA;  // �����x��Ⴍ�ݒ�i�A���t�@0.15�j
        }
        else  // �v���C���[����������
        {
            targetAlpha = VISIBLE_ALPHA;  // ���S�ɕs�����ɐݒ�i�A���t�@1�j
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
            if (targetAlpha < VISIBLE_ALPHA)  // �����̏ꍇ
            {
                material.SetInt("_SrcBlend", SRC_BLEND_SRC_ALPHA);  // �\�[�X�̃A���t�@���g�p
                material.SetInt("_DstBlend", DST_BLEND_ONE_MINUS_SRC_ALPHA);  // �t�̃A���t�@���g�p
                material.SetInt("_ZWrite", 0);  // ZWrite���I�t�ɂ��āA���ɂ���I�u�W�F�N�g�ɉB��Ȃ��悤�ɂ���
                material.DisableKeyword("_ALPHATEST_ON");  // �A���t�@�e�X�g������
                material.EnableKeyword("_ALPHABLEND_ON");  // �A���t�@�u�����h�L����
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");  // �A���t�@�v���}���`������
                material.renderQueue = TRANSPARENT_RENDER_QUEUE;  // �����I�u�W�F�N�g�̃����_�����O������ݒ�
            }
            else  // �s�����̏ꍇ
            {
                material.SetInt("_SrcBlend", SRC_BLEND_ONE);  // �\�[�X�̐F�����̂܂܎g�p
                material.SetInt("_DstBlend", DST_BLEND_ZERO);  // �ړI�n�̐F�͎g�p���Ȃ�
                material.SetInt("_ZWrite", 1);  // ZWrite���I���ɂ��āA�O�ɂ���I�u�W�F�N�g���B���悤�ɂ���
                material.DisableKeyword("_ALPHATEST_ON");  // �A���t�@�e�X�g������
                material.DisableKeyword("_ALPHABLEND_ON");  // �A���t�@�u�����h������
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");  // �A���t�@�v���}���`������
                material.renderQueue = OPAQUE_RENDER_QUEUE;  // �s�����I�u�W�F�N�g�̃����_�����O�������f�t�H���g�ɖ߂�
            }
        }
    }
}
