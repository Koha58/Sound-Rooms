using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPointManager : MonoBehaviour
{
    // �e�G�̏���|�C���g���Ǘ�����f�B�N�V���i��
    private Dictionary<int, List<Transform>> patrolPoints = new Dictionary<int, List<Transform>>();

    // ����|�C���g��ǉ�����֐�
    public void AddPatrolPoints(int enemyID, List<Transform> points)
    {
        if (!patrolPoints.ContainsKey(enemyID))
        {
            patrolPoints.Add(enemyID, points);
        }
    }

    // �w�肵��ID�̏���|�C���g���擾����֐�
    public List<Transform> GetPatrolPoints(int enemyID)
    {
        if (patrolPoints.ContainsKey(enemyID))
        {
            return patrolPoints[enemyID];
        }
        return null;
    }
}
