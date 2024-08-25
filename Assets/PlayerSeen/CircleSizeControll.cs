using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//�}�E�X����������(�͈͎w��)
public class CircleSizeControll : MonoBehaviour
{
    public GameObject MaxSound;//���̍L����̍ő�l
    public GameObject Sound; //���̍L����̉~

    //���̍L����̍ő�l�̃T�C�Y
    private float originSizemX = 10.3f;
    private float originSizemZ = 10.3f;
    //���̍L����̃T�C�Y
    private float originSizeX = 2.3f;
    private float originSizeZ = 2.3f;

    LevelMeter levelMeter;

    // �ύX�O�̒l
    private int preHeight;
    //�G��|�����Ƃɑ�������ϐ�
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
        levelMeter = soundobj.GetComponent<LevelMeter>(); //�t���Ă���X�N���v�g���擾

        if (levelMeter.nowdB > 0.0f)
        {
            if (preHeight != EnemyAttack.enemyDeathcnt)
            {
                plusSize += 1.0f;

                preHeight++;
            }

            Debug.Log(plusSize);

            originSizeX = (levelMeter.nowdB * 10) + plusSize;
            originSizeZ = (levelMeter.nowdB * 10) + plusSize;

            Sound.transform.localScale = new Vector3(originSizeX, 1, originSizeZ);

            originSizemX = 10.3f + plusSize;
            originSizemZ = 10.3f + plusSize;

            MaxSound.transform.localScale = new Vector3(originSizemX, 1, originSizemZ);

            Sound.SetActive(true);
            MaxSound.SetActive(true);
        }
        else
        {
            Sound.SetActive(false);
            MaxSound.SetActive(false);
        }
    }
}
