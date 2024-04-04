using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemySeen : MonoBehaviour
{
    public int ONoff = 0;//(0�������Ȃ��G�P���������ԁj
    private float Seetime;  //�o�ߎ���
    public  float SoundTime;
    [SerializeField] public GameObject Sphere;
    [SerializeField] public Transform _parentTransform;

    // Start is called before the first frame update
    void Start()
    {
        //tag��"EnemyParts"�ł���q�I�u�W�F�N�g��Transform�̃R���N�V�������擾
        var childTransforms = _parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("EnemyParts"));

        foreach (var item in childTransforms)
        {
            //�^�O��"EnemyParts"�ł���q�I�u�W�F�N�g�������Ȃ�����
            item.gameObject.GetComponent<Renderer>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //tag��"EnemyParts"�ł���q�I�u�W�F�N�g��Transform�̃R���N�V�������擾
        var childTransforms = _parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("EnemyParts"));
        if (ONoff == 0)//�����Ȃ��Ƃ�
        {
            SoundTime += Time.deltaTime;
            if (SoundTime > 10.0f)
            {              
                foreach (var item in childTransforms)
                {
                    //�^�O��"EnemyParts"�ł���q�I�u�W�F�N�g��������悤�ɂ���
                    item.gameObject.GetComponent<Renderer>().enabled = true;
                }
                ONoff = 1;
                SoundTime = 0.0f;
                Sphere.SetActive(true);//���g��\�����\��
            }
        }
         if (ONoff == 1)//�����Ă���Ƃ�
         {
            Seetime += Time.deltaTime;
            if (Seetime >= 10.0f)
            {
                foreach (var item in childTransforms)
                {
                    //�^�O��"EnemyParts"�ł���q�I�u�W�F�N�g�������Ȃ�����
                    item.gameObject.GetComponent<Renderer>().enabled = false;
                }
                ONoff = 0;
                Seetime = 0.0f;
                Sphere.SetActive(false);//���g�\������\��
            }
         }
    } 
}
