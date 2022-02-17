using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class GameManager : MonoBehaviour
    {
        [Header("Tower Config:")]
        [SerializeField]
        private TowerDataSO[] _towerDataConfigs;

        [Space(30)]

        [SerializeField]
        private UserDataSO _userDataConfig;

        [SerializeField]
        private UserData _userData;

        [SerializeField]
        private bool _isDontDestroyOnLoad;

        private static GameManager _instance;

        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<GameManager>();
                }

                if (_instance == null)
                {
                    GameObject newGameObj = new GameObject();
                    _instance = newGameObj.AddComponent<GameManager>();
                }

                return _instance;
            }
        }

        public long MapInstanceId { get; set; }
        public UserData UserData => _userData;

        private void Awake()
        {
            _isDontDestroyOnLoad = true;
            gameObject.name = "GameManager";

            if (_instance != null)
            {
                Destroy(gameObject);
                _instance = null;
            }
            else
            {
                _instance = this;
            }

            if (_isDontDestroyOnLoad)
                DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            _userData = new UserData(_userDataConfig);
        }

        public void ModifyGold(int gold)
        {
            _userData.Gold += gold;
        }

        public TowerDataSO GetTowerConfig(GameDefine.ETowerType towerType, int level = 1)
        {
            foreach (var configData in _towerDataConfigs)
            {
                if (configData.TowerType == towerType && configData.Level == level)
                    return configData;
            }

            return null;
        }
    }
}
