using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGRingSize1 : MonoBehaviour
{
    float i;
    public MeshRenderer Ring;
    public GameObject RingG1;

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine("ScaleUp");
        Ring = GetComponent<MeshRenderer>();
        RingG1.GetComponent<Collider>().enabled = false;//������i�L���j
    }

    // Update is called once per frame
    private void Update()
    {
        GameObject eobjG1 = GameObject.FindWithTag("EnemyG1");
        EnemyGController1 EGC1 = eobjG1.GetComponent<EnemyGController1>(); //Enemy�ɕt���Ă���X�N���v�g���擾

        if (EGC1.ONoff == 0)
        {
            i = 100;
            Ring.enabled = false;
            RingG1.GetComponent<Collider>().enabled = false;//������i�L���j
        }
        if (EGC1.ONoff == 1)
        {
            Ring.enabled = true;
            RingG1.GetComponent<Collider>().enabled =true;//������i�L���j
            //  StartCoroutine("ScaleUp");
        }

    }

    IEnumerator ScaleUp()
    {
        for (i = 150; i < 200; i += 5f)
        {
            this.transform.localScale = new Vector3(i, i, i);
            yield return new WaitForSeconds(0.1f);
        }
    }
}