using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour
{
    [SerializeField] AudioSource BGM;

    // Start is called before the first frame update
    void Start()
    {
        BGM = GetComponent<AudioSource>();
        InvokeRepeating("PlaySound", 0f, 10f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlaySound()
    {
        BGM.Play();
    }
}
