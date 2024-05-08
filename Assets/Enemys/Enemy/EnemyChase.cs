using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public Transform Player;//プレイヤーを参照
    private float Detection = 7f; //プレイヤーを検知する範囲
    static public  bool EnemyChaseOnOff = false;//Playerの追跡のONOFF 

    private bool Enemytouch;//壁にタッチのonoff
    private float time = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    private  void Update()
    {
        
        GameObject eobj = GameObject.FindWithTag("Enemy");
        EnemySeen ES = eobj.GetComponent<EnemySeen>();//EnemySeenに付いているスクリプトを取得

        float　detectionPlayer = Vector3.Distance(transform.position, Player.position);//プレイヤーと敵の位置の計算

        if (detectionPlayer <= Detection && ES.ONoff == 1 && Enemytouch == false )//Enemyが可視化状態かつプレイヤーが検知範囲に入ったら
        {
             EnemyChaseOnOff = true ;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        GameObject eobj = GameObject.FindWithTag("Enemy");
        // Enemyに付いているスクリプトを取得
        EnemySeen ES = eobj.GetComponent<EnemySeen>();

        if (other.gameObject.CompareTag("Wall"))
        {
            Debug.Log("!");
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
