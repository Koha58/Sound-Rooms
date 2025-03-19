using UnityEngine;
using Cinemachine;
#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// Cinemachine�̃J�����V�X�e���ɂ����āA�w�肳�ꂽ�����Œ肷��g���@�\
/// </summary>
[ExecuteInEditMode]  // �ҏW���[�h�ł����s�����悤�ɂ���
[SaveDuringPlay]    // �v���C���ł��ύX��ۑ�
[AddComponentMenu("")] // ���j���[�ɕ\�����Ȃ��悤�ɂ���i��\���j
public class YAxisLock : CinemachineExtension
{
    // X, Y, Z���̃��b�N��Ԃ�ێ�����t���O
    public bool x_islocked, y_islocked, z_islocked;
    // ���b�N����ʒu�i�ŏI�I�ɃJ�����̈ʒu�����̍��W�ɌŒ肳���j
    public Vector3 lockPosition;

    // Cinemachine�̃p�C�v���C�����̊e�X�e�[�W���Ă΂ꂽ��Ɏ��s�����R�[���o�b�N
    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        // �J�����̃{�f�B�����i�ʒu�j�ɑ΂��Ă̂ݏ������s��
        if (stage == CinemachineCore.Stage.Body)
        {
            var newPos = state.RawPosition;  // ���݂̃J�����ʒu���擾
            // �e���ɑ΂��ă��b�N����ꍇ�A�w�肵���ʒu�ɕύX
            if (x_islocked) newPos.x = lockPosition.x;
            if (y_islocked) newPos.y = lockPosition.y;
            if (z_islocked) newPos.z = lockPosition.z;
            state.RawPosition = newPos;  // �J�����ʒu���X�V
        }
    }
}

#if UNITY_EDITOR
// Unity�G�f�B�^�ł̃C���X�y�N�^�[�\�����J�X�^�}�C�Y
[CustomEditor(typeof(YAxisLock))]
public class LockAxisCameraEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // YAxisLock�R���|�[�l���g��ΏۂɃC���X�y�N�^�[GUI��`��
        var lockAxisCamera = target as YAxisLock;

        // ���̃��b�N��I�����邽�߂�UI��\��
        using (new EditorGUILayout.HorizontalScope())
        {
            EditorGUIUtility.labelWidth = 10;  // ���x���̕��𒲐�
            EditorGUILayout.LabelField("�Œ肷�鎲");
            lockAxisCamera.x_islocked = EditorGUILayout.Toggle("X", lockAxisCamera.x_islocked);  // X���̃��b�N
            lockAxisCamera.y_islocked = EditorGUILayout.Toggle("Y", lockAxisCamera.y_islocked);  // Y���̃��b�N
            lockAxisCamera.z_islocked = EditorGUILayout.Toggle("Z", lockAxisCamera.z_islocked);  // Z���̃��b�N
        }

        // �Œ肷����W�i�ʒu�j�̐ݒ��\��
        EditorGUILayout.LabelField("�Œ肷����W");
        using (new EditorGUILayout.HorizontalScope())
        {
            EditorGUIUtility.labelWidth = 10;  // ���x���̕��𒲐�
            lockAxisCamera.lockPosition.x = EditorGUILayout.FloatField("X", lockAxisCamera.lockPosition.x);  // X���W
            lockAxisCamera.lockPosition.y = EditorGUILayout.FloatField("Y", lockAxisCamera.lockPosition.y);  // Y���W
            lockAxisCamera.lockPosition.z = EditorGUILayout.FloatField("Z", lockAxisCamera.lockPosition.z);  // Z���W
        }
    }
}
#endif