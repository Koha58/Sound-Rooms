using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySearchAttck : MonoBehaviour
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
        if (other.CompareTag("EnemySearch"))
        {
            GameObject EnemySearch = GameObject.FindWithTag("EnemySearch");
            EnemySearchcontroller ES = EnemySearch.GetComponent<EnemySearchcontroller>();
            Enemy2Increase E2I = EnemySearch.GetComponent<Enemy2Increase>();

            if (ES.DestroyONOFF == true)
            {
                GetComponent<ParticleSystem>().Play();
                E2I.isHidden = false;
                /*
                GameObject Boss = GameObject.FindWithTag("Boss");
                BoosEnemy BS = Boss.GetComponent<BoosEnemy>();

                BS.MoveSpeed = -0.1f;
                BS.ChaseSpeed = -0.1f;
                BS.VisualizationPlayer = -1f;
                BS.ONOFF = 1;
                */
            }
        }

        if (other.CompareTag("EnemySearch1"))
        {
            GameObject EnemySearch1 = GameObject.FindWithTag("EnemySearch1");
            EnemySearchcontroller ES = EnemySearch1.GetComponent<EnemySearchcontroller>();
            Enemy2Increase E2I = EnemySearch1.GetComponent<Enemy2Increase>();

            if (ES.DestroyONOFF == true)
            {
                GetComponent<ParticleSystem>().Play();
                E2I.isHidden = false;

            }
        }

        if (other.CompareTag("EnemySearch2"))
        {
            GameObject EnemySearch2 = GameObject.FindWithTag("EnemySearch2");
            EnemySearchcontroller ES = EnemySearch2.GetComponent<EnemySearchcontroller>();
            Enemy2Increase E2I = EnemySearch2.GetComponent<Enemy2Increase>();

            if (ES.DestroyONOFF == true)
            {
                GetComponent<ParticleSystem>().Play();
                E2I.isHidden = false;

            }
        }

        if (other.CompareTag("EnemySearch3"))
        {
            GameObject EnemySearch3 = GameObject.FindWithTag("EnemySearch3");
            EnemySearchcontroller ES = EnemySearch3.GetComponent<EnemySearchcontroller>();
            Enemy2Increase E2I = EnemySearch3.GetComponent<Enemy2Increase>();

            if (ES.DestroyONOFF == true)
            {
                GetComponent<ParticleSystem>().Play();
                E2I.isHidden = false;


            }
        }

        if (other.CompareTag("EnemySearch4"))
        {
            GameObject EnemySearch4 = GameObject.FindWithTag("EnemySearch4");
            EnemySearchcontroller ES = EnemySearch4.GetComponent<EnemySearchcontroller>();
            Enemy2Increase E2I = EnemySearch4.GetComponent<Enemy2Increase>();

            if (ES.DestroyONOFF == true)
            {
                GetComponent<ParticleSystem>().Play();
                E2I.isHidden = false;

            }
        }

        if (other.CompareTag("EnemySearch5"))
        {
            GameObject EnemySearch5 = GameObject.FindWithTag("EnemySearch5");
            EnemySearchcontroller ES = EnemySearch5.GetComponent<EnemySearchcontroller>();
            Enemy2Increase E2I = EnemySearch5.GetComponent<Enemy2Increase>();

            if (ES.DestroyONOFF == true)
            {
                GetComponent<ParticleSystem>().Play();
                E2I.isHidden = false;

            }
        }

        if (other.CompareTag("EnemySearch6"))
        {
            GameObject EnemySearch6 = GameObject.FindWithTag("EnemySearch6");
            EnemySearchcontroller ES = EnemySearch6.GetComponent<EnemySearchcontroller>();
            Enemy2Increase E2I = EnemySearch6.GetComponent<Enemy2Increase>();

            if (ES.DestroyONOFF == true)
            {
                GetComponent<ParticleSystem>().Play();
                E2I.isHidden = false;

            }
        }

        if (other.CompareTag("EnemySearch7"))
        {
            GameObject EnemySearch7 = GameObject.FindWithTag("EnemySearch7");
            EnemySearchcontroller ES = EnemySearch7.GetComponent<EnemySearchcontroller>();
            Enemy2Increase E2I = EnemySearch7.GetComponent<Enemy2Increase>();

            if (ES.DestroyONOFF == true)
            {
                GetComponent<ParticleSystem>().Play();
                E2I.isHidden = false;

            }
        }

        if (other.CompareTag("EnemySearch8"))
        {
            GameObject EnemySearch8 = GameObject.FindWithTag("EnemySearch8");
            EnemySearchcontroller ES = EnemySearch8.GetComponent<EnemySearchcontroller>();
            Enemy2Increase E2I = EnemySearch8.GetComponent<Enemy2Increase>();

            if (ES.DestroyONOFF == true)
            {
                GetComponent<ParticleSystem>().Play();
                E2I.isHidden = false;

            }
        }

        if (other.CompareTag("EnemySearch9"))
        {
            GameObject EnemySearch9 = GameObject.FindWithTag("EnemySearch9");
            EnemySearchcontroller ES = EnemySearch9.GetComponent<EnemySearchcontroller>();
            Enemy2Increase E2I = EnemySearch9.GetComponent<Enemy2Increase>();

            if (ES.DestroyONOFF == true)
            {
                GetComponent<ParticleSystem>().Play();
                E2I.isHidden = false;

            }
        }
    }
}
