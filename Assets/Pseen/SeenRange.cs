using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//SeenAreaのサイズ変更
public class SeenRange : MonoBehaviour
{
    private float originSizeX = 3.3f;
    private float originSizeY = 2.0f;
    private float originSizeZ = 2.0f;
    // 変更前の値
    private int preHeight;
    ButtonHoldDown BD;
    private int low = 0;
    private int middle = 0;
    private int high = 0;

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
        
        for (int boundSize = 0; boundSize <= BD.boundHeight; boundSize++)
        {
            if (BD.boundHeight == 0)
            {
                transform.localScale = new Vector3(originSizeX, originSizeY, originSizeZ);
            }          
            else if (BD.boundHeight == 1)
            {
                transform.localScale = new Vector3(originSizeX + 0.6f, originSizeY, originSizeZ + 0.4f);
            }
            else if (BD.boundHeight == 2)
            {
                transform.localScale = new Vector3(originSizeX + 1.2f, originSizeY, originSizeZ + 1.0f);
            }
            else if (BD.boundHeight == 3)
            {
                transform.localScale = new Vector3(originSizeX + 2.0f, originSizeY, originSizeZ + 2.3f);
            }
        }
        /*
        if(preHeight != Enemyincrease.enemyDeathcnt)
        {
            if(BD.boundHeight == 1)
            {
                if(low == 0)
                {
                    low++;
                    transform.localScale = new Vector3(originSizeX + 1.0f, originSizeY, originSizeZ);
                    originSizeX += 1.0f;
                }
                transform.localScale = new Vector3(originSizeX, originSizeY, originSizeZ);
            }
            else if (BD.boundHeight == 2)
            {
                if(low == 1)
                {
                    originSizeX -= 1.0f;
                    low--;
                }
                if(middle == 0)
                {
                    middle++;
                    transform.localScale = new Vector3(originSizeX + 2.0f, originSizeY, originSizeZ);
                    originSizeX += 2.0f;
                }
                transform.localScale = new Vector3(originSizeX + 2.0f, originSizeY, originSizeZ);
            }
            else
            {
                transform.localScale = new Vector3(originSizeX + 3.0f, originSizeY, originSizeZ);
            }
        }*/
    }
}
