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
    float Runspeed = 2f / 100;

    Animator Animator;

    // �ő�̉�]�p���x[deg/s]
    [SerializeField] private float _maxAngularSpeed = Mathf.Infinity;

    // �i�s�����Ɍ����̂ɂ����邨���悻�̎���[s]
    [SerializeField] private float _smoothTime = 0.1f;

    private Transform _transform;

    // �O�t���[���̃��[���h�ʒu
    private Vector3 _prevPosition;

    bool Move;

    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();   //�A�j���[�^�[�R���g���[���[����A�j���[�V�������擾����

        _transform = transform;

        _prevPosition = _transform.position;
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
        if (transform.localRotation.y==0)
        {

        }
    }
}
