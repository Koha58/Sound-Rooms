using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;      

public class PlayerController : MonoBehaviour
{
    private GameInputSystem gameInputSystem;

    private void Awake()
    {
        gameInputSystem = new GameInputSystem();
        gameInputSystem.Enable();
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    private void FixedUpdate()//0.02秒ごとに呼ばれる
    {
        //ラジオ置く
        if (gameInputSystem.Player.PutOn.triggered)
        {
            Debug.Log("置く！！");
        }

       gameInputSystem.Player.MoveController.performed += MoveController_performed;

    }

    private void MoveController_performed(InputAction.CallbackContext obj)
    {
        Debug.Log("しゃがむ");
        throw new System.NotImplementedException();
    }
}
