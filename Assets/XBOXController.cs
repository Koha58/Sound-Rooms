using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class XBOXController : MonoBehaviour
{

    //�ړ��p�̕ϐ�
   // float x, z;

    //�X�s�[�h�����p�̕ϐ�
   // float speed = 0.1f;

    // Start is called before the first frame update

    private IEnumerator Start()
    {
        var gamepad = Gamepad.current;
        if (gamepad == null)
        {
            Debug.Log("�Q�[���p�b�h���ڑ�");
            yield break;
        }

        Debug.Log("�����[�^�[�U��");
        gamepad.SetMotorSpeeds(1.0f, 0.0f);
        yield return new WaitForSeconds(1.0f);

        Debug.Log("�E���[�^�[�U��");
        gamepad.SetMotorSpeeds(0.0f, 1.0f);
        yield return new WaitForSeconds(1.0f);

        Debug.Log("���[�^�[��~");
        gamepad.SetMotorSpeeds(0.0f, 0.0f);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("joystick button 0"))
        {
            Debug.Log("button0");//A
        }
        if (Input.GetKeyDown("joystick button 1"))
        {
            Debug.Log("button1");//B
        }
        if (Input.GetKeyDown("joystick button 2"))
        {
            Debug.Log("button2");//X
        }
        if (Input.GetKeyDown("joystick button 3"))
        {
            Debug.Log("button3");//Y
        }
        if (Input.GetKeyDown("joystick button 4"))
        {
            Debug.Log("button4");//LB
        }
        if (Input.GetKeyDown("joystick button 5"))
        {
            Debug.Log("button5");//RB
        }
        if (Input.GetKeyDown("joystick button 6"))
        {
            Debug.Log("button6");//�r���[ �{�^��
        }
        if (Input.GetKeyDown("joystick button 7"))
        {
            Debug.Log("button7");//���j���[ �{�^�� 
        }
        if (Input.GetKeyDown("joystick button 8"))
        {
            Debug.Log("button8");//L�X�e�B�b�N������
        }
        if (Input.GetKeyDown("joystick button 9"))
        {
            Debug.Log("button9");//R�X�e�B�b�N������
        }

        if (Input.GetKeyDown("joystick button 10"))
        {
            Debug.Log("button10");//
        }
        if (Input.GetKeyDown("joystick button 11"))
        {
            Debug.Log("button11");//
        }

        float hori = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        if ((hori != 0) || (vert != 0))//���X�e�B�b�N
        {
            Debug.Log("stick:" + hori + "," + vert);
        }

        //�ǉ�
        float DpadHorizontal = Input.GetAxis("DpadHorizontal");
        float DpadVertical = Input.GetAxis("DpadVertical");
        if (DpadHorizontal != 0||DpadVertical !=0) //�\���L�[
        {
            Debug.Log("DPad:" + DpadHorizontal + "," + DpadVertical);
        }
        /*��̕ϐ������Ă��炱����Update�ɓ����Γ����܂�
        x = 0;
        z = 0;

        x = Input.GetAxisRaw("Horizontal") * speed;
        z = Input.GetAxisRaw("Vertical") * speed;

        transform.position += new Vector3(x, 0, z);
        */

        //�{�^���̒ǉ����@
        //https://dkrevel.com/unity-explain/input-manager/
        //https://hakonebox.hatenablog.com/entry/2018/04/15/125152#Input-Manager%E3%81%A7%E5%85%A5%E5%8A%9B%E8%A8%AD%E5%AE%9A
        //https://simplestar-tech.hatenablog.com/entry/2017/11/05/181019
    }
}
