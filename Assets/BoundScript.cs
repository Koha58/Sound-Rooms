using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundScript : MonoBehaviour
{
    [SerializeField]
    private Rigidbody Sphere; // ���o�E���h���������I�u�W�F�N�g

    private void OnCollisionEnter(Collision collision)
    {
        Sphere.AddForce(Vector3.up * 3f, ForceMode.Impulse);
    }

}
