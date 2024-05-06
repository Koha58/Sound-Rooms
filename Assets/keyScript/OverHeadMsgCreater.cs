using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverHeadMsgCreater : MonoBehaviour
{
    [SerializeField]
    RectTransform canvasRect;//メッセージのTextを保持させたい親Canvasを登録

    [SerializeField]
    OverHeadMsg overHeadMsgPrefab;//頭上に表示するメッセージPrefabを登録

    OverHeadMsg overHeadMsg;

    public void OnEnable()
    {
        overHeadMsg = Instantiate(overHeadMsgPrefab, canvasRect);
        overHeadMsg.targetTran = transform;
    }

    public void OnDisable()
    {
        if (overHeadMsg != null)
        {
            overHeadMsg.gameObject.SetActive(false);
        }
    }
}
