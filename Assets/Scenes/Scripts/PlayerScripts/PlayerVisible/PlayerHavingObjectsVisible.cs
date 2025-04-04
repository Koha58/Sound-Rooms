using UnityEngine;

/// <summary>
/// Player�������Ă��镨�̓����x��ω�������N���X
/// </summary>
public class PlayerHavingObjectsVisible : MonoBehaviour
{
    // �萔�̒�`

    /// <summary>
    /// ���������x�i�A���t�@�l�j
    /// </summary>
    private const float INITIAL_ALPHA = 0.3f;

    /// <summary>
    /// ���S�s�����i�A���t�@�l�j
    /// </summary>
    private const float FULL_ALPHA = 1f;

    /// <summary>
    /// �����x�̕ω����x�i�����x�̑������ǂꂾ�������s�����j
    /// </summary>
    private const float FADE_SPEED = 2.0f;

    // �V�F�[�_�[�ݒ�p�̒萔

    /// <summary>
    /// _SrcBlend �Ɏw�肷��l: �\�[�X�̃A���t�@�l���g��
    /// </summary>
    private const int SRC_BLEND_SRC_ALPHA = (int)UnityEngine.Rendering.BlendMode.SrcAlpha;

    /// <summary>
    /// _DstBlend �Ɏw�肷��l: �\�[�X�̃A���t�@�̋t���g��
    /// </summary>
    private const int DST_BLEND_ONE_MINUS_SRC_ALPHA = (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha;

    /// <summary>
    /// ZWrite���I�t�ɂ��邽�߂̐ݒ�
    /// </summary>
    private const int ZWRITE_OFF = 0;

    // �v���C���[��Renderer�R���|�[�l���g
    private Renderer rend;

    // �}�e���A���̖ڕW�A���t�@�l�i�����x�j
    private float targetAlpha;

    // Start�͍ŏ��̃t���[���̑O�ɌĂ΂��
    void Start()
    {
        rend = GetComponent<Renderer>();  // Renderer�R���|�[�l���g���擾
        if (rend != null)
        {
            // �����V�F�[�_�[�̐ݒ�
            rend.material.SetInt("_SrcBlend", SRC_BLEND_SRC_ALPHA);
            rend.material.SetInt("_DstBlend", DST_BLEND_ONE_MINUS_SRC_ALPHA);
            rend.material.SetInt("_ZWrite", ZWRITE_OFF);  // ZWrite���I�t
            rend.material.EnableKeyword("_ALPHABLEND_ON");  // �A���t�@�u�����h�L����
        }
        targetAlpha = INITIAL_ALPHA;  // ������Ԃł͓����i�A���t�@�l0.3�j
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
            targetAlpha = INITIAL_ALPHA;  // �����x��ݒ�i�A���t�@0.3�j
        }
        else  // �v���C���[����������
        {
            targetAlpha = FULL_ALPHA;  // ���S�ɕs�����ɐݒ�i�A���t�@1�j
        }

        // �����x��ڕW�A���t�@�l�Ɍ������ď��X�ɕω�������
        Color color = rend.material.color;
        color.a = Mathf.MoveTowards(color.a, targetAlpha, FADE_SPEED * Time.deltaTime);
        rend.material.color = color;  // �F���X�V
    }
}
