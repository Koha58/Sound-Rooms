using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class NextPointer : MonoBehaviour
{
    public GameObject NextPoint;
    public Vector3 Pointer;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        GameObject eobj = GameObject.FindWithTag("Enemy");
        EnemyFailurework EF = eobj.GetComponent<EnemyFailurework>(); //EnemyFailurework付いているスクリプトを取得

       
            for (float X = 90; X == -90; X -= 10)
            {
                for (float Z = -90; Z == 90; Z += 10)
                {
                   Pointer = new Vector3(X, 0, Z);
                }
            }
            Instantiate(NextPoint, Pointer, Quaternion.identity);
            NextPoint.transform.position = Pointer;
            EF.PatrolPoints[EF.CurrentPointIndex] = NextPoint.transform;
            EF.PatrolPoints[EF.CurrentPointIndex] = EF.PatrolPoints[EF.CurrentPointIndex++];
        
    }
}
