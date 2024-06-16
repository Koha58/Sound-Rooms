using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public Transform Player;//プレイヤーを参照
    public  bool Chase=false;
    private float Chaseonoff;
    public bool Wall=false;
    private float Wallonoff;

    float i;
    public MeshRenderer Ring;

    // Start is called before the first frame update
    private  void Start()
    {
        Ring = GetComponent<MeshRenderer>();
        StartCoroutine("ScaleUp");
    }

    // Update is called once per frame
    private void Update()
    {
        if (Chase == true)
        {
            Chaseonoff += Time.deltaTime;
            if (Chaseonoff>=0.5f)
            Chase = false;
        }

        if (Wall == true)
        {
            Wallonoff += Time.deltaTime;
            if (Wallonoff >= 5f)
            {
                Wall = false;
            }
        }

        GameObject eobj = GameObject.FindWithTag("Enemy");
        EnemyController EC = eobj.GetComponent<EnemyController>(); //Enemyに付いているスクリプトを取得
        if (EC.ONoff == 0)
        {
            i = 100;
           Ring.enabled = false;
        }
        if (EC.ONoff == 1)
        {
            Ring.enabled = true;
          //  StartCoroutine("ScaleUp");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Play");
           Chase = true;
        }
        if (other.gameObject.CompareTag("Wall"))
        {
            Wall = true;
            Debug.Log("Wall");
        }
    }

    IEnumerator ScaleUp()
    {
        for (i = 150; i < 200; i += 1f)
        {
            this.transform.localScale = new Vector3(i, i, i);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
