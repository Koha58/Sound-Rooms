using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Enemy�̏���|�C���g��ID�ŊǗ�����N���X
/// </summary>
public class GameManager : MonoBehaviour
{
    public PatrolPointManager patrolPointManager; // PatrolPointManager�ւ̎Q�� (����|�C���g���Ǘ�)
    public Transform[] enemy1PatrolPoints;        // �G1�p�̏���|�C���g
    public Transform[] enemy2PatrolPoints;        // �G2�p�̏���|�C���g
    public Transform[] enemy3PatrolPoints;        // �G3�p�̏���|�C���g
    public Transform[] enemy4PatrolPoints;        // �G4�p�̏���|�C���g
    public Transform[] enemy5PatrolPoints;        // �G5�p�̏���|�C���g
    public Transform[] enemy6PatrolPoints;        // �G6�p�̏���|�C���g
    public Transform[] enemy7PatrolPoints;        // �G7�p�̏���|�C���g
    public Transform[] enemy8PatrolPoints;        // �G8�p�̏���|�C���g

    private void Start()
    {
        // �G1�̏���|�C���g��ǉ�
        patrolPointManager.AddPatrolPoints(1, new List<Transform>(enemy1PatrolPoints));

        // �G2�̏���|�C���g��ǉ�
        patrolPointManager.AddPatrolPoints(2, new List<Transform>(enemy2PatrolPoints));

        // �G3�̏���|�C���g��ǉ�
        patrolPointManager.AddPatrolPoints(3, new List<Transform>(enemy3PatrolPoints));

        // �G4�̏���|�C���g��ǉ�
        patrolPointManager.AddPatrolPoints(4, new List<Transform>(enemy4PatrolPoints));

        // �G5�̏���|�C���g��ǉ�
        patrolPointManager.AddPatrolPoints(5, new List<Transform>(enemy5PatrolPoints));

        // �G6�̏���|�C���g��ǉ�
        patrolPointManager.AddPatrolPoints(6, new List<Transform>(enemy6PatrolPoints));

        // �G7�̏���|�C���g��ǉ�
        patrolPointManager.AddPatrolPoints(7, new List<Transform>(enemy7PatrolPoints));

        // �G8�̏���|�C���g��ǉ�
        patrolPointManager.AddPatrolPoints(8, new List<Transform>(enemy8PatrolPoints));
    }
}
