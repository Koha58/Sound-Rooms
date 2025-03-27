using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEmitter : MonoBehaviour
{
    public float soundRange =5f; // �����͂��͈�
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
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, soundRange);
        foreach (Collider collider in hitColliders)
        {
            EnemyController enemy = collider.GetComponent<EnemyController>();

            if (enemy != null)
            {
                enemy.OnSoundHeard(this.transform.position);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // ���͈̔͂�����
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, soundRange);
    }

}
