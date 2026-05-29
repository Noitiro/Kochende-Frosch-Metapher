using UnityEngine;

[CreateAssetMenu(menuName = "Radio/WaveConfig")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] private float ampMin = 10f;
    [SerializeField] private float ampMax = 120f;
    [SerializeField] private float freqMin = 2f;
    [SerializeField] private float freqMax = 10f;

    public float AmpMin => ampMin;
    public float AmpMax => ampMax;
    public float FreqMin => freqMin;
    public float FreqMax => freqMax;
}
