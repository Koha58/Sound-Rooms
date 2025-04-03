using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player�̉����̓����x��ω�������N���X
/// </summary>
public class UnderwareSet : MonoBehaviour
{
    Renderer rend;

    // �萔�̒�`
    private const float FULL_ALPHA = 1f;  // ���S�ɕs�����ȏ�ԁi�A���t�@�l1�j
    private const float TRANSPARENT_ALPHA = 0f;  // ���S�ɓ����ȏ�ԁi�A���t�@�l0�j
    private const int RENDER_QUEUE_TRANSPARENT = 3000;  // �����I�u�W�F�N�g�̃����_�����O��
    private const int RENDER_QUEUE_OPAQUE = -1;  // �s�����I�u�W�F�N�g�̃����_�����O��
    private const int SRC_BLEND_SRC_ALPHA = (int)UnityEngine.Rendering.BlendMode.SrcAlpha;  // �\�[�X�A���t�@
    private const int DST_BLEND_ONE_MINUS_SRC_ALPHA = (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha;  // �t�̃A���t�@
    private const int SRC_BLEND_ONE = (int)UnityEngine.Rendering.BlendMode.One;  // �\�[�X�̐F�����̂܂܎g�p
    private const int DST_BLEND_ZERO = (int)UnityEngine.Rendering.BlendMode.Zero;  // �ړI�n�̐F�͎g�p���Ȃ�
    private const int ZWRITE_ON = 1;  // ZWrite�i�[�x�������݁j���I���ɂ���
    private const int ZWRITE_OFF = 0;  // ZWrite�i�[�x�������݁j���I�t�ɂ���

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();  // Renderer�R���|�[�l���g���擾
    }

    // Update is called once per frame
    void Update()
    {
        GameObject obj = GameObject.Find("Player");  // Player�I�u�W�F�N�g��T��
        PlayerSeen PS = obj.GetComponent<PlayerSeen>();  // PlayerSeen�X�N���v�g���擾

        if (rend != null && !PS.isVisible)
        {
            // �v���C���[�������Ȃ��ꍇ�A�����ɐݒ�
            SetMaterialTransparency(TRANSPARENT_ALPHA, RENDER_QUEUE_TRANSPARENT, SRC_BLEND_SRC_ALPHA, DST_BLEND_ONE_MINUS_SRC_ALPHA, ZWRITE_OFF);
        }
        else
        {
            // �v���C���[��������ꍇ�A�s�����ɐݒ�
            SetMaterialTransparency(FULL_ALPHA, RENDER_QUEUE_OPAQUE, SRC_BLEND_ONE, DST_BLEND_ZERO, ZWRITE_ON);
        }
    }

    // �}�e���A���̓����x��u�����h�ݒ��ύX���郁�\�b�h
    private void SetMaterialTransparency(float alpha, int renderQueue, int srcBlend, int dstBlend, int zWrite)
    {
        for (int i = 0; i < rend.materials.Length; i++)
        {
            Material material = rend.materials[i];

            // �u�����h�ݒ��ύX
            material.SetInt("_SrcBlend", srcBlend);
            material.SetInt("_DstBlend", dstBlend);
            material.SetInt("_ZWrite", zWrite);
            material.DisableKeyword("_ALPHATEST_ON");
            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            material.renderQueue = renderQueue;

            // �����x��ݒ�
            material.SetColor("_Color", new Color(1, 1, 1, alpha));  // �A���t�@�l��ݒ�
            if (alpha < FULL_ALPHA)
            {
                material.EnableKeyword("_ALPHABLEND_ON");  // ������ԂȂ�A���t�@�u�����h��L����
            }
            else
            {
                material.DisableKeyword("_ALPHABLEND_ON");  // �s������ԂȂ�A���t�@�u�����h�𖳌���
            }
        }
    }
}
