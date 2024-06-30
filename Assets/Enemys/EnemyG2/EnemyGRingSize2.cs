using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGRingSize2 : MonoBehaviour
{
    float i;
    public MeshRenderer Ring;
    public GameObject RingG2;

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine("ScaleUp");
        Ring = GetComponent<MeshRenderer>();
        RingG2.GetComponent<Collider>().enabled = false;//������i�L���j
    }

    // Update is called once per frame
    private void Update()
    {
        GameObject eobjG2 = GameObject.FindWithTag("EnemyG2");
        EnemyGController2 EGC2 = eobjG2.GetComponent<EnemyGController2>(); //Enemy�ɕt���Ă���X�N���v�g���擾

        if (EGC2.ONoff == 0)
        {
            i = 100;
            Ring.enabled = false;
            RingG2.GetComponent<Collider>().enabled = false;//������i�L���j
        }
        if (EGC2.ONoff == 1)
        {
            Ring.enabled = true;
            RingG2.GetComponent<Collider>().enabled = false;//������i�L���j
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
