using RadioMinigame;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class RadioMinigameLogic : MonoBehaviour
{

    [SerializeField] private Button setButton;
    [SerializeField] private GameObject radioPanel;
    [SerializeField] private WaveDisplay waveDisplay;
    [SerializeField] private KnobLogic amplitudeKnob;
    [SerializeField] private KnobLogic frequencyKnob;

    public GameObject progressBarObject;
    public bool isFinished;
    
    
    public void StartRadioMinigame()
    {
        isFinished = false;
        radioPanel.SetActive(true);
        SetObjectsLocked(true);
        waveDisplay.OnWaveSwitched += OnWaveSwitched;
        waveDisplay.InitWave();
    }

    public void OnButtonClicked()
    {
        CalculatePoints();
        isFinished = true;
        radioPanel.SetActive(false);
    }

    private void CalculatePoints() {
        float randomWaveAmp = waveDisplay.RandomWaveAmplitude;
        float randomWaveFreq = waveDisplay.RandomWaveFrequency;
        float chosenWaveAmp = amplitudeKnob.CurrentValue;
        float chosenWaveFreq = frequencyKnob.CurrentValue;

        float ampScore = (1f - (Mathf.Clamp01(Mathf.Abs(randomWaveAmp - chosenWaveAmp) / 30f))) * 50f;
        float freqScore = (1f - Mathf.Clamp01(Mathf.Abs(randomWaveFreq - chosenWaveFreq) / 2f)) * 50f;
        float score = ampScore + freqScore + 55;
        
        Debug.Log("Score for radio minigame is: " + score);
        progressBarObject.GetComponent<ProgressBar>().gameResultHandler(score);
    }

    private void OnWaveSwitched()
    {
        SetObjectsLocked(false);
        waveDisplay.OnWaveSwitched -= OnWaveSwitched;
    }

    private void SetObjectsLocked(bool locked)
    {
        amplitudeKnob.SetLocked(locked);
        frequencyKnob.SetLocked(locked);
        setButton.interactable = !locked;
    }
    // void Update()
    // {
    //     
    // }
}
