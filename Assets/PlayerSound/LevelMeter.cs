using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMeter : MonoBehaviour
{
    //�X�V����Ώۂ�levelMeter(uGUI Image)
    Image levelMeterImage = null;

    //����dB��levelMeter�\���̉����ɓ��B����
    [SerializeField]
    private float dB_Min = -60.0f;

    //����dB��levelMeter�\���̏���ɓ��B����
    [SerializeField]
    private float dB_Max = -0.0f;

    //dB���擾����Ώۂ�micAudioSource
    [SerializeField]
    private MicAudioSource micAS = null;

    public float nowdB;

    void Awake()
    {
        //�X�V����Ώۂ�Image���擾
        levelMeterImage = GetComponent<Image>();
    }

    void Update()
    {
        //dB�l����levelMeterImage�p��fillAount�̒l�ɕϊ�
        float fillAmountValue = dB_ToFillAmountValue(micAS.now_dB);
        if (Input.GetMouseButton(0))
        {
            //fillAmount�l�X�V
            this.levelMeterImage.fillAmount = fillAmountValue;
            nowdB = fillAmountValue;
        }
        else
        {
            //fillAmount�l�X�V
            this.levelMeterImage.fillAmount = 0.0f;
            nowdB = 0.0f;
        }
    }

    /// <summary>
    /// dB_Min��db_Max�Ɋ�Â���dB��fillAmount�l�ɕϊ�
    /// </summary>
    /// <param name="dB">dB�l</param>
    /// <returns>fillAmount�l</returns>
    float dB_ToFillAmountValue(float dB)
    {
        //���͂��ꂽdB��dB_Max��dBMin�l�Ő؂�̂�
        float modified_dB = dB;
        if (modified_dB > dB_Max) { modified_dB = dB_Max; }
        else if (modified_dB < dB_Min) { modified_dB = dB_Min; }

        //fillAmount�l�ɕϊ�(dB_Min=0.0f, dB_Max=1.0f)
        float fillAountValue = 1.0f + (modified_dB / (dB_Max - dB_Min));
        return fillAountValue;
    }

}
