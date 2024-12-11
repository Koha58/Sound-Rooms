using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseUIRight : MonoBehaviour
{
    public Image targetImage; // �_�ł�������Image�R���|�[�l���g
    public Color color1 = Color.red; // �ԐF
    public Color color2 = Color.white; // ���F
    public float blinkDuration = 1.0f; // �_�ł̊Ԋu�i�b�j

    private bool isColor1 = true; // ���݂̐F���
    private float timer = 0f; // �^�C�}�[

    void Update()
    {
        if (targetImage == null) return;

        if (EnemyController1.ImageOn)
        {
            // ���Ԃ��X�V
            timer += Time.deltaTime;

            // �w�肳�ꂽ�Ԋu�𒴂����ꍇ�ɐF��؂�ւ���
            if (timer >= blinkDuration)
            {
                isColor1 = !isColor1; // �F��Ԃ�؂�ւ���
                targetImage.color = isColor1 ? color1 : color2; // �F��ύX
                timer = 0f; // �^�C�}�[�����Z�b�g
            }
        }
    }
}
