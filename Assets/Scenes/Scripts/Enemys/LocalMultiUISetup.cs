using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.InputSystem;

public class LocalMultiUISetup : MonoBehaviour
{
    [SerializeField] private PlayerInputManager _playerInputManager;

    // �v���C���[����UI���
    [Serializable]
    private struct PlayerUIInfo
    {
        public GameObject playerRoot;
        public GameObject firstSelected;
    }

    [SerializeField] private PlayerUIInfo[] _playerUIInfo;

    // �����C�x���g�̓o�^�E����
    private void Awake() => _playerInputManager.onPlayerJoined += OnPlayerJoined;
    private void OnDestroy() => _playerInputManager.onPlayerJoined -= OnPlayerJoined;

    // �v���C���[�����������Ƃ��̏���
    private void OnPlayerJoined(PlayerInput playerInput)
    {
        // Multiplayer Event System���擾
        if (!playerInput.TryGetComponent(out MultiplayerEventSystem eventSystem))
        {
            // Multiplayer Event System���A�^�b�`����Ă��Ȃ��ꍇ�͒ǉ�
            eventSystem = playerInput.gameObject.AddComponent<MultiplayerEventSystem>();
        }

        // �v���C���[�����擾
        if (playerInput.playerIndex >= _playerUIInfo.Length)
        {
            Debug.LogError("���蓖�ĉ\�ȃv���C���[��񂪂���܂���B");
            return;
        }

        var playerUiInfo = _playerUIInfo[playerInput.playerIndex];

        // UI����ݒ�
        eventSystem.playerRoot = playerUiInfo.playerRoot;
        eventSystem.firstSelectedGameObject = playerUiInfo.firstSelected;
    }
}
