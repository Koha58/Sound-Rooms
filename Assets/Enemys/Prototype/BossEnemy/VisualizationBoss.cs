using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VisualizationBoss : MonoBehaviour
{
   [SerializeField] Transform Boss;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       transform.position = Boss.transform.position ;
        GameObject gameObject =GameObject.FindWithTag("Boss"); //Playerオブジェクトを探す
        BoosEnemy BC = gameObject.GetComponent<BoosEnemy>();
        BC.PrototypeBodySkinnedMeshRenderer.enabled = true;
        BC.ONOFF = 1;
        if (BC.ONOFF == 1)
        {
            this.gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("Enemy"))
        {
            Enemycontroller EC =other.GetComponent<Enemycontroller>();
            EC.PrototypeBodySkinnedMeshRenderer.enabled=true;
            EC.ONOFF = 1;
        }

        if (other.CompareTag("Enemy1"))
        {
            EnemyController EC1 = other.GetComponent<EnemyController>();
            EC1.PrototypeBodySkinnedMeshRenderer.enabled = true;
            EC1.ONOFF = 1;
        }

        if (other.CompareTag("EnemyG"))
        {
            Enemycontroller ECG = other.GetComponent<Enemycontroller>();
            ECG.PrototypeBodySkinnedMeshRenderer.enabled = true;
            ECG.ONOFF = 1;
        }

        if (other.CompareTag("EnemySearch"))
        {
            EnemySearchcontroller ES = other.GetComponent<EnemySearchcontroller>();
            ES.PrototypeBodySkinnedMeshRenderer.enabled = true;
            ES.ONOFF = 1;
        }

        if (other.CompareTag("Player"))
        {
            GameObject obj = GameObject.Find("Player"); //Playerオブジェクトを探す
            PlayerSeen PS = obj.GetComponent<PlayerSeen>(); //付いているスクリプトを取得
            var childTransforms = PS._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));
            PS.onoff = 1;  //見えているから1
            PS.Visualization = true;
            foreach (var playerParts in childTransforms)
            {
                //タグが"PlayerParts"である子オブジェクトを見えるようにする
                playerParts.gameObject.GetComponent<Renderer>().enabled = true;
            }
        }

    }
}
