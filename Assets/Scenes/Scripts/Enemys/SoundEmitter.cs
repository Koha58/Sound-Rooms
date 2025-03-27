using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEmitter : MonoBehaviour
{
    public float soundRange =5f; // ‰¹‚ª“Í‚­”ÍˆÍ
    void Start()
    {

    }

    void Update()
    {

        EmitSound();
    }

    public void EmitSound()
    {
        // ”ÍˆÍ“à‚Ì“G‚ğŒŸo‚µ‚Ä’Ê’m
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
        // ‰¹‚Ì”ÍˆÍ‚ğ‰Â‹‰»
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, soundRange);
    }

}
