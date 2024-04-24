using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySound1 : MonoBehaviour
{
    public AudioClip clip;
    AudioSource source;
    EnemySeen ES;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        GameObject eobj1 = GameObject.FindWithTag("Enemy1");
        ES = eobj1.GetComponent<EnemySeen>(); //�t���Ă���X�N���v�g���擾

        if (ES.ONoff == 1)
        {
            source.PlayOneShot(clip);
        }
    }
}
