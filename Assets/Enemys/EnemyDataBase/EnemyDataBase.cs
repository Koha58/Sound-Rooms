using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

[CreateAssetMenu]
public class EnemyDataBase : ScriptableObject//�ω����Ȃ��l��o�^���Ă����̂Ɏg���f�[�^�x�[�X
{ 
        public List<Enemy> ItemList = new List<Enemy>();
}