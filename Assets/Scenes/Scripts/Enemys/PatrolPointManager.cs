using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemy�̏�����Ǘ�����N���X
/// </summary>
public class PatrolPointManager : MonoBehaviour
{
    // �e�G�̏���|�C���g���Ǘ�����f�B�N�V���i���B
    // �G��ID�ienemyID�j���L�[�Ƃ��āA�Ή����鏄��|�C���g�iTransform�̃��X�g�j���i�[�B
    private Dictionary<int, List<Transform>> patrolPoints = new Dictionary<int, List<Transform>>();

    // ����|�C���g��ǉ�����֐�
    // enemyID: �G�̎��ʎq
    // points: �G�����񂷂�n�_��Transform���X�g
    public void AddPatrolPoints(int enemyID, List<Transform> points)
    {
        // patrolPoints�ɓG��ID�����ɑ��݂��邩�m�F
        if (!patrolPoints.ContainsKey(enemyID))
        {
            // �܂����݂��Ȃ��ꍇ�A�V�����ǉ�
            patrolPoints.Add(enemyID, points);
        }
    }

    // �w�肵��ID�̏���|�C���g���擾����֐�
    // enemyID: �擾�������G�̎��ʎq
    // �GID�ɑΉ����鏄��|�C���g�̃��X�g��Ԃ�
    public List<Transform> GetPatrolPoints(int enemyID)
    {
        // patrolPoints�ɓG��ID�����݂��邩�m�F
        if (patrolPoints.ContainsKey(enemyID))
        {
            // ���݂���ꍇ�A����ID�ɑΉ����鏄��|�C���g���X�g��Ԃ�
            return patrolPoints[enemyID];
        }
        // �GID��������Ȃ������ꍇ�Anull��Ԃ�
        return null;
    }
}
