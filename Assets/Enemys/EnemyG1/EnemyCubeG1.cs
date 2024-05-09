using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCubeG1 : MonoBehaviour
{
    private bool Enemytouch;//壁にタッチのonoff
    private float time = 0.0f;

    // Start is called before the first frame update
    private void Start()
    {
        Enemytouch = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {

        GameObject eobj = GameObject.FindWithTag("Enemy");
        // Enemyに付いているスクリプトを取得
        EnemySeen ES = eobj.GetComponent<EnemySeen>();

        if (other.gameObject.CompareTag("Wall"))
        {
            if (ES.ONoff == 1)
            {
                Enemytouch = true;

                if (Enemytouch == true)
                {
                    time += Time.deltaTime;
                    if (time > 2.0f)
                    {
                        Enemytouch = false;
                    }
                }
            }
        }
    }
}
