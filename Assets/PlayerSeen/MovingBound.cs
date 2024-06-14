using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBound : MonoBehaviour
{
    [SerializeField]
    private Rigidbody MovingSphere; // ←バウンドさせたいオブジェクト
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        MovingSphere.AddForce(Vector3.up * 10f, ForceMode.Impulse);
    }
}
