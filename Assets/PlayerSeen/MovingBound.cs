using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBound : MonoBehaviour
{
    [SerializeField]
    private Rigidbody MovingSphere; // ���o�E���h���������I�u�W�F�N�g
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        MovingSphere.AddForce(Vector3.up * 10f, ForceMode.Impulse);
    }
}
