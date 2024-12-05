using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputActionExample : MonoBehaviour
{
    // InputActionAssetへの参照
    [SerializeField] private InputActionReference _moveAction;

    // コールバックの登録・解除
    private void Awake()
    {
        // 入力値が0以外の値に変化したときに呼び出されるコールバック
        _moveAction.action.performed += OnMove;

        // 入力値が0に戻ったときに呼び出されるコールバック
        _moveAction.action.canceled += OnMove;
    }

    private void OnDestroy()
    {
        _moveAction.action.performed -= OnMove;
        _moveAction.action.canceled -= OnMove;
    }

    // InputActionの有効化・無効化
    private void OnEnable() => _moveAction.action.Enable();
    private void OnDisable() => _moveAction.action.Disable();

    // コールバックの実装
    private void OnMove(InputAction.CallbackContext context)
    {
        // 2軸入力を受け取る
        var move = context.ReadValue<Vector2>();

        // 2軸入力の値を表示
        print($"move: {move}");
    }
}
