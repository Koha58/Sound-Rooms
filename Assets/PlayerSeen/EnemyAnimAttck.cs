using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimAttck : MonoBehaviour
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
        if (other.CompareTag("EnemyAnim"))
        {
            GameObject EnemyAnim = GameObject.FindWithTag("EnemyAnim");
            EnemyAnimcontroller EA = EnemyAnim.GetComponent<EnemyAnimcontroller>();
            EnemyAnimIncrease EAI = EnemyAnim.GetComponent<EnemyAnimIncrease>();
            if (EA.DestroyONOFF == true)
            {
                GetComponent<ParticleSystem>().Play();
                EAI.isHidden = false;

                GameObject Boss = GameObject.FindWithTag("Boss");
                BoosEnemy BS = Boss.GetComponent<BoosEnemy>();

                BS.MoveSpeed = -0.1f;
                BS.ChaseSpeed = -0.1f;
                BS.VisualizationPlayer = -1f;
                BS.ONOFF = 1;
            }
        }

        if (other.CompareTag("EnemyAnim1"))
        {
            GameObject EnemyAnim1 = GameObject.FindWithTag("EnemyAnim1");
            EnemyAnimcontroller EA = EnemyAnim1.GetComponent<EnemyAnimcontroller>();
            EnemyAnimIncrease EAI = EnemyAnim1.GetComponent<EnemyAnimIncrease>();
            if (EA.DestroyONOFF == true)
            {
                GetComponent<ParticleSystem>().Play();
                EAI.isHidden = false;

                GameObject Boss = GameObject.FindWithTag("Boss");
                BoosEnemy BS = Boss.GetComponent<BoosEnemy>();

                BS.MoveSpeed = -0.1f;
                BS.ChaseSpeed = -0.1f;
                BS.VisualizationPlayer = -1f;
                BS.ONOFF = 1;
            }
        }
        if (other.CompareTag("EnemyAnim2"))
        {
            GameObject EnemyAnim2 = GameObject.FindWithTag("EnemyAnim2");
            EnemyAnimcontroller EA = EnemyAnim2.GetComponent<EnemyAnimcontroller>();
            EnemyAnimIncrease EAI = EnemyAnim2.GetComponent<EnemyAnimIncrease>();
            if (EA.DestroyONOFF == true)
            {
                GetComponent<ParticleSystem>().Play();
                EAI.isHidden = false;

                GameObject Boss = GameObject.FindWithTag("Boss");
                BoosEnemy BS = Boss.GetComponent<BoosEnemy>();

                BS.MoveSpeed = -0.1f;
                BS.ChaseSpeed = -0.1f;
                BS.VisualizationPlayer = -1f;
                BS.ONOFF = 1;
            }
        }
        if (other.CompareTag("EnemyAnim3"))
        {
            GameObject EnemyAnim3 = GameObject.FindWithTag("EnemyAnim3");
            EnemyAnimcontroller EA = EnemyAnim3.GetComponent<EnemyAnimcontroller>();
            EnemyAnimIncrease EAI = EnemyAnim3.GetComponent<EnemyAnimIncrease>();
            if (EA.DestroyONOFF == true)
            {
                GetComponent<ParticleSystem>().Play();
                EAI.isHidden = false;

                GameObject Boss = GameObject.FindWithTag("Boss");
                BoosEnemy BS = Boss.GetComponent<BoosEnemy>();

                BS.MoveSpeed = -0.1f;
                BS.ChaseSpeed = -0.1f;
                BS.VisualizationPlayer = -1f;
                BS.ONOFF = 1;
            }
        }
        if (other.CompareTag("EnemyAnim4"))
        {
            GameObject EnemyAnim4 = GameObject.FindWithTag("EnemyAnim4");
            EnemyAnimcontroller EA = EnemyAnim4.GetComponent<EnemyAnimcontroller>();
            EnemyAnimIncrease EAI = EnemyAnim4.GetComponent<EnemyAnimIncrease>();
            if (EA.DestroyONOFF == true)
            {
                GetComponent<ParticleSystem>().Play();
                EAI.isHidden = false;

                GameObject Boss = GameObject.FindWithTag("Boss");
                BoosEnemy BS = Boss.GetComponent<BoosEnemy>();

                BS.MoveSpeed = -0.1f;
                BS.ChaseSpeed = -0.1f;
                BS.VisualizationPlayer = -1f;
                BS.ONOFF = 1;
            }
        }
        if (other.CompareTag("EnemyAnim5"))
        {
            GameObject EnemyAnim5 = GameObject.FindWithTag("EnemyAnim5");
            EnemyAnimcontroller EA = EnemyAnim5.GetComponent<EnemyAnimcontroller>();
            EnemyAnimIncrease EAI = EnemyAnim5.GetComponent<EnemyAnimIncrease>();
            if (EA.DestroyONOFF == true)
            {
                GetComponent<ParticleSystem>().Play();
                EAI.isHidden = false;

                GameObject Boss = GameObject.FindWithTag("Boss");
                BoosEnemy BS = Boss.GetComponent<BoosEnemy>();

                BS.MoveSpeed = -0.1f;
                BS.ChaseSpeed = -0.1f;
                BS.VisualizationPlayer = -1f;
                BS.ONOFF = 1;
            }
        }
        if (other.CompareTag("EnemyAnim6"))
        {
            GameObject EnemyAnim6 = GameObject.FindWithTag("EnemyAnim6");
            EnemyAnimcontroller EA = EnemyAnim6.GetComponent<EnemyAnimcontroller>();
            EnemyAnimIncrease EAI = EnemyAnim6.GetComponent<EnemyAnimIncrease>();
            if (EA.DestroyONOFF == true)
            {
                GetComponent<ParticleSystem>().Play();
                EAI.isHidden = false;

                GameObject Boss = GameObject.FindWithTag("Boss");
                BoosEnemy BS = Boss.GetComponent<BoosEnemy>();

                BS.MoveSpeed = -0.1f;
                BS.ChaseSpeed = -0.1f;
                BS.VisualizationPlayer = -1f;
                BS.ONOFF = 1;
            }
        }
        if (other.CompareTag("EnemyAnim7"))
        {
            GameObject EnemyAnim7 = GameObject.FindWithTag("EnemyAnim7");
            EnemyAnimcontroller EA = EnemyAnim7.GetComponent<EnemyAnimcontroller>();
            EnemyAnimIncrease EAI = EnemyAnim7.GetComponent<EnemyAnimIncrease>();
            if (EA.DestroyONOFF == true)
            {
                GetComponent<ParticleSystem>().Play();
                EAI.isHidden = false;

                GameObject Boss = GameObject.FindWithTag("Boss");
                BoosEnemy BS = Boss.GetComponent<BoosEnemy>();

                BS.MoveSpeed = -0.1f;
                BS.ChaseSpeed = -0.1f;
                BS.VisualizationPlayer = -1f;
                BS.ONOFF = 1;
            }
        }
        if (other.CompareTag("EnemyAnim8"))
        {
            GameObject EnemyAnim = GameObject.FindWithTag("EnemyAnim8");
            EnemyAnimcontroller EA = EnemyAnim.GetComponent<EnemyAnimcontroller>();
            EnemyAnimIncrease EAI = EnemyAnim.GetComponent<EnemyAnimIncrease>();
            if (EA.DestroyONOFF == true)
            {
                GetComponent<ParticleSystem>().Play();
                EAI.isHidden = false;

                GameObject Boss = GameObject.FindWithTag("Boss");
                BoosEnemy BS = Boss.GetComponent<BoosEnemy>();

                BS.MoveSpeed = -0.1f;
                BS.ChaseSpeed = -0.1f;
                BS.VisualizationPlayer = -1f;
                BS.ONOFF = 1;
            }
        }
        if (other.CompareTag("EnemyAnim9"))
        {
            GameObject EnemyAnim = GameObject.FindWithTag("EnemyAnim9");
            EnemyAnimcontroller EA = EnemyAnim.GetComponent<EnemyAnimcontroller>();
            EnemyAnimIncrease EAI = EnemyAnim.GetComponent<EnemyAnimIncrease>();
            if (EA.DestroyONOFF == true)
            {
                GetComponent<ParticleSystem>().Play();
                EAI.isHidden = false;

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
