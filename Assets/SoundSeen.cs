using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SoundSeen : MonoBehaviour
{
    MeshRenderer spmr;


    void Start()
    {
        //Å‰‚ÍŒ©‚¦‚È‚¢ó‘Ô
        spmr = GetComponent<MeshRenderer>();
        spmr.enabled = false; //Œ©‚¦‚È‚¢i–³Œøj
    }
}
