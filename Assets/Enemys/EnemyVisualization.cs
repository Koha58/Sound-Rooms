using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class EnemyVisualization : MonoBehaviour
{
    [SerializeField] public GameObject EnemyRing;
   
    void Start()
    {
       
    }

    private void Update()
    {
  
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Wall"))//�ڐG�����I�u�W�F�N�g�̃^�O��"Wall"�̂Ƃ�
        {
            other.gameObject.GetComponent<Renderer>().enabled = true;
        }
        if (other.CompareTag("Box"))//�ڐG�����I�u�W�F�N�g�̃^�O��"Box"�̂Ƃ�
        {
            other.GetComponent<Renderer>().enabled = true;
        }
    }
}
