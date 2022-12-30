using System;
using System.Linq;
using ProudLlama.CircleGenerator;
using UnityEngine;

    public class Ring : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _radius;
        [SerializeField] private float _duration;
        private float t = 0;
        private StrokeCircleGenerator _circleGenerator;
        private LineRenderer _lineRenderer;
        
        public void SetSpeed(float speed) => _speed = speed;
        
        public void SetRadius(float radius) => _radius = radius;
        public void SetDuration(float duration) => _duration = duration;
        
        private void Start()
        {
            _circleGenerator = GetComponent<StrokeCircleGenerator>();
            _lineRenderer = GetComponent<LineRenderer>();
            SetCircleRadius(_radius * 2);
        }

        private void SetCircleRadius(float radius)
        {
            DrawCircle(radius);
        }


        private void Update(){
            SetCircleRadius(_radius + _duration * _speed);
            _duration -= Time.deltaTime;
        }

        private void DrawCircle(float radius)
        {
            int verticeCount = 100;
            _lineRenderer.positionCount = verticeCount + 1;
            var vertices = Enumerable
                .Range(0, verticeCount + 1) 
                .Select(i =>
                    Mathf.PI * 2 * i /
                    verticeCount) 
                .Select(a =>
                    new Vector3(Mathf.Cos(a), Mathf.Sin(a), 0) *
                    radius / 2)
                .Select(v => transform.position + v)
                .ToArray();
            _lineRenderer.SetPositions(vertices);
        }


        
    }