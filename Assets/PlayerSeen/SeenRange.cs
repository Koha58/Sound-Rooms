using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//SeenAreaのサイズ変更
public class SeenRange : MonoBehaviour
{
    private float originSizeX;
    private float originSizeY = 2.0f;
    private float originSizeZ = 2.0f;

    // 変更前の値
    private int preHeight;
    //敵を倒すごとに増加する変数
    private float plusSize;

    LevelMeter levelMeter;


    // Start is called before the first frame update
    void Start()
    {
        preHeight = 0;
        plusSize = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject soundobj = GameObject.Find("SoundVolume");
        levelMeter = soundobj.GetComponent<LevelMeter>(); //付いているスクリプトを取得

        if (preHeight != EnemyAttack.enemyDeathcnt)
        {
            plusSize += 1.0f;

            preHeight++;
        }

        originSizeX = (levelMeter.nowdB * 10) + plusSize;

        transform.localScale = new Vector3(originSizeX, originSizeY, originSizeZ);

    }
}
