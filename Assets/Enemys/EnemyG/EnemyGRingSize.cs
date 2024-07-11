using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGRingSize : MonoBehaviour
{
    float i;
    public MeshRenderer Ring;
    public GameObject RingG;

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine("ScaleUp");
        Ring = GetComponent<MeshRenderer>();
        RingG.GetComponent<Collider>().enabled = false;//������i�L���j
    }

    // Update is called once per frame
    private void Update()
    {
        GameObject eobjG = GameObject.FindWithTag("EnemyG");
        EnemyGController EGC = eobjG.GetComponent<EnemyGController>(); //Enemy�ɕt���Ă���X�N���v�g���擾

        if (EGC.ONoff == 0)
        {
            i = 50;
            Ring.enabled = false;
            RingG.GetComponent<Collider>().enabled = false;//������i�L���j
        }
        if (EGC.ONoff == 1)
        {
            Ring.enabled = true;
            RingG.GetComponent<Collider>().enabled = true;//������i�L���j
            //  StartCoroutine("ScaleUp");
        }

    }

    IEnumerator ScaleUp()
    {
        for (i = 50; i < 200; i += 5f)
        {
            this.transform.localScale = new Vector3(i, i, i);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
