using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRingSize4 : MonoBehaviour
{
    float i;
    public MeshRenderer Ring;
    public GameObject Ring1;

    // Start is called before the first frame update
    private void Start()
    {
        GameObject eobj4 = GameObject.FindWithTag("Enemy4");
        EnemyController4 EC4 = eobj4.GetComponent<EnemyController4>(); //Enemy�ɕt���Ă���X�N���v�g���擾
        EC4.ONoff = 0;
        StartCoroutine("ScaleUp");
        Ring = GetComponent<MeshRenderer>();
        Ring1.GetComponent<Collider>().enabled = false;//������i�L���j
    }

    // Update is called once per frame
    private void Update()
    {
        GameObject eobj4 = GameObject.FindWithTag("Enemy4");
        EnemyController4 EC4 = eobj4.GetComponent<EnemyController4>(); //Enemy�ɕt���Ă���X�N���v�g���擾

        if (EC4.ONoff == 0)
        {
            i = 200;
            Ring.enabled = false;
            Ring1.GetComponent<Collider>().enabled = false;//������i�L���j
        }
        if (EC4.ONoff == 1)
        {
            Ring.enabled = true;
            Ring1.GetComponent<Collider>().enabled = true;//������i�L���j
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
