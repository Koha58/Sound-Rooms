using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public class NextPointer : MonoBehaviour
{
    public GameObject NextPoint;
    Vector3 Pointers;
    float X=90f;
    float Z=-90;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    private void Update()
    {
        GameObject eobj = GameObject.FindWithTag("Enemy");
        EnemyFailurework EF = eobj.GetComponent<EnemyFailurework>(); //EnemyFailurework付いているスクリプトを取得

        GameObject  Point = GameObject.FindWithTag("Pointer");
        Pointer P= Point.GetComponent<Pointer>(); //EnemyFailurework付いているスクリプトを取得

        if (P.Nextpoint == true)
        {
            Pointers = new Vector3(X-10, 0, Z+10);
      
            NextPoint.transform.position = Pointers;
            Instantiate(NextPoint, Pointers, Quaternion.identity);
            if (EF.PatrolPoints[0] == null)
            {
                EF.PatrolPoints[0] = NextPoint.transform;
                EF.PatrolPoints[0] = EF.PatrolPoints[0];
                Instantiate(NextPoint, Pointers, Quaternion.identity);
            }
            else if (EF.PatrolPoints[1] == null)
            {
                EF.PatrolPoints[1] = NextPoint.transform;
                EF.PatrolPoints[1] = EF.PatrolPoints[1];
                Instantiate(NextPoint, Pointers, Quaternion.identity);
            }
            else if (EF.PatrolPoints[2] == null)
            {
                EF.PatrolPoints[2] = NextPoint.transform;
                EF.PatrolPoints[2] = EF.PatrolPoints[2];
                Instantiate(NextPoint, Pointers, Quaternion.identity);
            }
            else if (EF.PatrolPoints[3] == null)
            {
                EF.PatrolPoints[3] = NextPoint.transform;
                EF.PatrolPoints[3] = EF.PatrolPoints[3];
                Instantiate(NextPoint, Pointers, Quaternion.identity);
            }
            else if (EF.PatrolPoints[4] == null)
            {
                EF.PatrolPoints[4] = NextPoint.transform;
                EF.PatrolPoints[4] = EF.PatrolPoints[4];
                Instantiate(NextPoint, Pointers, Quaternion.identity);
            }
        }
    }
}
