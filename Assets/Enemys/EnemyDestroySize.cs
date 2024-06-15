using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroySize : MonoBehaviour
{
     float i;
     MeshRenderer Ring;
    // Start is called before the first frame update
    void Start()
    {
      //  StartCoroutine("ScaleUp");
        Ring=GetComponent<MeshRenderer>();
     
    }

    // Update is called once per frame
    private void Update()
    {
        /*
        GameObject eobj = GameObject.FindWithTag("Enemy");
        Enemy E = eobj.GetComponent<Enemy>(); //Enemy‚É•t‚¢‚Ä‚¢‚éƒXƒNƒŠƒvƒg‚ðŽæ“¾
        if (E.ONoff == 0)
        {
            i = 0;
           // Ring.enabled = false;
        }
        if (E.ONoff == 1)
        {
<<<<<<< HEAD
           // StartCoroutine("ScaleUp");
           // Ring.enabled = true;
=======
            StartCoroutine("ScaleUp");
>>>>>>> Enemy2.0.7.ï¼’
        }
        */
        GameObject eobj = GameObject.FindWithTag("Enemy");
        EnemyController EC = eobj.GetComponent<EnemyController>(); //Enemy‚É•t‚¢‚Ä‚¢‚éƒXƒNƒŠƒvƒg‚ðŽæ“¾
        if (EC.ONoff == 0)
        {
            i = 0;
            // Ring.enabled = false;
        }
        if (EC.ONoff == 1)
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
