using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class TestRotation : MonoBehaviour
    {
        [SerializeField]
        private float _speed;
        [SerializeField]
        private float _angle;

        [SerializeField]
        private Transform _target;

        [SerializeField]
        private SpriteRenderer _sprite;

        [SerializeField]
        private GameObject _prefab;

        private Quaternion _toRotation;

        private float _length;
        private Vector3 _direction;
        private float _startTime;
        private Vector3 _previousPosition;

        private float _time;
        private bool _isStop;
        public float? _calculatedAngle;

        // Start is called before the first frame update
        void Start()
        {
            _toRotation = Quaternion.Euler(0f, 0f, _angle);
            _direction = _target.position - transform.position;
            _length = _direction.magnitude;
            _startTime = Time.time;

            _calculatedAngle = CalculateAngle();
        }

        // Update is called once per frame
        void Update()
        {
            //transform.Rotate(transform.forward, _speed * Time.deltaTime);
            //var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //mousePos.z = 0f;
            //var direction = (mousePos - transform.position).normalized;
            //_toRotation = Quaternion.FromToRotation(transform.right, direction);
            //transform.rotation = Quaternion.Slerp(transform.rotation, _toRotation, _speed * Time.deltaTime);

            //transform.position = Vector3.Slerp(transform.position, _target.position, _speed * Time.deltaTime);
            //transform.position = Vector3.Lerp(transform.position, _target.position, _speed * Time.deltaTime);

            //transform.rotation = Quaternion.AngleAxis(_speed, Vector3.forward);

            //MoveWithSlerp();
            //MoveWithAngle();
        }

        private void MoveWithAngle()
        {
            _calculatedAngle = CalculateAngle();
            transform.rotation = Quaternion.AngleAxis(_calculatedAngle.Value, Vector3.forward);
        }

        private void MoveWithSlerp()
        {
            var lengthCovered = (Time.time - _startTime) * _speed;
            var fractionLength = lengthCovered / _length;
            _previousPosition = transform.position;
            transform.position = Vector3.Slerp(transform.position, _target.position, fractionLength);

            _time += Time.deltaTime;
            if (_time >= 0.1f && !_isStop)
            {
                Instantiate(_prefab, transform.position, Quaternion.identity);
                _time = 0f;
            }

            _isStop = Vector2.Distance(transform.position, _target.position) <= 0.01f;
        }

        private float? CalculateAngle(bool low = false)
        {
            var targetDir = _target.position - transform.position;
            float y = targetDir.y;
            y = 0f;
            float x = targetDir.magnitude;
            float gravity = 9.81f;
            float square = _speed * _speed;
            float underSquareRoot = (float)(Math.Pow(_speed, 4) - gravity * (gravity * Math.Pow(x, 2) + 2 * y * Math.Pow(_speed, 2)));
            if (underSquareRoot >= 0f)
            {
                float root = Mathf.Sqrt(underSquareRoot);
                float highAngle = square + root;
                float lowAngle = square - root;

                if (low) return lowAngle;
                else return highAngle;
            }
            else
                return null;
        }

        private void LookAtDirection2D(Vector2 direction)
        {
            if (_isStop) return;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        // LateUpdate is called every frame, if the Behaviour is enabled
        private void LateUpdate()
        {
            //var direction = transform.position - _previousPosition;
            //LookAtDirection2D(direction);
        }

        [ContextMenu("Test_FromTo")]
        private void Test_FromTo()
        {
            var rotation = Quaternion.FromToRotation(transform.right, transform.up);
            Debug.Log($"Quaternion: {rotation} vs Euler: {rotation.eulerAngles}");
        }

        [ContextMenu("Test_Rotate")]
        private void Test_Rotate()
        {
            var toRot = Quaternion.Euler(0f, 0f, 45f);
            transform.rotation = toRot;
        }
    }
}
