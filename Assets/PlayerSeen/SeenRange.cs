using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//SeenAreaのサイズ変更
public class SeenRange : MonoBehaviour
{
    private float originSizeX = 2.3f;
    private float originSizeY = 2.0f;
    private float originSizeZ = 2.0f;
    private float originSizeSX = 3.3f;
    private float originSizeSY = 2.0f;
    private float originSizeSZ = 2.0f;
    private float originSizeMX = 4.3f;
    private float originSizeMY = 2.0f;
    private float originSizeMZ = 2.0f;
    private float originSizeLX = 5.3f;
    private float originSizeLY = 2.0f;
    private float originSizeLZ = 2.0f;
    // 変更前の値
    private int preHeight;
    ButtonHoldDown BD;
   

    // Start is called before the first frame update
    void Start()
    {
        preHeight = 0;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject hobj = GameObject.Find("GaugeManager");
        BD = hobj.GetComponent<ButtonHoldDown>(); //付いているスクリプトを取得

        if (preHeight != Enemyincrease.enemyDeathcnt)
        {
            originSizeX += 1.0f;
            originSizeSX += 1.0f;
            originSizeMX += 2.0f;
            originSizeLX += 3.0f;

            preHeight++;
        }

        if(BD.boundHeight == 0)
        {
            transform.localScale = new Vector3(originSizeX, originSizeY, originSizeZ);
        }
        else if (BD.boundHeight == 1)
        {
            transform.localScale = new Vector3(originSizeSX, originSizeSY, originSizeSZ);
        }
        else if (BD.boundHeight == 2)
        {
            transform.localScale = new Vector3(originSizeMX, originSizeMY, originSizeMZ);
        }
        else
        {
            transform.localScale = new Vector3(originSizeLX, originSizeLY, originSizeLZ);
        }
    }
}
