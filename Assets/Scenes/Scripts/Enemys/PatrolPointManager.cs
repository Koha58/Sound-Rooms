using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPointManager : MonoBehaviour
{
    public Transform[] patrolPoints;  // ����|�C���g�̔z��

    // ���݂̏���|�C���g�C���f�b�N�X
    private int currentPatrolIndex = 0;

    // �L�����N�^�[ID�Ɋ�Â��ď���|�C���g���擾
    public Transform GetNextPatrolPoint(int characterID)
    {
        if (patrolPoints.Length == 0)
        {
            return null;
        }

        // �L�����N�^�[ID�𖳎����āA�P���Ɏ��̏���|�C���g���擾
        // ���̕��@�ł́A���ׂẴL�����N�^�[���������Ԃŏ��񂷂�悤�ɂȂ�܂�
        Transform nextPatrolPoint = patrolPoints[currentPatrolIndex];

        // ���̏���|�C���g�Ɉړ����邽�߁A�C���f�b�N�X���X�V
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;

        return nextPatrolPoint;
    }
}
