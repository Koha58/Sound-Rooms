using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadOfKeyScript : MonoBehaviour
{
    public float speed;
    public float maxDistanceFromParent = 5f; // 親オブジェクトとの最大距離
    public float returnDistance = 2f; // 親オブジェクトの元に戻る際の距離閾値
    private GameObject[] targets;
    private bool isSwitch = false;

    private GameObject closeEnemy;
    private bool isAtTarget = false; // 目的地（敵）に到達したかどうか

    private void Start()
    {
        // タグを使って画面上の全ての敵の情報を取得
        targets = GameObject.FindGameObjectsWithTag("EnemyG");

        // 「初期値」の設定
        float closeDist = 1000;

        foreach (GameObject t in targets)
        {
            // このオブジェクトと敵までの距離を計測
            float tDist = Vector3.Distance(transform.position, t.transform.position);

            // もしも「初期値」よりも「計測した敵までの距離」の方が近いならば、
            if (closeDist > tDist)
            {
                // 「closeDist」を「tDist（その敵までの距離）」に置き換える。
                closeDist = tDist;

                // 一番近い敵の情報をcloseEnemyという変数に格納する（★）
                closeEnemy = t;
            }
        }

        // 砲弾が生成されて0.5秒後に、一番近い敵に向かって移動を開始する。
        Invoke("SwitchOn", 0.5f);
    }

    void Update()
    {
        if (isSwitch)
        {
            if (isAtTarget)
            {
                // 目的地（敵）に到達した場合
                HandleReturnToParent();
            }
            else
            {
                // 親オブジェクトとの距離を計算
                float distanceFromParent = Vector3.Distance(transform.position, transform.parent.position);

                // 親オブジェクトとの距離が最大距離を超えていないかチェック
                if (distanceFromParent > maxDistanceFromParent)
                {
                    // 最大距離を超えていたら、親オブジェクトに近づける
                    Vector3 directionToParent = (transform.parent.position - transform.position).normalized;
                    transform.position = Vector3.MoveTowards(transform.position, transform.parent.position, speed * Time.deltaTime);
                }
                else
                {
                    // 親オブジェクトとの距離が許容範囲内なら、目的地（敵）に向かって移動
                    float step = speed * Time.deltaTime;
                    Vector3 targetPosition = closeEnemy.transform.position;

                    // y座標を固定
                    targetPosition.y = transform.position.y;

                    // x, z座標を移動
                    transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

                    // 目的地に到達した場合
                    if (Vector3.Distance(transform.position, closeEnemy.transform.position) <= 0.1f)
                    {
                        isAtTarget = true; // 目的地に到達
                    }
                }
            }
        }
    }

    void SwitchOn()
    {
        isSwitch = true;
    }

    // 親オブジェクトの元に戻る処理
    void HandleReturnToParent()
    {
        float distanceFromParent = Vector3.Distance(transform.position, transform.parent.position);

        // 親オブジェクトとの距離が閾値よりも遠ければ戻る
        if (distanceFromParent > returnDistance)
        {
            Vector3 directionToParent = (transform.parent.position - transform.position).normalized;
            transform.position = Vector3.MoveTowards(transform.position, transform.parent.position, speed * Time.deltaTime);
        }
        else
        {
            // 親オブジェクトに近づいたら目的地への移動を再開
            isAtTarget = false;
        }
    }
}
