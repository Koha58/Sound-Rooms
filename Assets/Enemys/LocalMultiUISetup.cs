using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.InputSystem;

public class LocalMultiUISetup : MonoBehaviour
{
    [SerializeField] private PlayerInputManager _playerInputManager;

    // プレイヤー毎のUI情報
    [Serializable]
    private struct PlayerUIInfo
    {
        public GameObject playerRoot;
        public GameObject firstSelected;
    }

    [SerializeField] private PlayerUIInfo[] _playerUIInfo;

    // 入室イベントの登録・解除
    private void Awake() => _playerInputManager.onPlayerJoined += OnPlayerJoined;
    private void OnDestroy() => _playerInputManager.onPlayerJoined -= OnPlayerJoined;

    // プレイヤーが入室したときの処理
    private void OnPlayerJoined(PlayerInput playerInput)
    {
        // Multiplayer Event Systemを取得
        if (!playerInput.TryGetComponent(out MultiplayerEventSystem eventSystem))
        {
            // Multiplayer Event Systemがアタッチされていない場合は追加
            eventSystem = playerInput.gameObject.AddComponent<MultiplayerEventSystem>();
        }

        // プレイヤー情報を取得
        if (playerInput.playerIndex >= _playerUIInfo.Length)
        {
            Debug.LogError("割り当て可能なプレイヤー情報がありません。");
            return;
        }

        var playerUiInfo = _playerUIInfo[playerInput.playerIndex];

        // UI情報を設定
        eventSystem.playerRoot = playerUiInfo.playerRoot;
        eventSystem.firstSelectedGameObject = playerUiInfo.firstSelected;
    }
}
