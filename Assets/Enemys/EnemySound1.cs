using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySound1 : MonoBehaviour
{
    public AudioClip clip;
    AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // マウスクリックで再生
            source.PlayOneShot(clip);
        }
    }
}
