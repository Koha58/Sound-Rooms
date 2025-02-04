using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InputDeviceManager;

//�v���C���[�̈ړ�

public class PlayerRun : MonoBehaviour
{
    public int moving = 0;
    [SerializeField]
    float moveSpeedIn;//�v���C���[�̈ړ����x�����
    private Animator animator;

    Rigidbody playerRb;//�v���C���[��Rigidbody

    Vector3 moveSpeed;//�v���C���[�̈ړ����x

    Vector3 currentPos;//�v���C���[�̌��݂̈ʒu
    Vector3 pastPos;//�v���C���[�̉ߋ��̈ʒu

    Vector3 delta;//�v���C���[�̈ړ���

    Quaternion playerRot;//�v���C���[�̐i�s�����������N�H�[�^�j�I��

    float currentAngularVelocity;//���݂̉�]�e���x

    [SerializeField]
    float maxAngularVelocity = Mathf.Infinity;//�ő�̉�]�p���x[deg/s]

    [SerializeField]
    float smoothTime = 0.1f;//�i�s�����ɂ����邨���悻�̎���[s]

    float diffAngle;//���݂̌����Ɛi�s�����̊p�x

    float rotAngle;//���݂̉�]����p�x

    Quaternion nextRot;//�ǂ̂��炢��]���邩

    public static bool walk = false;
    public static bool run = false;
    public static bool crouch = false;
    public bool crouchWalk = false;

    private Rigidbody rb;

    //���Ⴊ�ނƂ�
    public static bool CrouchOn = false;


    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();   //�A�j���[�^�[�R���g���[���[����A�j���[�V�������擾����
        pastPos = transform.position;

        Application.targetFrameRate = 60;

        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //Animation�Ǘ�
        Animation();
        //Move���\�b�h�ŁA�͉����Ă��炤
        Move();

        //�����Ǘ��A���Ⴊ��
        Inertia();

        //Player�̉�]
        Rotation();

    }

    /// <summary>
    /// �ړ������ɗ͂�������
    /// </summary>
    private void Move()
    {
        if (walk == true)
        {
            playerRb.velocity = moveSpeed;
        }

        if (run == true)
        {
            playerRb.velocity = moveSpeed * 2;
        }

        if (crouchWalk == true)
        {
            playerRb.velocity = moveSpeed / 5;
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
        if (!crouch && Input.GetKey(KeyCode.W))
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

        if (!crouch && Input.GetKey(KeyCode.A))
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

        if (!crouch && Input.GetKey(KeyCode.S))
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

        if (!crouch && Input.GetKey(KeyCode.D))
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

        if (!crouch && (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)))
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
        if (!crouch && (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D)))
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
        if (!crouch && (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A)))
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
        if (!crouch && (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)))
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
        if (!crouch && Input.GetAxisRaw("Vertical") < 0)
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

        if (!crouch && Input.GetAxisRaw("Horizontal") < 0)
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

        if (!crouch && Input.GetAxisRaw("Vertical") > 0)
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

        if (!crouch && Input.GetAxisRaw("Horizontal") > 0)
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

        if (!crouch && (Input.GetAxisRaw("Horizontal") > 0 && Input.GetAxisRaw("Vertical") > 0))
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
        if (!crouch && (Input.GetAxisRaw("Horizontal") > 0 && Input.GetAxisRaw("Vertical") < 0))
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
        if (!crouch && (Input.GetAxisRaw("Horizontal") < 0 && Input.GetAxisRaw("Vertical") < 0))
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
        if (!crouch && Input.GetKey(KeyCode.W) && Input.GetMouseButton(1))
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

        if (!crouch && Input.GetKey(KeyCode.A) && Input.GetMouseButton(1))
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

        if (!crouch && Input.GetKey(KeyCode.S) && Input.GetMouseButton(1))
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

        if (!crouch && Input.GetKey(KeyCode.D) && Input.GetMouseButton(1))
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

        if (!crouch && (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)) && Input.GetMouseButton(1))
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
        if (!crouch && (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D)) && Input.GetMouseButton(1))
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
        if (!crouch && (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A)) && Input.GetMouseButton(1))
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
        if (!crouch && Input.GetAxisRaw("Vertical") < 0 && Input.GetKey("joystick button 5"))
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

        if (!crouch && Input.GetAxisRaw("Horizontal") < 0 && Input.GetKey("joystick button 5"))
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

        if (!crouch && Input.GetAxisRaw("Vertical") > 0 && Input.GetKey("joystick button 5"))
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

        if (!crouch && Input.GetAxisRaw("Horizontal") > 0 && Input.GetKey("joystick button 5"))
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

        if (!crouch && (Input.GetAxisRaw("Horizontal") > 0 && Input.GetAxisRaw("Vertical") > 0) && Input.GetKey("joystick button 5"))
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
        if (!crouch && (Input.GetAxisRaw("Horizontal") > 0 && Input.GetAxisRaw("Vertical") < 0) && Input.GetKey("joystick button 5"))
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
        if (!crouch && (Input.GetAxisRaw("Horizontal") < 0 && Input.GetAxisRaw("Vertical") < 0) && Input.GetKey("joystick button 5"))
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
        if (crouch && Input.GetKey(KeyCode.W))
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

        if (crouch && Input.GetKey(KeyCode.A))
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

        if (crouch && Input.GetKey(KeyCode.S))
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

        if (crouch && Input.GetKey(KeyCode.D))
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


        if (CrouchOn == true && !crouch && (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)))
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
        if (CrouchOn == true && !crouch && (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D)))
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
        if (CrouchOn == true && !crouch && (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A)))
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
        if (crouch && Input.GetAxisRaw("Vertical") < 0)
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

        if (crouch && Input.GetAxisRaw("Horizontal") < 0)
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

        if (crouch && Input.GetAxisRaw("Vertical") > 0)
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

        if (crouch && Input.GetAxisRaw("Horizontal") > 0)
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

        if (CrouchOn == true && !crouch && (Input.GetAxisRaw("Horizontal") > 0 && Input.GetAxisRaw("Vertical") > 0))
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
        if (CrouchOn == true && !crouch && (Input.GetAxisRaw("Horizontal") > 0 && Input.GetAxisRaw("Vertical") < 0))
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
        if (CrouchOn == true && !crouch && (Input.GetAxisRaw("Horizontal") < 0 && Input.GetAxisRaw("Vertical") < 0))
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
