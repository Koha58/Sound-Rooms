using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

[CreateAssetMenu]
public class EnemyDataBase : ScriptableObject//変化しない値を登録しておくのに使うデータベース
{ 
        public List<Enemy> ItemList = new List<Enemy>();
}