using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGetRandomPosition9 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 GetRandomPosition()
    {
        // ƒ‰ƒ“ƒ_ƒ€‚Èx, y, zÀ•W‚ğ¶¬‚·‚é
        float randomX = Random.Range(-20f, -85f);
        float randomY = 0f;// Random.Range(-10f, 10f);
        float randomZ = Random.Range(-70f, 35f);

        // ¶¬‚µ‚½À•W‚ğ•Ô‚·
        return new Vector3(randomX, randomY, randomZ);
    }
}
