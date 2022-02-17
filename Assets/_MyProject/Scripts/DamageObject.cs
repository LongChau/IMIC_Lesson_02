using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class DamageObject : MonoBehaviour
    {
        [SerializeField]
        private float _speed;
        [SerializeField]
        private int _damage;
        [SerializeField]
        private Transform _spriteChild;

        private Vector3 _direction;

        public Vector3 Direction { get => _direction; set => _direction = value; }
        public Transform SpriteChild { get => _spriteChild; set => _spriteChild = value; }
        public int Damage { get => _damage; set => _damage = value; }
        public float Speed { get => _speed; set => _speed = value; }

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            transform.Translate(Direction * _speed * Time.deltaTime);
            var angle = CalculateAngle();
            var newRot = Quaternion.Euler(0f, 0f, angle);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRot, _speed * Time.deltaTime);
            //Vector3.Slerp();
            //transform.rotation = newRot;
            Debug.Log(angle);
        }

        private float CalculateAngle(bool low = false)
        {
            var targetDir = Direction;
            float y = targetDir.y;
            y = 0f;
            float x = targetDir.magnitude;
            float gravity = 9.81f;
            float square = Speed * Speed;
            float underSquareRoot = (float)(Math.Pow(Speed, 4) - gravity * (gravity * Math.Pow(x, 2) + 2 * y * Math.Pow(Speed, 2)));
            if (underSquareRoot >= 0f)
            {
                float root = Mathf.Sqrt(underSquareRoot);
                float highAngle = square + root;
                float lowAngle = square - root;

                if (low) return lowAngle;
                else return highAngle;
            }
            else
                return 0f;
        }

        private void PointToPosition(float t)
        {

        }

        //private void OnBecameInvisible()
        //{
        //    Destroy(gameObject);
        //}
    }
}
