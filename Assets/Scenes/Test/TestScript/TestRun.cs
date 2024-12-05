using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class TestRun : MonoBehaviour
{
    //�ړ��p�̕ϐ�
    float x, z;

    //�����X�s�[�h�����p�̕ϐ�
    float Walkspeed = 1f/100;
    //����X�s�[�h�����p�̕ϐ�
    float Runspeed = 2f / 50;

    Animator Animator;

    // �ő�̉�]�p���x[deg/s]
    [SerializeField] private float _maxAngularSpeed = Mathf.Infinity;

    // �i�s�����Ɍ����̂ɂ����邨���悻�̎���[s]
    [SerializeField] private float _smoothTime = 0.1f;

    private Transform _transform;

    // �O�t���[���̃��[���h�ʒu
    private Vector3 _prevPosition;

    bool Move;

    private Transform CamPos;
    private Vector3 Camforward;
    private Vector3 ido;
    private Vector3 Animdir = Vector3.zero;

    float runspeed = 0.2f;


    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();   //�A�j���[�^�[�R���g���[���[����A�j���[�V�������擾����

        _transform = transform;

        _prevPosition = _transform.position;

        if (Camera.main != null)
        {
            CamPos = Camera.main.transform;
        }
        else
        {
            Debug.LogWarning(
    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Idle();
        Controller();
        if (Move == true)
        {
            Rotation();
            PlayerWalk();
            if (Input.GetKey("joystick button 5")) { PlayerRun(); }
        }
    }


    private void Idle()
    {
        if(Input.GetAxisRaw("Horizontal")==0&&Input.GetAxisRaw("Vertical")==0)
        {
            Animator.SetBool("Walking", false);
            Animator.SetBool("Running", false);
            Animator.SetBool("Squatting", false);
            Animator.SetBool("CrouchWalking", false);
            Move = false;
        }
        else {Move = true;}
    }

    private void PlayerWalk()
    {
        x = Input.GetAxisRaw("Horizontal") * Walkspeed;
        z = Input.GetAxisRaw("Vertical") * Walkspeed;
        Animator.SetBool("Walking", true);
        Animator.SetBool("Running", false);
        transform.position += new Vector3(x*-1 , 0, z);
    }

    private void PlayerRun()
    {
        x = Input.GetAxisRaw("Horizontal") * Runspeed;
        z = Input.GetAxisRaw("Vertical") * Runspeed;
        Animator.SetBool("Walking", false);
        Animator.SetBool("Running", true);

        transform.position += new Vector3(x*-1, 0, z);
    }

    private void Rotation()
    {
        // ���݃t���[���̃��[���h�ʒu
        var position = _transform.position;

        // �ړ��ʂ��v�Z
        var delta = position - _prevPosition;

        // ����Update�Ŏg�����߂̑O�t���[���ʒu�X�V
        _prevPosition = position;

        // �Î~���Ă����Ԃ��ƁA�i�s���������ł��Ȃ����߉�]���Ȃ�
        if (delta == Vector3.zero)
            return;

        // �i�s�����i�ړ��ʃx�N�g���j�Ɍ����悤�ȃN�H�[�^�j�I�����擾
        var rotation = Quaternion.LookRotation(delta, Vector3.up);

        // �I�u�W�F�N�g�̉�]�ɔ��f
        _transform.rotation = rotation;
    }

    private void Controller()
    {
        //�L�[�{�[�h���l�擾�B�v���C���[�̕����Ƃ��Ĉ���
        float h = Input.GetAxis("Horizontal");//��
        float v = Input.GetAxis("Vertical");//�c

        //�J������Transform���擾����Ă�Ύ��s
        if (CamPos != null)
        {
            //2�̃x�N�g���̊e�����̏�Z(Vector3.Scale)�B�P�ʃx�N�g����(.normalized)
            Camforward = Vector3.Scale(CamPos.forward, new Vector3(1, 0, 1)).normalized;
            //�ړ��x�N�g����ido�Ƃ����g�����X�t�H�[���ɑ��
            ido = (v*-1) * Camforward * runspeed + h * CamPos.right * runspeed;
            //Debug.Log(ido);
        }

        //���݂̃|�W�V������ido�̃g�����X�t�H�[���̐��l������
        transform.position = new Vector3(
        transform.position.x + ido.x,
        0,
        transform.position.z + ido.z);

        //�����]���pTransform

        Vector3 AnimDir = ido;
        AnimDir.y = 0;
        //�����]��
        if (AnimDir.sqrMagnitude > 0.001)
        {
            Vector3 newDir = Vector3.RotateTowards(transform.forward, AnimDir, 5f * Time.deltaTime, 0f);
            transform.rotation = Quaternion.LookRotation(newDir);
        }
    }
}
