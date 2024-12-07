using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEmitter : MonoBehaviour
{
    public float soundRange =10f; // ‰¹‚ª“Í‚­”ÍˆÍ
    public AudioSource audioSource;

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    void Update()
    {

        EmitSound();

    }

    public void EmitSound()
    {
        // ‰¹‚ğÄ¶
        audioSource.Play();

        // ”ÍˆÍ“à‚Ì“G‚ğŒŸo‚µ‚Ä’Ê’m
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
        // ‰¹‚Ì”ÍˆÍ‚ğ‰Â‹‰»
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, soundRange);
    }

}
