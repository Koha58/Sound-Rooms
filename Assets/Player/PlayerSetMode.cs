using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Player�̓����A�s�����̏�ԕω�
public class PlayerSetMode : MonoBehaviour
{
    Renderer rend;
    private float targetAlpha; // �}�e���A���̖ڕW�A���t�@�l
    private float fadeSpeed = 2.0f; // �����x���ω����鑬��

    // Start�͍ŏ��̃t���[���̑O�ɌĂ΂��
    void Start()
    {
        rend = GetComponent<Renderer>(); // Renderer�R���|�[�l���g���擾
        targetAlpha = 0.2f; // ������Ԃł͓���
    }

    // Update�͖��t���[���Ă΂��
    void Update()
    {
        GameObject obj = GameObject.Find("Player");  // Player�I�u�W�F�N�g��T��
        PlayerSeen PS = obj.GetComponent<PlayerSeen>();  // PlayerSeen�X�N���v�g���擾

        if (PS.onoff == 0)
        {
            targetAlpha = 0.2f; // �����ɂ���i�A���t�@0.05�j
        }
        else
        {
            targetAlpha = 1f; // �s�����ɖ߂��i�A���t�@1�j
        }

        // �����x��ڕW�l�Ɍ������ď��X�ɕύX����
        for (int i = 0; i < rend.materials.Length; i++)
        {
            Material material = rend.materials[i];
            float currentAlpha = material.GetColor("_Color").a; // ���݂̃A���t�@�l���擾

            // �ڕW�A���t�@�Ɍ������ď��X�ɕω�������
            float newAlpha = Mathf.MoveTowards(currentAlpha, targetAlpha, fadeSpeed * Time.deltaTime);

            // �V�����A���t�@�l�ŐF���X�V
            Color newColor = material.GetColor("_Color");
            newColor.a = newAlpha;
            material.SetColor("_Color", newColor);

            // �������[�h�̏ꍇ�A�u�����h�ݒ��ύX
            if (targetAlpha < 1f)
            {
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                material.SetInt("_ZWrite", 0); // ZWrite���I�t�ɂ���
                material.DisableKeyword("_ALPHATEST_ON");
                material.EnableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = 3000; // �����x�̃����_�����O����ݒ�
            }
            else
            {
                // �s�������[�h�̏ꍇ�A�u�����h�ݒ��߂�
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                material.SetInt("_ZWrite", 1); // ZWrite���I���ɂ���
                material.DisableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = -1; // �s�����̃����_�����O����ݒ�
            }
        }
    }
}
