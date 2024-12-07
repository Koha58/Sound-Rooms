using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTiming : MonoBehaviour
{
    ParticleSystem SoundParticle;
    LevelMeter levelMeter;

    // Start is called before the first frame update
    void Start()
    {
        SoundParticle = GetComponent<ParticleSystem>();
        GameObject soundobj = GameObject.Find("SoundVolume");
        levelMeter = soundobj.GetComponent<LevelMeter>(); //�t���Ă���X�N���v�g���擾
        GameObject SoundEffect = GameObject.Find("SoundParticle");
        SoundParticle = SoundEffect.GetComponent<ParticleSystem>();
        SoundParticle.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject soundobj = GameObject.Find("SoundVolume");
        levelMeter = soundobj.GetComponent<LevelMeter>(); //�t���Ă���X�N���v�g���擾
        GameObject SoundEffect = GameObject.Find("SoundParticle");
        SoundParticle = SoundEffect.GetComponent<ParticleSystem>();

        // ���̑傫���ɉ����ăp�[�e�B�N���̏�Ԃ𐧌�
        if (levelMeter.nowdB > 0.0f)
        {
            if (!SoundParticle.isPlaying)
            {
                SoundParticle.Play();
            }
        }
        else
        {
            if (SoundParticle.isPlaying)
            {
                SoundParticle.Stop();
            }
        }
    }
}
