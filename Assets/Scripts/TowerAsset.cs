using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    [CreateAssetMenu(fileName = "TowerAsset", menuName = "ConfigData/TowerAsset")]
    public class TowerAsset : ScriptableObject
    {
        public Tower towerData;
    }
}
