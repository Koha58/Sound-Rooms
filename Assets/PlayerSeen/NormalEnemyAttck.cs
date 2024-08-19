using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemyAttck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("Enemy"))
        {
            GameObject Enemy = GameObject.FindWithTag("Enemy");
            Enemycontroller Ec = Enemy.GetComponent<Enemycontroller>();
            EnemyIncrease EI = Enemy.GetComponent<EnemyIncrease>();
            if (Ec.DestroyONOFF == true)
            {
                GetComponent<ParticleSystem>().Play();
                EI.isHidden = false;

                GameObject Boss = GameObject.FindWithTag("Boss");
                BoosEnemy BS = Boss.GetComponent<BoosEnemy>();

                BS.MoveSpeed = -0.1f;
                BS.ChaseSpeed = -0.1f;
                BS.VisualizationPlayer = -1f;
                BS.ONOFF = 1;
            }
        }

        if (other.CompareTag("Enemy1"))
        {
            GameObject Enemy1 = GameObject.FindWithTag("Enemy1");
            Enemycontroller Ec = Enemy1.GetComponent<Enemycontroller>();
            EnemyIncrease EI = Enemy1.GetComponent<EnemyIncrease>();
            if (Ec.DestroyONOFF == true)
            {
                GetComponent<ParticleSystem>().Play();
                EI.isHidden = false;

                GameObject Boss = GameObject.FindWithTag("Boss");
                BoosEnemy BS = Boss.GetComponent<BoosEnemy>();

                BS.MoveSpeed = -0.1f;
                BS.ChaseSpeed = -0.1f;
                BS.VisualizationPlayer = -1f;
                BS.ONOFF = 1;
            }
        }

        if (other.CompareTag("Enemy2"))
        {
            GameObject Enemy2 = GameObject.FindWithTag("Enemy2");
            Enemycontroller Ec = Enemy2.GetComponent<Enemycontroller>();
            EnemyIncrease EI = Enemy2.GetComponent<EnemyIncrease>();
            if (Ec.DestroyONOFF == true)
            {
                GetComponent<ParticleSystem>().Play();
                EI.isHidden = false;

                GameObject Boss = GameObject.FindWithTag("Boss");
                BoosEnemy BS = Boss.GetComponent<BoosEnemy>();

                BS.MoveSpeed = -0.1f;
                BS.ChaseSpeed = -0.1f;
                BS.VisualizationPlayer = -1f;
                BS.ONOFF = 1;
            }
        }

        if (other.CompareTag("Enemy3"))
        {
            GameObject Enemy3 = GameObject.FindWithTag("Enemy3");
            Enemycontroller Ec = Enemy3.GetComponent<Enemycontroller>();
            EnemyIncrease EI = Enemy3.GetComponent<EnemyIncrease>();
            if (Ec.DestroyONOFF == true)
            {
                GetComponent<ParticleSystem>().Play();
                EI.isHidden = false;

                GameObject Boss = GameObject.FindWithTag("Boss");
                BoosEnemy BS = Boss.GetComponent<BoosEnemy>();

                BS.MoveSpeed = -0.1f;
                BS.ChaseSpeed = -0.1f;
                BS.VisualizationPlayer = -1f;
                BS.ONOFF = 1;
            }
        }

        if (other.CompareTag("Enemy4"))
        {
            GameObject Enemy4 = GameObject.FindWithTag("Enemy4");
            Enemycontroller Ec = Enemy4.GetComponent<Enemycontroller>();
            EnemyIncrease EI = Enemy4.GetComponent<EnemyIncrease>();
            if (Ec.DestroyONOFF == true)
            {
                GetComponent<ParticleSystem>().Play();
                EI.isHidden = false;

                GameObject Boss = GameObject.FindWithTag("Boss");
                BoosEnemy BS = Boss.GetComponent<BoosEnemy>();

                BS.MoveSpeed = -0.1f;
                BS.ChaseSpeed = -0.1f;
                BS.VisualizationPlayer = -1f;
                BS.ONOFF = 1;
            }
        }

        if (other.CompareTag("Enemy5"))
        {
            GameObject Enemy5= GameObject.FindWithTag("Enemy5");
            Enemycontroller Ec = Enemy5.GetComponent<Enemycontroller>();
            EnemyIncrease EI = Enemy5.GetComponent<EnemyIncrease>();
            if (Ec.DestroyONOFF == true)
            {
                GetComponent<ParticleSystem>().Play();
                EI.isHidden = false;

                GameObject Boss = GameObject.FindWithTag("Boss");
                BoosEnemy BS = Boss.GetComponent<BoosEnemy>();

                BS.MoveSpeed = -0.1f;
                BS.ChaseSpeed = -0.1f;
                BS.VisualizationPlayer = -1f;
                BS.ONOFF = 1;
            }
        }

        if (other.CompareTag("Enemy6"))
        {
            GameObject Enemy6 = GameObject.FindWithTag("Enemy6");
            Enemycontroller Ec = Enemy6.GetComponent<Enemycontroller>();
            EnemyIncrease EI = Enemy6.GetComponent<EnemyIncrease>();
            if (Ec.DestroyONOFF == true)
            {
                GetComponent<ParticleSystem>().Play();
                EI.isHidden = false;

                GameObject Boss = GameObject.FindWithTag("Boss");
                BoosEnemy BS = Boss.GetComponent<BoosEnemy>();

                BS.MoveSpeed = -0.1f;
                BS.ChaseSpeed = -0.1f;
                BS.VisualizationPlayer = -1f;
                BS.ONOFF = 1;
            }
        }

        if (other.CompareTag("Enemy7"))
        {
            GameObject Enemy7 = GameObject.FindWithTag("Enemy7");
            Enemycontroller Ec = Enemy7.GetComponent<Enemycontroller>();
            EnemyIncrease EI = Enemy7.GetComponent<EnemyIncrease>();
            if (Ec.DestroyONOFF == true)
            {
                GetComponent<ParticleSystem>().Play();
                EI.isHidden = false;

                GameObject Boss = GameObject.FindWithTag("Boss");
                BoosEnemy BS = Boss.GetComponent<BoosEnemy>();

                BS.MoveSpeed = -0.1f;
                BS.ChaseSpeed = -0.1f;
                BS.VisualizationPlayer = -1f;
                BS.ONOFF = 1;
            }
        }

        if (other.CompareTag("Enemy8"))
        {
            GameObject Enemy8 = GameObject.FindWithTag("Enemy8");
            Enemycontroller Ec = Enemy8.GetComponent<Enemycontroller>();
            EnemyIncrease EI = Enemy8.GetComponent<EnemyIncrease>();
            if (Ec.DestroyONOFF == true)
            {
                GetComponent<ParticleSystem>().Play();
                EI.isHidden = false;

                GameObject Boss = GameObject.FindWithTag("Boss");
                BoosEnemy BS = Boss.GetComponent<BoosEnemy>();

                BS.MoveSpeed = -0.1f;
                BS.ChaseSpeed = -0.1f;
                BS.VisualizationPlayer = -1f;
                BS.ONOFF = 1;
            }
        }

        if (other.CompareTag("Enemy9"))
        {
            GameObject Enemy9 = GameObject.FindWithTag("Enemy9");
            Enemycontroller Ec = Enemy9.GetComponent<Enemycontroller>();
            EnemyIncrease EI = Enemy9.GetComponent<EnemyIncrease>();
            if (Ec.DestroyONOFF == true)
            {
                GetComponent<ParticleSystem>().Play();
                EI.isHidden = false;

                GameObject Boss = GameObject.FindWithTag("Boss");
                BoosEnemy BS = Boss.GetComponent<BoosEnemy>();

                BS.MoveSpeed = -0.1f;
                BS.ChaseSpeed = -0.1f;
                BS.VisualizationPlayer = -1f;
                BS.ONOFF = 1;
            }
        }
    }
}
