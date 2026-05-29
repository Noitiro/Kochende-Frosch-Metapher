using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RadioMinigame
{
    public class KnobLogic : MonoBehaviour, IPointerDownHandler, IDragHandler
    {

        private enum KnobType
        {
           Amplitude,
           Frequency
        }
        
        [SerializeField] private WaveConfig config;
        [SerializeField] private KnobType knobType;
        [SerializeField] private float minAngle = -90f;
        [SerializeField] private float maxAngle = 90f;
        [SerializeField] private WaveDisplay waveDisplay;
        
        private RectTransform _rect;
        private float _currentAngle;
        private Vector2 _lastMousePos;
        private bool _isLocked;
        
        public float CurrentValue { get; private set; }

        void Awake()
        {
            _rect = GetComponent<RectTransform>();
            
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if(_isLocked) return;
            
            _lastMousePos = eventData.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if(_isLocked) return;
            
            Vector2 mouseDelta =
                eventData.position - _lastMousePos;

            // obrót zależny od ruchu myszy
            float rotationSpeed = 0.5f;

            _currentAngle += mouseDelta.x * rotationSpeed;

            // limit
            _currentAngle = Mathf.Clamp(
                _currentAngle,
                minAngle,
                maxAngle
            );

            switch (knobType)
            {
                case KnobType.Amplitude:
                    CurrentValue = MapAngle(_currentAngle, config.AmpMin, config.AmpMax);
                    waveDisplay.SetAmplitude(CurrentValue);
                    break;
                
                case KnobType.Frequency:
                    CurrentValue = MapAngle(_currentAngle, config.FreqMin, config.FreqMax);
                    waveDisplay.SetFrequency(CurrentValue);
                    break;
            }

            _rect.localEulerAngles = new Vector3(0, 0, _currentAngle);

            _lastMousePos = eventData.position;
        }
        

        private float MapAngle(float angle, float minValue, float maxValue)
        {
            float t = Mathf.InverseLerp(minAngle, maxAngle, angle);
            return Mathf.Lerp(minValue, maxValue, t);
        }
        
        public void SetLocked(bool locked)
        {
            _isLocked = locked;
        }
        
    }
}