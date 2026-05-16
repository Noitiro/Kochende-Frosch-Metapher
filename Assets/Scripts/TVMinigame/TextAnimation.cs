using System;
using UnityEngine;

public class TextAnimation : MonoBehaviour
{

        private float _textSpeed;
        private RectTransform _rectTransform;

        public void SetTextSpeed(float textSpeed)
        {
                _textSpeed = textSpeed;
        }

        public void SpeedUp(float speedUpValue)
        {
                _textSpeed += speedUpValue;
        }
        
        void Start()
        {
                _rectTransform = GetComponent<RectTransform>();
        }

        void Update()
        {
                _rectTransform.anchoredPosition += new Vector2(-(_textSpeed * Time.deltaTime), 0f);
                
        }
        
}
