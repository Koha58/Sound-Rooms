using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroySize : MonoBehaviour
{
    float i;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("ScaleUp");
    }

    // Update is called once per frame
    void Update()
    {

        GameObject eobj = GameObject.FindWithTag("Enemy");
        EnemyFailurework EF = eobj.GetComponent<EnemyFailurework>(); //Enemy�ɕt���Ă���X�N���v�g���擾
        if(EF.ONoff == 1)
        {
            StartCoroutine("ScaleUp");
        }

    }

    IEnumerator ScaleUp()
    {
        for (i = 50; i < 200; i += 1f)
        {
            this.transform.localScale = new Vector3(i, i, i);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
