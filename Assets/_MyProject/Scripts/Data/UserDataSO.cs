using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TowerDefense
{
    [CreateAssetMenu(fileName = "UserDataSO", menuName = "TowerDefense/DataAsset/UserDataSO")]
    public class UserDataSO : ScriptableObject
    {
        [SerializeField]
        private int _gold;

        public int Gold { get => _gold; set => _gold = value; }
    }

    [Serializable]
    public class UserData
    {
        [SerializeField]
        private int _gold;
        public int Gold
        {
            get => _gold;
            set
            {
                _gold = value;
                EventManager.Instance.Event_OnUserGoldChanged?.Invoke();
            }
        }

        public bool IsInitialized { get; private set; }

        public UserData(UserDataSO config)
        {
            Gold = config.Gold;

            IsInitialized = true;
        }
    }
}
