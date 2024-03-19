using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverHeadMsgCreater : MonoBehaviour
{
    [SerializeField]
    RectTransform canvasRect;//���b�Z�[�W��Text��ێ����������eCanvas��o�^

    [SerializeField]
    OverHeadMsg overHeadMsgPrefab;//����ɕ\�����郁�b�Z�[�WPrefab��o�^

    OverHeadMsg overHeadMsg;

    void OnEnable()
    {
        overHeadMsg = Instantiate(overHeadMsgPrefab, canvasRect);
        overHeadMsg.targetTran = transform;
    }

    void OnDisable()
    {
        if (overHeadMsg != null)
        {
            Destroy(overHeadMsg.gameObject);
        }
    }
}
