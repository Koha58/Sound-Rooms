using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEmitter : MonoBehaviour
{
    public float soundRange =10f; // �����͂��͈�
    void Start()
    {

    }

    void Update()
    {

        EmitSound();

    }

    public void EmitSound()
    {

        // �͈͓��̓G�����o���Ēʒm
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, soundRange);
        foreach (Collider collider in hitColliders)
        {
            EnemyController1 enemy = collider.GetComponent<EnemyController1>();

            if (enemy != null)
            {
                enemy.OnSoundHeard(transform.position);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // ���͈̔͂�����
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, soundRange);
    }

}
