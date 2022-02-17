using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class TowerController : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _spriteRender;
        private TowerDataSO _towerConfig;

        public TowerDataSO TowerConfig 
        { 
            get => _towerConfig;
            set
            {
                _towerConfig = value;
                _spriteRender.sprite = _towerConfig.TowerPrefab.GetComponent<SpriteRenderer>().sprite;
            }
        }

        private void OnValidate()
        {
            _spriteRender = GetComponent<SpriteRenderer>();
        }

        // Start is called before the first frame update
        void Start()
        {
            
        }

        public void Sell()
        {
            Debug.Log("Sold");
            GameManager.Instance.ModifyGold(+_towerConfig.SellCost);
            Destroy(gameObject);
        }
    }
}
