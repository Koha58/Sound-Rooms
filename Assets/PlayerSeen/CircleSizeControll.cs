using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//マウス長押し挙動(範囲指定)
public class CircleSizeControll : MonoBehaviour
{
    public GameObject MaxSound;//音の広がりの最大値
    public GameObject Sound; //音の広がりの円

    //音の広がりの最大値のサイズ
    private float originSizemX = 10.3f;
    private float originSizemZ = 10.3f;
    //音の広がりのサイズ
    private float originSizeX = 2.3f;
    private float originSizeZ = 2.3f;

    LevelMeter levelMeter;

    // 変更前の値
    private int preHeight;
    //敵を倒すごとに増加する変数
    private float plusSize;

    // Start is called before the first frame update
    void Start()
    {
        Sound.SetActive(false);
        MaxSound.SetActive(false);

        preHeight = 0;
        plusSize = 0f;

        originSizeX = 2.3f;
        originSizeZ = 2.3f;

        originSizemX = 10.3f;
        originSizemZ = 10.3f;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject soundobj = GameObject.Find("SoundVolume");
    }
}
