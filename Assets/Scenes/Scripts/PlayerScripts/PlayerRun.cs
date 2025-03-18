using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InputDeviceManager;

/// <summary>
/// Player�̈ړ����Ǘ�����N���X
/// </summary>

public class PlayerRun : MonoBehaviour
{
    public int moving = 0; // �ړ������ǂ����������t���O�i0�͒�~�A1�͈ړ����j
    [SerializeField]
    float moveSpeedIn; // �v���C���[�̈ړ����x��ݒ肷��ϐ�

    private Animator animator; // �v���C���[�̃A�j���[�^�[�i�A�j���[�V�����𐧌�j

    Rigidbody playerRb; // �v���C���[��Rigidbody�i�������Z���s�����߁j

    Vector3 moveSpeed; // �v���C���[�̈ړ����x�i�x�N�g���`���j

    Vector3 currentPos; // �v���C���[�̌��݈ʒu

    Vector3 pastPos; // �v���C���[�̉ߋ��̈ʒu�i�ړ��̑O���r�p�j

    Vector3 delta; // �v���C���[�̈ړ��ʁi�ʒu�̕ω��ʁj

    Quaternion playerRot; // �v���C���[�̐i�s�����������N�H�[�^�j�I���i��]��\���j

    float currentAngularVelocity; // �v���C���[�̌��݂̉�]�p���x

    [SerializeField]
    float maxAngularVelocity = Mathf.Infinity; // �ő�̉�]�p���x[deg/s]�i�����l�j

    [SerializeField]
    float smoothTime = 0.1f; // �i�s�����ɂ����邨���悻�̎���[s]�i��]�����炩�ɂ���j

    float diffAngle; // ���݂̌����Ɛi�s�����̊p�x��

    float rotAngle; // ���݂̉�]����p�x

    Quaternion nextRot; // ���ɉ�]����p�x�i��ԂɎg���j

    public static bool walk = false; // �����Ă��邩�ǂ����������t���O�i�f�t�H���g�͕����Ă��Ȃ��j

    public static bool run = false; // �����Ă��邩�ǂ����������t���O�i�f�t�H���g�͑����Ă��Ȃ��j

    public static bool crouch = false; // ���Ⴊ��ł��邩�ǂ����������t���O�i�f�t�H���g�͂��Ⴊ��ł��Ȃ��j

    public bool crouchWalk = false; // ���Ⴊ��ŕ����Ă��邩�ǂ����������t���O�i���Ⴊ�݂Ȃ���̕��s�j

    private Rigidbody rb; // �v���C���[��Rigidbody�i�������Z���s�����߁j

    // ���Ⴊ�ނƂ�
    public static bool CrouchOn = false; // ���Ⴊ�ݏ�Ԃ��Ǘ�����t���O�i���Ⴊ�ݒ��Ȃ�true�j



    void Start()
    {
        playerRb = GetComponent<Rigidbody>(); // Rigidbody�R���|�[�l���g���擾���āA��������������
        animator = GetComponent<Animator>(); // �A�j���[�^�[�R���g���[���[���擾���ăA�j���[�V�����𐧌�
        pastPos = transform.position; // �v���C���[�̏����ʒu���L�^

        Application.targetFrameRate = 60; // �A�v���P�[�V�����̃^�[�Q�b�g�t���[�����[�g��60�ɐݒ�i���炩�ȓ����j

        rb = GetComponent<Rigidbody>(); // Rigidbody�R���|�[�l���g���ēx�擾�i���ʂȏd���ł����A�ʂ̗p�r�̉\������j
    }

    void Update()
    {
        // Animation�Ǘ��i�v���C���[�̃A�j���[�V������Ԃ��X�V�j
        Animation();

        // �ړ����\�b�h�ŁA�v���C���[�ɗ͂�������
        Move();

        // �����̊Ǘ��₵�Ⴊ�ݏ�Ԃ�����
        Inertia();

        // �v���C���[�̉�]�����i�i�s�����ɍ��킹�ĉ�]�j
        Rotation();
    }

