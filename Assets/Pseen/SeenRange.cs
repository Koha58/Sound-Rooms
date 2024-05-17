using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeenRange : MonoBehaviour
{
    private float originSizeX = 3.0f;
    private float originSizeY = 2.0f;
    private float originSizeZ = 2.0f;
    ButtonHoldDown BD;

    // Start is called before the first frame update
    void Start()
    {
        
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
            else
            {

            }
            /*
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
            }*/
        }
    }
}
