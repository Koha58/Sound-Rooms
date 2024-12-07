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
        levelMeter = soundobj.GetComponent<LevelMeter>(); //付いているスクリプトを取得
        GameObject SoundEffect = GameObject.Find("SoundParticle");
        SoundParticle = SoundEffect.GetComponent<ParticleSystem>();
        SoundParticle.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject soundobj = GameObject.Find("SoundVolume");
        levelMeter = soundobj.GetComponent<LevelMeter>(); //付いているスクリプトを取得
        GameObject SoundEffect = GameObject.Find("SoundParticle");
        SoundParticle = SoundEffect.GetComponent<ParticleSystem>();

        // 音の大きさに応じてパーティクルの状態を制御
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
