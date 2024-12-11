using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoute : MonoBehaviour
{
    public List<Transform> enemy1RoutePoint; // Enemy1用のルート
    public List<Transform> enemy2Route2Point; // Enemy2用のルート
    public List<Transform> enemy3Route3Point; // Enemy3用のルート


    private void Start()
    {
        // Enemy1のルートを登録
        GameManager.instance.RegisterRoute(1, enemy1RoutePoint);

        //// Enemy2のルートを登録
        GameManager.instance.RegisterRoute(2, enemy2Route2Point);

        //// Enemy3のルートを登録
        GameManager.instance.RegisterRoute(3, enemy3Route3Point);
    }
}
