using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// Xử lý các vấn đề data trong level.
    /// </summary>
    public class GameController : MonoBehaviour
    {
        [SerializeField]
        private LevelAsset _lvlAsset;
        [SerializeField]
        private Level _levelData;

        public int testGold;

        private static GameController _instance;
        public static GameController Instance
        {
            get
            {
                return _instance;
            }
        }

        public Level LevelData { get => _levelData; }

        public event Action<int> Event_UpdateGold;
        public event Action<int> Event_UpdateLife;

        private void Awake()
        {
            _instance = this;
            //_lvlAsset = Instantiate(_lvlAsset);
            _levelData = _lvlAsset.level;
            testGold = _lvlAsset.testGold;
        }

        [ContextMenu("MinusGold")]
        private void MinusGold()
        {
            testGold -= 10;
            _levelData.Gold -= 10;
        }

        // Start is called before the first frame update
        void Start()
        {

        } 

        public void UpdateGold(int gold)
        {
            _levelData.Gold += gold;
            Event_UpdateGold?.Invoke(_levelData.Gold);
        }

        public void UpdateLife(int life)
        {
            _levelData.Life += life;
            Event_UpdateLife?.Invoke(_levelData.Life);
        }
    }
}
