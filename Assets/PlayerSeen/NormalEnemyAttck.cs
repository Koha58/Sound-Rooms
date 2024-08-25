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

                /*
                GameObject Boss = GameObject.FindWithTag("Boss");
                BoosEnemy BS = Boss.GetComponent<BoosEnemy>();

                BS.MoveSpeed = -0.1f;
                BS.ChaseSpeed = -0.1f;
                BS.VisualizationPlayer = -1f;
                BS.ONOFF = 1;*/
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
            }
        }

        if (other.CompareTag("Enemy10"))
        {
            GameObject Enemy10 = GameObject.FindWithTag("Enemy10");
            Enemycontroller Ec = Enemy10.GetComponent<Enemycontroller>();
            EnemyIncrease EI = Enemy10.GetComponent<EnemyIncrease>();
            if (Ec.DestroyONOFF == true)
            {
                GetComponent<ParticleSystem>().Play();
                EI.isHidden = false;

         
            }
        }
        if (other.CompareTag("Enemy11"))
        {
            GameObject Enemy11 = GameObject.FindWithTag("Enemy11");
            Enemycontroller Ec = Enemy11.GetComponent<Enemycontroller>();
            EnemyIncrease EI = Enemy11.GetComponent<EnemyIncrease>();
            if (Ec.DestroyONOFF == true)
            {
                GetComponent<ParticleSystem>().Play();
                EI.isHidden = false;
            }
        }
        if (other.CompareTag("Enemy12"))
        {
            GameObject Enemy12 = GameObject.FindWithTag("Enemy12");
            Enemycontroller Ec = Enemy12.GetComponent<Enemycontroller>();
            EnemyIncrease EI = Enemy12.GetComponent<EnemyIncrease>();
            if (Ec.DestroyONOFF == true)
            {
                GetComponent<ParticleSystem>().Play();
                EI.isHidden = false;
            }
        }
        if (other.CompareTag("Enemy13"))
        {
            GameObject Enemy13 = GameObject.FindWithTag("Enemy13");
            Enemycontroller Ec = Enemy13.GetComponent<Enemycontroller>();
            EnemyIncrease EI = Enemy13.GetComponent<EnemyIncrease>();
            if (Ec.DestroyONOFF == true)
            {
                GetComponent<ParticleSystem>().Play();
                EI.isHidden = false;
            }
        }

        if (other.CompareTag("Enemy14"))
        {
            GameObject Enemy14 = GameObject.FindWithTag("Enemy14");
            Enemycontroller Ec = Enemy14.GetComponent<Enemycontroller>();
            EnemyIncrease EI = Enemy14.GetComponent<EnemyIncrease>();
            if (Ec.DestroyONOFF == true)
            {
                GetComponent<ParticleSystem>().Play();
                EI.isHidden = false;
            }
        }

        if (other.CompareTag("Enemy15"))
        {
            GameObject Enemy15 = GameObject.FindWithTag("Enemy15");
            Enemycontroller Ec = Enemy15.GetComponent<Enemycontroller>();
            EnemyIncrease EI = Enemy15.GetComponent<EnemyIncrease>();
            if (Ec.DestroyONOFF == true)
            {
                GetComponent<ParticleSystem>().Play();
                EI.isHidden = false;
            }
        }

        if (other.CompareTag("Enemy16"))
        {
            GameObject Enemy16 = GameObject.FindWithTag("Enemy16");
            Enemycontroller Ec = Enemy16.GetComponent<Enemycontroller>();
            EnemyIncrease EI = Enemy16.GetComponent<EnemyIncrease>();
            if (Ec.DestroyONOFF == true)
            {
                GetComponent<ParticleSystem>().Play();
                EI.isHidden = false;
            }
        }

        if (other.CompareTag("Enemy17"))
        {
            GameObject Enemy17 = GameObject.FindWithTag("Enemy17");
            Enemycontroller Ec = Enemy17.GetComponent<Enemycontroller>();
            EnemyIncrease EI = Enemy17.GetComponent<EnemyIncrease>();
            if (Ec.DestroyONOFF == true)
            {
                GetComponent<ParticleSystem>().Play();
                EI.isHidden = false;
            }
        }

        if (other.CompareTag("Enemy18"))
        {
            GameObject Enemy18= GameObject.FindWithTag("Enemy18");
            Enemycontroller Ec = Enemy18.GetComponent<Enemycontroller>();
            EnemyIncrease EI = Enemy18.GetComponent<EnemyIncrease>();
            if (Ec.DestroyONOFF == true)
            {
                GetComponent<ParticleSystem>().Play();
                EI.isHidden = false;
            }
        }

        if (other.CompareTag("Enemy19"))
        {
            GameObject Enemy19 = GameObject.FindWithTag("Enemy19");
            Enemycontroller Ec = Enemy19.GetComponent<Enemycontroller>();
            EnemyIncrease EI = Enemy19.GetComponent<EnemyIncrease>();
            if (Ec.DestroyONOFF == true)
            {
                GetComponent<ParticleSystem>().Play();
                EI.isHidden = false;
            }
        }

        if (other.CompareTag("Enemy20"))
        {
            GameObject Enemy20 = GameObject.FindWithTag("Enemy20");
            Enemycontroller Ec = Enemy20.GetComponent<Enemycontroller>();
            EnemyIncrease EI = Enemy20.GetComponent<EnemyIncrease>();
            if (Ec.DestroyONOFF == true)
            {
                GetComponent<ParticleSystem>().Play();
                EI.isHidden = false;
            }
        }

        if (other.CompareTag("Enemy21"))
        {
            GameObject Enemy21 = GameObject.FindWithTag("Enemy21");
            Enemycontroller Ec = Enemy21.GetComponent<Enemycontroller>();
            EnemyIncrease EI = Enemy21.GetComponent<EnemyIncrease>();
            if (Ec.DestroyONOFF == true)
            {
                GetComponent<ParticleSystem>().Play();
                EI.isHidden = false;
            }
        }

        if (other.CompareTag("Enemy22"))
        {
            GameObject Enemy22 = GameObject.FindWithTag("Enemy22");
            Enemycontroller Ec = Enemy22.GetComponent<Enemycontroller>();
            EnemyIncrease EI = Enemy22.GetComponent<EnemyIncrease>();
            if (Ec.DestroyONOFF == true)
            {
                GetComponent<ParticleSystem>().Play();
                EI.isHidden = false;
            }
        }

        if (other.CompareTag("Enemy23"))
        {
            GameObject Enemy23 = GameObject.FindWithTag("Enemy23");
            Enemycontroller Ec = Enemy23.GetComponent<Enemycontroller>();
            EnemyIncrease EI = Enemy23.GetComponent<EnemyIncrease>();
            if (Ec.DestroyONOFF == true)
            {
                GetComponent<ParticleSystem>().Play();
                EI.isHidden = false;
            }
        }

        if (other.CompareTag("Enemy24"))
        {
            GameObject Enemy24 = GameObject.FindWithTag("Enemy24");
            Enemycontroller Ec = Enemy24.GetComponent<Enemycontroller>();
            EnemyIncrease EI = Enemy24.GetComponent<EnemyIncrease>();
            if (Ec.DestroyONOFF == true)
            {
                GetComponent<ParticleSystem>().Play();
                EI.isHidden = false;
            }
        }

        if (other.CompareTag("Enemy25"))
        {
            GameObject Enemy25 = GameObject.FindWithTag("Enemy25");
            Enemycontroller Ec = Enemy25.GetComponent<Enemycontroller>();
            EnemyIncrease EI = Enemy25.GetComponent<EnemyIncrease>();
            if (Ec.DestroyONOFF == true)
            {
                GetComponent<ParticleSystem>().Play();
                EI.isHidden = false;
            }
        }

        if (other.CompareTag("Enemy26"))
        {
            GameObject Enemy26 = GameObject.FindWithTag("Enemy26");
            Enemycontroller Ec = Enemy26.GetComponent<Enemycontroller>();
            EnemyIncrease EI = Enemy26.GetComponent<EnemyIncrease>();
            if (Ec.DestroyONOFF == true)
            {
                GetComponent<ParticleSystem>().Play();
                EI.isHidden = false;
            }
        }

        if (other.CompareTag("Enemy27"))
        {
            GameObject Enemy27 = GameObject.FindWithTag("Enemy27");
            Enemycontroller Ec = Enemy27.GetComponent<Enemycontroller>();
            EnemyIncrease EI = Enemy27.GetComponent<EnemyIncrease>();
            if (Ec.DestroyONOFF == true)
            {
                GetComponent<ParticleSystem>().Play();
                EI.isHidden = false;
            }
        }

        if (other.CompareTag("Enemy28"))
        {
            GameObject Enemy28 = GameObject.FindWithTag("Enemy28");
            Enemycontroller Ec = Enemy28.GetComponent<Enemycontroller>();
            EnemyIncrease EI = Enemy28.GetComponent<EnemyIncrease>();
            if (Ec.DestroyONOFF == true)
            {
                GetComponent<ParticleSystem>().Play();
                EI.isHidden = false;
            }
        }

        if (other.CompareTag("Enemy29"))
        {
            GameObject Enemy29 = GameObject.FindWithTag("Enemy29");
            Enemycontroller Ec = Enemy29.GetComponent<Enemycontroller>();
            EnemyIncrease EI = Enemy29.GetComponent<EnemyIncrease>();
            if (Ec.DestroyONOFF == true)
            {
                GetComponent<ParticleSystem>().Play();
                EI.isHidden = false;
            }
        }

        if (other.CompareTag("Enemy30"))
        {
            GameObject Enemy30 = GameObject.FindWithTag("Enemy30");
            Enemycontroller Ec = Enemy30.GetComponent<Enemycontroller>();
            EnemyIncrease EI = Enemy30.GetComponent<EnemyIncrease>();
            if (Ec.DestroyONOFF == true)
            {
                GetComponent<ParticleSystem>().Play();
                EI.isHidden = false;
            }
        }

        if (other.CompareTag("Enemy31"))
        {
            GameObject Enemy31 = GameObject.FindWithTag("Enemy31");
            Enemycontroller Ec = Enemy31.GetComponent<Enemycontroller>();
            EnemyIncrease EI = Enemy31.GetComponent<EnemyIncrease>();
            if (Ec.DestroyONOFF == true)
            {
                GetComponent<ParticleSystem>().Play();
                EI.isHidden = false;
            }
        }
    }
}