    /// <summary>
    /// �v���C���[�̈ړ������ɗ͂������鏈��
    /// </summary>
    private void Move()
    {
        // �����Ă���ꍇ�A�ʏ�̈ړ����x�ŗ͂�������
        if (walk == true)
        {
            playerRb.velocity = moveSpeed; // �������x�ňړ��ivelocity��ݒ肵�ė͂�������j
        }

        // �����Ă���ꍇ�A���x��2�{�ɂ��ė͂�������
        if (run == true)
        {
            playerRb.velocity = moveSpeed * 2; // ����Ƃ��͈ړ����x��{�ɂ���
        }

        // ���Ⴊ�݂Ȃ�������ꍇ�A�ړ����x��1/5�ɂ��ė͂�������
        if (crouchWalk == true)
        {
            playerRb.velocity = moveSpeed / 5; // ���Ⴊ��ł���Ƃ��̈ړ����x��x������
        }
    }


    /// <summary>
    /// Animation�Ǘ�
    /// </summary>
    private void Animation()
    {
        //------�v���C���[�̈ړ�------

        //�J�����ɑ΂��đO���擾
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        //�J�����ɑ΂��ĉE���擾
        Vector3 cameraRight = Vector3.Scale(Camera.main.transform.right, new Vector3(1, 0, 1)).normalized;

        //moveVelocity��0�ŏ���������
        moveSpeed = Vector3.zero;

        //�����Ƃ�
        if (!crouch && Input.GetKey(KeyCode.W)) //�O�����Ɉړ�
        {
            moveSpeed = moveSpeedIn * cameraForward;
            animator.SetBool("Walking", true);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 1;
            walk = true;
            run = false;
            crouch = false;
            crouchWalk = false;
            CrouchOn = false;
        }

        if (!crouch && Input.GetKey(KeyCode.A)) //�������Ɉړ�
        {
            moveSpeed = -moveSpeedIn * cameraRight;
            animator.SetBool("Walking", true);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 1;
            walk = true;
            run = false;
            crouch = false;
            crouchWalk = false;
            CrouchOn = false;
        }

        if (!crouch && Input.GetKey(KeyCode.S)) //������Ɉړ�
        {
            moveSpeed = -moveSpeedIn * cameraForward;
            animator.SetBool("Walking", true);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 1;
            walk = true;
            run = false;
            crouch = false;
            crouchWalk = false;
            CrouchOn = false;
        }

        if (!crouch && Input.GetKey(KeyCode.D)) //�E�����Ɉړ�
        {
            moveSpeed = moveSpeedIn * cameraRight;
            animator.SetBool("Walking", true);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 1;
            walk = true;
            run = false;
            crouch = false;
            crouchWalk = false;
            CrouchOn = false;
        }

        if (!crouch && (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))) //�E�������Ɉړ�
        {
            moveSpeed = (-moveSpeedIn * cameraForward) + (moveSpeedIn * cameraRight).normalized;
            animator.SetBool("Walking", true);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 1;
            walk = true;
            run = false;
            crouch = false;
            crouchWalk = false;
        }
        if (!crouch && (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))) //�E�O�����Ɉړ�
        {
            moveSpeed = (moveSpeedIn * cameraForward) + (moveSpeedIn * cameraRight).normalized;
            animator.SetBool("Walking", true);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 1;
            walk = true;
            run = false;
            crouch = false;
            crouchWalk = false;
        }
        if (!crouch && (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))) //���O�����Ɉړ�
        {
            moveSpeed = (moveSpeedIn * cameraForward) + (-moveSpeedIn * cameraRight).normalized;
            animator.SetBool("Walking", true);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 1;
            walk = true;
            run = false;
            crouch = false;
            crouchWalk = false;
        }
        if (!crouch && (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))) //���O�����Ɉړ�
        {
            moveSpeed = (-moveSpeedIn * cameraForward) + (-moveSpeedIn * cameraRight).normalized;
            animator.SetBool("Walking", true);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 1;
            walk = true;
            run = false;
            crouch = false;
            crouchWalk = false;
        }


        //�����Ƃ�(Xbox)
        if (!crouch && Input.GetAxisRaw("Vertical") < 0) //�O�����Ɉړ�
        {
            moveSpeed = moveSpeedIn * cameraForward;
            animator.SetBool("Walking", true);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 1;
            walk = true;
            run = false;
            crouch = false;
            crouchWalk = false;
            CrouchOn = false;
        }

        if (!crouch && Input.GetAxisRaw("Horizontal") < 0) //�������Ɉړ�
        {
            moveSpeed = -moveSpeedIn * cameraRight;
            animator.SetBool("Walking", true);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 1;
            walk = true;
            run = false;
            crouch = false;
            crouchWalk = false;
            CrouchOn = false;
        }

        if (!crouch && Input.GetAxisRaw("Vertical") > 0) //������Ɉړ�
        {
            moveSpeed = -moveSpeedIn * cameraForward;
            animator.SetBool("Walking", true);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 1;
            walk = true;
            run = false;
            crouch = false;
            crouchWalk = false;
            CrouchOn = false;
        }

        if (!crouch && Input.GetAxisRaw("Horizontal") > 0) //�E�����Ɉړ�
        {
            moveSpeed = moveSpeedIn * cameraRight;
            animator.SetBool("Walking", true);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 1;
            walk = true;
            run = false;
            crouch = false;
            crouchWalk = false;
            CrouchOn = false;
        }

        if (!crouch && (Input.GetAxisRaw("Horizontal") > 0 && Input.GetAxisRaw("Vertical") > 0)) //�E�������Ɉړ�
        {
            moveSpeed = (-moveSpeedIn * cameraForward) + (moveSpeedIn * cameraRight).normalized;
            animator.SetBool("Walking", true);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 1;
            walk = true;
            run = false;
            crouch = false;
            crouchWalk = false;
        }
        if (!crouch && (Input.GetAxisRaw("Horizontal") > 0 && Input.GetAxisRaw("Vertical") < 0)) //�E�O�����Ɉړ�
        {
            moveSpeed = (moveSpeedIn * cameraForward) + (moveSpeedIn * cameraRight).normalized;
            animator.SetBool("Walking", true);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 1;
            walk = true;
            run = false;
            crouch = false;
            crouchWalk = false;
        }
        if (!crouch && (Input.GetAxisRaw("Horizontal") < 0 && Input.GetAxisRaw("Vertical") < 0)) //���O�����Ɉړ�
        {
            moveSpeed = (moveSpeedIn * cameraForward) + (-moveSpeedIn * cameraRight).normalized;
            animator.SetBool("Walking", true);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 1;
            walk = true;
            run = false;
            crouch = false;
            crouchWalk = false;
        }
        if (!crouch && (Input.GetAxisRaw("Horizontal") < 0 && Input.GetAxisRaw("Vertical") > 0))
        {
            moveSpeed = (-moveSpeedIn * cameraForward) + (-moveSpeedIn * cameraRight).normalized;
            animator.SetBool("Walking", true);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 1;
            walk = true;
            run = false;
            crouch = false;
            crouchWalk = false;
        }

        //����Ƃ�
        if (!crouch && Input.GetKey(KeyCode.W) && Input.GetMouseButton(1)) //�O�����Ɉړ�
        {
            moveSpeed = moveSpeedIn * cameraForward;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", true);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 1;
            run = true;
            walk = false;
            crouch = false;
            crouchWalk = false;
        }

        if (!crouch && Input.GetKey(KeyCode.A) && Input.GetMouseButton(1)) //�������Ɉړ�
        {
            moveSpeed = -moveSpeedIn * cameraRight;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", true);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 1;
            run = true;
            walk = false;
            crouch = false;
            crouchWalk = false;
        }

        if (!crouch && Input.GetKey(KeyCode.S) && Input.GetMouseButton(1)) //������Ɉړ�
        {
            moveSpeed = -moveSpeedIn * cameraForward;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", true);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 1;
            run = true;
            walk = false;
            crouch = false;
            crouchWalk = false;
        }

        if (!crouch && Input.GetKey(KeyCode.D) && Input.GetMouseButton(1)) //�E�����Ɉړ�
        {
            moveSpeed = moveSpeedIn * cameraRight;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", true);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 1;
            run = true;
            walk = false;
            crouch = false;
            crouchWalk = false;
        }

        if (!crouch && (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)) && Input.GetMouseButton(1)) //�E�������Ɉړ�
        {
            moveSpeed = (-moveSpeedIn * cameraForward) + (moveSpeedIn * cameraRight).normalized;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", true);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 1;
            run = true;
            walk = false;
            crouch = false;
            crouchWalk = false;
        }
        if (!crouch && (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D)) && Input.GetMouseButton(1)) //�E�O�����Ɉړ�
        {
            moveSpeed = (moveSpeedIn * cameraForward) + (moveSpeedIn * cameraRight).normalized;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", true);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 1;
            run = true;
            walk = false;
            crouch = false;
            crouchWalk = false;
        }
        if (!crouch && (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A)) && Input.GetMouseButton(1)) //���O�����Ɉړ�
        {
            moveSpeed = (moveSpeedIn * cameraForward) + (-moveSpeedIn * cameraRight).normalized;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", true);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 1;
            run = true;
            walk = false;
            crouch = false;
            crouchWalk = false;
        }
        if (!crouch && (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A)) && Input.GetMouseButton(1))
        {
            moveSpeed = (-moveSpeedIn * cameraForward) + (-moveSpeedIn * cameraRight).normalized;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", true);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 1;
            run = true;
            walk = false;
            crouch = false;
            crouchWalk = false;

        }

        //����Ƃ�(Xbox)
        if (!crouch && Input.GetAxisRaw("Vertical") < 0 && Input.GetKey("joystick button 5")) //�O�����Ɉړ�
        {
            moveSpeed = moveSpeedIn * cameraForward;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", true);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 1;
            run = true;
            walk = false;
            crouch = false;
            crouchWalk = false;
        }

        if (!crouch && Input.GetAxisRaw("Horizontal") < 0 && Input.GetKey("joystick button 5")) //�������Ɉړ�
        {
            moveSpeed = -moveSpeedIn * cameraRight;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", true);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 1;
            run = true;
            walk = false;
            crouch = false;
            crouchWalk = false;
        }

        if (!crouch && Input.GetAxisRaw("Vertical") > 0 && Input.GetKey("joystick button 5")) //������Ɉړ�
        {
            moveSpeed = -moveSpeedIn * cameraForward;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", true);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 1;
            run = true;
            walk = false;
            crouch = false;
            crouchWalk = false;
        }

        if (!crouch && Input.GetAxisRaw("Horizontal") > 0 && Input.GetKey("joystick button 5")) //�E�����Ɉړ�
        {
            moveSpeed = moveSpeedIn * cameraRight;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", true);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 1;
            run = true;
            walk = false;
            crouch = false;
            crouchWalk = false;
        }

        if (!crouch && (Input.GetAxisRaw("Horizontal") > 0 && Input.GetAxisRaw("Vertical") > 0) && Input.GetKey("joystick button 5")) //�E�������Ɉړ�
        {
            moveSpeed = (-moveSpeedIn * cameraForward) + (moveSpeedIn * cameraRight).normalized;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", true);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 1;
            run = true;
            walk = false;
            crouch = false;
            crouchWalk = false;
        }
        if (!crouch && (Input.GetAxisRaw("Horizontal") > 0 && Input.GetAxisRaw("Vertical") < 0) && Input.GetKey("joystick button 5")) //�E�O�����Ɉړ�
        {
            moveSpeed = (moveSpeedIn * cameraForward) + (moveSpeedIn * cameraRight).normalized;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", true);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 1;
            run = true;
            walk = false;
            crouch = false;
            crouchWalk = false;
        }
        if (!crouch && (Input.GetAxisRaw("Horizontal") < 0 && Input.GetAxisRaw("Vertical") < 0) && Input.GetKey("joystick button 5")) //���O�����Ɉړ�
        {
            moveSpeed = (moveSpeedIn * cameraForward) + (-moveSpeedIn * cameraRight).normalized;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", true);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 1;
            run = true;
            walk = false;
            crouch = false;
            crouchWalk = false;
        }
        if (!crouch && (Input.GetAxisRaw("Horizontal") < 0 && Input.GetAxisRaw("Vertical") > 0) && Input.GetKey("joystick button 5"))
        {
            moveSpeed = (-moveSpeedIn * cameraForward) + (-moveSpeedIn * cameraRight).normalized;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", true);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 1;
            run = true;
            walk = false;
            crouch = false;
            crouchWalk = false;
        }

        //���Ⴊ�ݕ���
        if (crouch && Input.GetKey(KeyCode.W)) //�O�����Ɉړ�
        {
            moveSpeed = moveSpeedIn * cameraForward;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", true);
            moving = 1;
            walk = false;
            run = false;
            crouchWalk = true;
        }

        if (crouch && Input.GetKey(KeyCode.A)) //�������Ɉړ�
        {
            moveSpeed = -moveSpeedIn * cameraRight;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", true);
            moving = 1;
            walk = false;
            run = false;
            crouchWalk = true;
        }

        if (crouch && Input.GetKey(KeyCode.S)) //������Ɉړ�
        {
            moveSpeed = -moveSpeedIn * cameraForward;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", true);
            moving = 1;
            walk = false;
            run = false;
            crouchWalk = true;
        }

        if (crouch && Input.GetKey(KeyCode.D)) //�E�����Ɉړ�
        {
            moveSpeed = moveSpeedIn * cameraRight;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", true);
            moving = 1;
            walk = false;
            run = false;
            crouchWalk = true;
        }


        if (CrouchOn == true && !crouch && (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))) //�E�������Ɉړ�
        {
            moveSpeed = (-moveSpeedIn * cameraForward) + (moveSpeedIn * cameraRight).normalized;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", true);
            moving = 1;
            run = false;
            walk = false;
            crouch = false;
            crouchWalk = true;
        }
        if (CrouchOn == true && !crouch && (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))) //�E�O�����Ɉړ�
        {
            moveSpeed = (moveSpeedIn * cameraForward) + (moveSpeedIn * cameraRight).normalized;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", true);
            moving = 1;
            run = false;
            walk = false;
            crouch = false;
            crouchWalk = true;
        }
        if (CrouchOn == true && !crouch && (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))) //���O�����Ɉړ�
        {
            moveSpeed = (moveSpeedIn * cameraForward) + (-moveSpeedIn * cameraRight).normalized;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", true);
            moving = 1;
            run = false;
            walk = false;
            crouch = false;
            crouchWalk = true;
        }
        if (CrouchOn == true && !crouch && (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A)))
        {
            moveSpeed = (-moveSpeedIn * cameraForward) + (-moveSpeedIn * cameraRight).normalized;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", true);
            moving = 1;
            run = false;
            walk = false;
            crouch = false;
            crouchWalk = true;

        }


        //���Ⴊ�ݕ���(Xbox)
        if (crouch && Input.GetAxisRaw("Vertical") < 0) //�O�����Ɉړ�
        {
            moveSpeed = moveSpeedIn * cameraForward;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", true);
            moving = 1;
            run = false;
            walk = false;
            crouch = false;
            crouchWalk = true;
        }

        if (crouch && Input.GetAxisRaw("Horizontal") < 0) //�������Ɉړ�
        {
            moveSpeed = -moveSpeedIn * cameraRight;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", true);
            moving = 1;
            run = true;
            walk = false;
            crouch = false;
            crouchWalk = true;
        }

        if (crouch && Input.GetAxisRaw("Vertical") > 0) //������Ɉړ�
        {
            moveSpeed = -moveSpeedIn * cameraForward;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", true);
            moving = 1;
            run = false;
            walk = false;
            crouch = false;
            crouchWalk = true;
        }

        if (crouch && Input.GetAxisRaw("Horizontal") > 0) //�E�����Ɉړ�
        {
            moveSpeed = moveSpeedIn * cameraRight;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", true);
            moving = 1;
            run = false;
            walk = false;
            crouch = false;
            crouchWalk = true;
        }

        if (CrouchOn == true && !crouch && (Input.GetAxisRaw("Horizontal") > 0 && Input.GetAxisRaw("Vertical") > 0)) //�E�������Ɉړ�
        {
            moveSpeed = (-moveSpeedIn * cameraForward) + (moveSpeedIn * cameraRight).normalized;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", true);
            moving = 1;
            run = false;
            walk = false;
            crouch = false;
            crouchWalk = true;
        }
        if (CrouchOn == true && !crouch && (Input.GetAxisRaw("Horizontal") > 0 && Input.GetAxisRaw("Vertical") < 0)) //�E�O�����Ɉړ�
        {
            moveSpeed = (moveSpeedIn * cameraForward) + (moveSpeedIn * cameraRight).normalized;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", true);
            moving = 1;
            run = false;
            walk = false;
            crouch = false;
            crouchWalk = true;
        }
        if (CrouchOn == true && !crouch && (Input.GetAxisRaw("Horizontal") < 0 && Input.GetAxisRaw("Vertical") < 0)) //���O�����Ɉړ�
        {
            moveSpeed = (moveSpeedIn * cameraForward) + (-moveSpeedIn * cameraRight).normalized;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", true);
            moving = 1;
            run = false;
            walk = false;
            crouch = false;
            crouchWalk = true;
        }
        if (CrouchOn == true && !crouch && (Input.GetAxisRaw("Horizontal") < 0 && Input.GetAxisRaw("Vertical") > 0))
        {
            moveSpeed = (-moveSpeedIn * cameraForward) + (-moveSpeedIn * cameraRight).normalized;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", true);
            moving = 1;
            run = false;
            walk = false;
            crouch = false;
            crouchWalk = true;
        }
    }

    /// <summary>
    /// �����A���Ⴊ�񂾍ۂ̓����̒���
    /// </summary>
    private void Inertia()
    {
        //����������
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D)
            || Input.GetKeyUp("left shift") || Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") == 0)
        {
            playerRb.velocity = Vector3.zero;
            playerRb.angularVelocity = Vector3.zero;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 0;
            walk = false;
            run = false;
            crouch = false;
            crouchWalk = false;
        }

        //���Ⴊ�ނƂ�
        if (Input.GetKey("left shift"))
        {
            animator.SetBool("Walking", false);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", true);
            moving = 0;
            walk = false;
            run = false;
            crouch = true;

            CrouchOn = true;
        }
        //���Ⴊ�ނƂ�(Xbox)
        if (Input.GetKey("joystick button 4"))//LB
        {
            animator.SetBool("Walking", false);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", true);
            moving = 0;
            walk = false;
            run = false;
            crouch = true;

            CrouchOn = true;
        }
    }

    /// <summary>
    /// Player�̉�]
    /// </summary>
    private void Rotation()
    {
        //------�v���C���[�̉�]------

        //���݂̈ʒu
        currentPos = transform.position;

        //�ړ��ʌv�Z
        delta = currentPos - pastPos;
        delta.y = 0;

        //�ߋ��̈ʒu�̍X�V
        pastPos = currentPos;

        if (delta == Vector3.zero)
            return;

        playerRot = Quaternion.LookRotation(delta, Vector3.up);

        diffAngle = Vector3.Angle(transform.forward, delta);

        //Vector3.SmoothDamp��Vector3�^�̒l�����X�ɕω�������
        //Vector3.SmoothDamp (���ݒn, �ړI�n, ref ���݂̑��x, �J�ڎ���, �ō����x);
        rotAngle = Mathf.SmoothDampAngle(0, diffAngle, ref currentAngularVelocity, smoothTime, maxAngularVelocity);

        nextRot = Quaternion.RotateTowards(transform.rotation, playerRot, rotAngle);

        transform.rotation = nextRot;
    }
    
}
