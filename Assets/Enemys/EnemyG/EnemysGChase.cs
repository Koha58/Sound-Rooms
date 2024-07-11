using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemysGChase : MonoBehaviour
{
    public Transform Player;//プレイヤーを参照
    public bool GChase = false;
    float GChaseTime;
    public bool ViG;
    float ViGTime;

    // Start is called before the first frame update
    private void Start()
    {
        GChase = false;
        ViG = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if (GChase == true)
        {
            GChaseTime += Time.deltaTime;
            if (GChaseTime > 25.0f)
            {
                GChase = false;
                GChaseTime = 0f;
            }
        }
        if (ViG == false)
        {
            ViGTime += Time.deltaTime;
            if (ViGTime > 0.5f)
            {
                ViG = true;
                ViGTime = 0.0f;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GChase = true;
        }

        if (other.gameObject.CompareTag("InWall"))
        {
            ViG = false;
        }

        if (other.gameObject.CompareTag("Wall"))
        {
            ViG = false;
        }
    }
}
