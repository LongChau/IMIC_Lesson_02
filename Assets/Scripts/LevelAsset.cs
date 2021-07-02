﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    [CreateAssetMenu(fileName = "LevelAsset", menuName = "ConfigData/LevelAsset")]
    public class LevelAsset : ScriptableObject
    {
        public Level level;
        public int testGold;
    }

    /// <summary>
    /// 
    /// </summary>
    /// nếu để struct thì sẽ không thay đổi ScriptableObject lúc runtime. 
    /// Còn class thì sẽ bị thay đổi. Nhưng không bị serialize.
    [Serializable]
    public struct Level
    {
        [SerializeField]
        private int _gold;
        [SerializeField]
        private int _life;

        public int Gold { get => _gold; set => _gold = value; }
        public int Life { get => _life; set => _life = value; }
    }
}
