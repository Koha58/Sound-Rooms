using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TutorialVisualizationBox : MonoBehaviour
{
    [SerializeField] Transform Boss;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Boss.transform.position;
        GameObject gameObject = GameObject.FindWithTag("Boss1"); //Player�I�u�W�F�N�g��T��
        BossTutoriaru BC = gameObject.GetComponent<BossTutoriaru>();
        BC.PrototypeBodySkinnedMeshRenderer.enabled = true;
        if (BC.ONOFF == 1)
        {
            this.gameObject.SetActive(true);

        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("Enemy"))
        {
            Enemycontroller EC = other.GetComponent<Enemycontroller>();
            EC.PrototypeBodySkinnedMeshRenderer.enabled = true;
            EC.ONOFF = 1;
        }

        if (other.CompareTag("Enemy1"))
        {
            TutorialEnemyController EC1 = other.GetComponent<TutorialEnemyController>();
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
            if (Table.ON == false)
            {
                GameObject obj = GameObject.Find("Player"); //Player�I�u�W�F�N�g��T��
                PlayerSeen PS = obj.GetComponent<PlayerSeen>(); //�t���Ă���X�N���v�g���擾
                GameOverScript GOS = obj.GetComponent<GameOverScript>();
                var childTransforms = PS._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));
                PS.onoff = 1;  //�����Ă��邩��1
                PS.Visualization = true;
                foreach (var playerParts in childTransforms)
                {
                    //�^�O��"PlayerParts"�ł���q�I�u�W�F�N�g��������悤�ɂ���
                    playerParts.gameObject.GetComponent<Renderer>().enabled = true;
                }
            }
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject obj = GameObject.Find("Player"); //Player�I�u�W�F�N�g��T��
            PlayerSeen PS = obj.GetComponent<PlayerSeen>(); //�t���Ă���X�N���v�g���擾
            var childTransforms = PS._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));
            PS.Visualization = false;
            PS.onoff = 0;                                                             //�����Ă��邩��1
            foreach (var playerParts in childTransforms)
            {
                //�^�O��"PlayerParts"�ł���q�I�u�W�F�N�g��������悤�ɂ���
                playerParts.gameObject.GetComponent<Renderer>().enabled = false;
            }
        }
    }
}
