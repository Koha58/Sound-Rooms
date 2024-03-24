using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySeen : MonoBehaviour
{
    SkinnedMeshRenderer MR;
    GameObject Eneny;
    static public int ONoff = 0;//(0‚ªŒ©‚¦‚È‚¢G‚P‚ªŒ©‚¦‚éó‘Ôj
    private float Seetime;  //Œo‰ßŠÔ
    private float SoundTime;

    // [SerializeField] GameObject Sphere;

    // Start is called before the first frame update
    void Start()
    {
        MR = GetComponent<SkinnedMeshRenderer>();
        MR.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (ONoff == 0)//Œ©‚¦‚È‚¢‚Æ‚«
        {
            SoundTime += Time.deltaTime;
            if (SoundTime > 10.0f)
            {
                MR.enabled = true;
                ONoff = 1;
                SoundTime = 0.0f;
                
            }
        }
        else if (ONoff == 1)//Œ©‚¦‚Ä‚¢‚é‚Æ‚«
        {
            Seetime += Time.deltaTime;
            if (Seetime >= 10.0f)
            {
                MR.enabled = false;
                ONoff = 0;
                Seetime = 0.0f;
               
            }
        }
    }
}
