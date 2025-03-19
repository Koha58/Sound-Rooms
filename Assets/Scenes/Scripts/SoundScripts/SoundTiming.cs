using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player�̉��g�G�t�F�N�g���Ǘ�����N���X
/// </summary>
public class SoundTiming : MonoBehaviour
{
    // �p�[�e�B�N���V�X�e���̎Q�Ɓi���ɍ��킹�ăp�[�e�B�N���𐧌�j
    ParticleSystem SoundParticle;

    // LevelMeter�X�N���v�g�̎Q�Ɓi���̑傫�����擾���邽�߁j
    LevelMeter levelMeter;

    void Start()
    {
        // "SoundVolume"�Ƃ������O��GameObject���V�[������擾
        GameObject soundobj = GameObject.Find("SoundVolume");

        // LevelMeter�R���|�[�l���g���擾�i���ʏ����擾���邽�߁j
        levelMeter = soundobj.GetComponent<LevelMeter>(); // LevelMeter�X�N���v�g���擾

        // "SoundParticle"�Ƃ������O��GameObject���V�[������擾
        GameObject SoundEffect = GameObject.Find("SoundParticle");

        // ParticleSystem�R���|�[�l���g���擾�i�p�[�e�B�N���𐧌䂷�邽�߁j
        SoundParticle = SoundEffect.GetComponent<ParticleSystem>();

        // ������Ԃł̓p�[�e�B�N�����~
        SoundParticle.Stop();
    }

    void Update()
    {
        // "SoundVolume"�Ƃ������O��GameObject���ēx�擾
        GameObject soundobj = GameObject.Find("SoundVolume");

        // LevelMeter�R���|�[�l���g���ēx�擾�i���ʏ�񂪕ϓ�����\�������邽�ߖ��t���[���m�F�j
        levelMeter = soundobj.GetComponent<LevelMeter>();

        // "SoundParticle"�Ƃ������O��GameObject���ēx�擾
        GameObject SoundEffect = GameObject.Find("SoundParticle");

        // ParticleSystem�R���|�[�l���g���ēx�擾
        SoundParticle = SoundEffect.GetComponent<ParticleSystem>();

        // ���̑傫���idB�j�Ɋ�Â��ăp�[�e�B�N���̏�Ԃ𐧌�
        if (levelMeter.nowdB > 0.0f) // ���ʂ�0�ȏ�̏ꍇ
        {
            // �p�[�e�B�N�����Đ����łȂ���΍Đ��J�n
            if (!SoundParticle.isPlaying)
            {
                SoundParticle.Play();
            }
        }
        else // ���ʂ�0�����̏ꍇ
        {
            // �p�[�e�B�N�����Đ����ł���Β�~
            if (SoundParticle.isPlaying)
            {
                SoundParticle.Stop();
            }
        }
    }
}