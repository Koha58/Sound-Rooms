using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enemy�ɉ����Ԃ��鋓��
public class EnemyAttack : MonoBehaviour
{
    int onoff = 0;  //����p�i�����Ă��Ȃ����F0/�����Ă��鎞�F1�j

    private float seentime = 0.0f; //�o�ߎ��ԋL�^�p
    [SerializeField] public GameObject EnemyAttackArea;
    // Start is called before the first frame update
    void Start()
    {
        //�ŏ��͌����Ȃ����
        EnemyAttackArea.GetComponent<Collider>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //���N���b�N�Ŕ͈͓�������
        if (Input.GetMouseButtonUp(0))
        {
            EnemyAttackArea.GetComponent<Collider>().enabled = true;//������i�L���j
            onoff = 1;  //�����Ă��邩��1
        }

        if (onoff == 1)
        {
            seentime += Time.deltaTime;
            if (seentime >= 10.0f)
            {
                EnemyAttackArea.GetComponent<Collider>().enabled = false;//�����Ȃ��i�����j
                onoff = 0;  //�����Ă��Ȃ�����0
                seentime = 0.0f;    //�o�ߎ��Ԃ����Z�b�g
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemyincrease.isHidden = false;
            Destroy(other.gameObject);
        }
    }
}
