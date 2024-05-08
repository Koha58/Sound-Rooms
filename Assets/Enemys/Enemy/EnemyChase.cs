using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public Transform Player;//�v���C���[���Q��
    private float Detection = 7f; //�v���C���[�����m����͈�
    static public  bool EnemyChaseOnOff = false;//Player�̒ǐՂ�ONOFF 

    private bool Enemytouch;//�ǂɃ^�b�`��onoff
    private float time = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    private  void Update()
    {
        
        GameObject eobj = GameObject.FindWithTag("Enemy");
        EnemySeen ES = eobj.GetComponent<EnemySeen>();//EnemySeen�ɕt���Ă���X�N���v�g���擾

        float�@detectionPlayer = Vector3.Distance(transform.position, Player.position);//�v���C���[�ƓG�̈ʒu�̌v�Z

        if (detectionPlayer <= Detection && ES.ONoff == 1 && Enemytouch == false )//Enemy��������Ԃ��v���C���[�����m�͈͂ɓ�������
        {
             EnemyChaseOnOff = true ;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        GameObject eobj = GameObject.FindWithTag("Enemy");
        // Enemy�ɕt���Ă���X�N���v�g���擾
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
