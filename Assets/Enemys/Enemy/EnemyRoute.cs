using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoute : MonoBehaviour
{
    public List<Transform> enemy1RoutePoint; // Enemy1�p�̃��[�g
    public List<Transform> enemy2Route2Point; // Enemy2�p�̃��[�g

    private void Start()
    {
        // Enemy1�̃��[�g��o�^
        GameManager.instance.RegisterRoute(1, enemy1RoutePoint);

        // Enemy2�̃��[�g��o�^
        GameManager.instance.RegisterRoute(2, enemy2Route2Point);
    }
}
