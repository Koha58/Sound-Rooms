using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�A�C�e���擾
public class DoorCheck : MonoBehaviour
{
    Animator anim;
    LevelMeter levelMeter;

    bool OnOff;

    GameObject Rote;

    public float rotateAngle;
    public float rotateSpeed;

    public bool Right;

    [SerializeField] AudioSource AutoDoorSound;

    [SerializeField] AudioSource RollingDoorSound;

    ParticleSystem EF;

    void Start()
    {
        //�ŏ��͌����Ȃ����
        GetComponent<Collider>().enabled = false;
        OnOff = false;
        Right = false;

        AutoDoorSound = GetComponent<AudioSource>();

        GameObject RotationDoorEffect = GameObject.Find("Dark Lvl 1");
        EF = RotationDoorEffect.GetComponent<ParticleSystem>();
        EF.Stop();
    }

    // Update is called once per frame
    private void Update()
    {
        GameObject soundobj = GameObject.Find("SoundVolume");
        levelMeter = soundobj.GetComponent<LevelMeter>(); //�t���Ă���X�N���v�g���擾

        GameObject RotationDoorEffect = GameObject.Find("Dark Lvl 1");
        EF = RotationDoorEffect.GetComponent<ParticleSystem>();

        //�����o�����Ƃŉ���
        if (levelMeter.nowdB > 0.0f)
        {
            GetComponent<Collider>().enabled = true;//������i�L���j
            OnOff = true;
        }

        if (OnOff == true)
        {
            if (levelMeter.nowdB == 0.0f)
            {
                GetComponent<Collider>().enabled = false;//�����Ȃ��i�����j
                OnOff = false;
                EF.Stop();
                RollingDoorSound.Stop();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AutoDoor"))
        {
            anim = other.GetComponent<Animator>();
            anim.SetBool("Open", true);
            AutoDoorSound.PlayOneShot(AutoDoorSound.clip);
        }

        GameObject RotationDoorEffect = GameObject.Find("Dark Lvl 1");
        EF = RotationDoorEffect.GetComponent<ParticleSystem>();

        if (other.CompareTag("Right"))
        {
            EF.Play();
            RollingDoorSound.PlayOneShot(RollingDoorSound.clip);
        }
        else if (other.CompareTag("Left") && !Right)
        {
            EF.Play();
            RollingDoorSound.PlayOneShot(RollingDoorSound.clip);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Right"))
        {
            Rote = other.transform.parent.gameObject;
            Rote.transform.Rotate(0, -rotateAngle * Time.deltaTime * rotateSpeed, 0 );
            Right = true;
        }
        else if (other.CompareTag("Left") && !Right)
        {
            Rote = other.transform.parent.gameObject;
            Rote.transform.Rotate(0, rotateAngle * Time.deltaTime * rotateSpeed, 0);
            Right = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject RotationDoorEffect = GameObject.Find("Dark Lvl 1");
        EF = RotationDoorEffect.GetComponent<ParticleSystem>();
        if (other.CompareTag("Right"))
        {
            EF.Stop();
            Rote = other.transform.parent.gameObject;
            Rote.transform.Rotate(0, -rotateAngle * Time.deltaTime * rotateSpeed, 0);
            Right = false;
        }
    }
}
