using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeenRange : MonoBehaviour
{
    private float originSizeX = 2.8f;
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
        if (BD.boundHeight == 0)
        {
            this.transform.localScale = new Vector3(originSizeX, originSizeY, originSizeZ);
        }
        else if (BD.boundHeight == 1)
        {
            this.transform.localScale = new Vector3(originSizeX+ 0.6f, originSizeY, originSizeZ+0.4f);
        }
        else if (BD.boundHeight == 2)
        {
            this.transform.localScale = new Vector3(originSizeX +1.2f, originSizeY, originSizeZ+1.0f);
        }
        else if (BD.boundHeight == 3)
        {
            this.transform.localScale = new Vector3(originSizeX+2.0f, originSizeY, originSizeZ+2.3f);
        }
        else if (BD.boundHeight == 4)
        {
            this.transform.localScale = new Vector3(originSizeX + 2.9f, originSizeY, originSizeZ);
        }
        else if (BD.boundHeight == 5)
        {
            this.transform.localScale = new Vector3(originSizeX + 2.9f, originSizeY, originSizeZ);
        }
        else if (BD.boundHeight == 6)
        {
            this.transform.localScale = new Vector3(originSizeX + 2.9f, originSizeY, originSizeZ);
        }
        else if (BD.boundHeight == 7)
        {
            this.transform.localScale = new Vector3(originSizeX + 2.9f, originSizeY, originSizeZ);
        }
    }
}
