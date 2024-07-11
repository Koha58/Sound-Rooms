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
        RingG.GetComponent<Collider>().enabled = false;//見える（有効）
    }

    // Update is called once per frame
    private void Update()
    {
        Ring.enabled = false;
        RingG.GetComponent<Collider>().enabled = false;//見える（有効）
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
