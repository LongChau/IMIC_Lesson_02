using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    [CreateAssetMenu(fileName = "EnemyDataAsset", menuName = "TowerDefense/DataAsset/EnemyDataAsset")]
    public class EnemySO : ScriptableObject
    {
        public float speed;
        public int health;
    }
}
