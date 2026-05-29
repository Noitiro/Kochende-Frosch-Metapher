using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace RadioMinigame
{
    public class WaveDisplay : Graphic
    {
        [System.Serializable]
        private struct WavePreset
        {
            public float amplitude;
            public float frequency;
        }

        [Header("Wave Colors")] 
        [SerializeField] private Color randomWaveColor = Color.cornflowerBlue;
        [SerializeField] private Color targetWaveColor = Color.darkGreen;

        [Header("Config")]
        [SerializeField] private WaveConfig config;
        public event System.Action OnWaveSwitched;
        
        [Header("Wave Presets")] 
        [SerializeField] private WavePreset targetWave;
        [SerializeField] private float switchDelay = 2f;
        
        [Header("Line properties")]
        [SerializeField] private int points = 50;
        [SerializeField] private float amplitude = 10f;
        [SerializeField] private float frequency = 2f;
        [SerializeField] private float waveSpeed = 1f;
        [SerializeField] private float lineThickness = 3f;

        [Header("Wave start and end points")] 
        [SerializeField] [Range(0f, 1f)] private float waveStart = 0.2f;
        [SerializeField] [Range(0f, 1f)] private float waveEnd = 0.8f;
        [SerializeField] private bool smoothEdges = true;
        
        private float _time;
        
        public float RandomWaveAmplitude { get; private set; }
        public float RandomWaveFrequency { get; private set; }

        public void InitWave()
        {
            _time = 0f;
            color = randomWaveColor;
            
            WavePreset randomWave = GenerateRandomWave();
            RandomWaveAmplitude = randomWave.amplitude;
            RandomWaveFrequency = randomWave.frequency;
            
            ApplyPreset(randomWave);
            StartCoroutine(SwitchRoutine());
        }
        
        
        // ReSharper disable commentTypo
        //Animacja - jesli chcemy   
        void Update()
        {
            _time += Time.deltaTime * waveSpeed;
            SetVerticesDirty();
        }

        public void SetAmplitude(float amp)
        {
                amplitude = amp;
        }

        public void SetFrequency(float freq)
        {
            frequency = freq;
        }

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();

            float width = rectTransform.rect.width;
            float height = rectTransform.rect.height;

            for (int i = 0; i < points - 1; i++)
            {
                float progress0 = (float)i / (points - 1);
                float progress1 = (float)(i + 1) / (points - 1);

                float x0 = Mathf.Lerp(-width / 2f, width / 2f, progress0);
                float x1 = Mathf.Lerp(-width / 2f, width / 2f, progress1);

                float y0 = GetY(progress0);
                float y1 = GetY(progress1);

                DrawSegment(vh, new Vector2(x0, y0), new Vector2(x1, y1), lineThickness);
            }
        }
        
        private float GetY(float progress)
        {
            // ReSharper disable commentTypo
            // Jeśli poza zakresem fali → prosta linia
            if (progress <= waveStart || progress >= waveEnd)
                return 0f;
            
            float localProgress = (progress - waveStart) / (waveEnd - waveStart);

            float sineValue = amplitude * Mathf.Sin((2 * Mathf.PI * frequency * localProgress) + _time);

            // ReSharper disable commentTypo
            // Płynne wejście/wyjście żeby nie było "skoku" na krawędziach
            if (smoothEdges)
            {
                float edgeZone = 0.1f;
                float blend = 1f;

                if (localProgress < edgeZone)
                    blend = Mathf.SmoothStep(0f, 1f, localProgress / edgeZone);
                else if (localProgress > 1f - edgeZone)
                    blend = Mathf.SmoothStep(0f, 1f, (1f - localProgress) / edgeZone);

                sineValue *= blend;
            }

            return sineValue;
        }

        private void DrawSegment(VertexHelper vh, Vector2 p0, Vector2 p1, float thickness)
        {
            Vector2 dir = (p1 - p0).normalized;
            Vector2 perp = new Vector2(-dir.y, dir.x) * (thickness / 2f);

            int startIndex = vh.currentVertCount;

            UIVertex vertex = UIVertex.simpleVert;
            vertex.color = color;

            vertex.position = p0 - perp; vh.AddVert(vertex); // 0
            vertex.position = p0 + perp; vh.AddVert(vertex); // 1
            vertex.position = p1 + perp; vh.AddVert(vertex); // 2
            vertex.position = p1 - perp; vh.AddVert(vertex); // 3

            vh.AddTriangle(startIndex, startIndex + 1, startIndex + 2);
            vh.AddTriangle(startIndex, startIndex + 2, startIndex + 3);
        }

        private IEnumerator SwitchRoutine()
        {
            
            yield return new WaitForSeconds(switchDelay);
            color = targetWaveColor;
            ApplyPreset(targetWave);
            OnWaveSwitched?.Invoke();
        }

        private void ApplyPreset(WavePreset preset)
        {
            
            amplitude = preset.amplitude;
            frequency = preset.frequency;
        }

        private WavePreset GenerateRandomWave()
        {
            return new WavePreset
            {
                amplitude = Random.Range(config.AmpMin, config.AmpMax),
                frequency = Random.Range(config.FreqMin, config.FreqMax)
            };
        }
    }
}