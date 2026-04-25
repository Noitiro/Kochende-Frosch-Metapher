using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class EndingCard : MonoBehaviour
{
    private float finalScore;
    private char grade;
    private int citizenNo;
    private string resolution;
    [SerializeField] private TextMeshProUGUI CitizenNumber;
    [SerializeField] private TextMeshProUGUI PlayerScore;
    [SerializeField] private TextMeshProUGUI Resolution;
    [SerializeField] private TextMeshProUGUI ScoreGrade;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        citizenNo = setCitizenNo();
        CitizenNumber.text = citizenNo.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        finalScore = ProgressBar.getProgress();
        grade = getGradeFromScore();
        int rounded = (int)MathF.Round(finalScore); 
        PlayerScore.text = rounded.ToString();
        resolution = getResolution();
        Resolution.text = resolution;
    }

    char getGradeFromScore()
    {
        if (finalScore <= 16.66f)
        {
            return 'F';
        }else if (finalScore <= 33.33f)
        {
            return 'E';
        }else if (finalScore <= 50f)
        {
            return 'D';
        }else if (finalScore <= 66.66f)
        {
            return 'C';
        }else if (finalScore <= 83.33f)
        {
            return 'B';
        }
        else
        {
            return 'A';
        }
    }

    String getResolution()
    {
        if (finalScore <= 33f)
        {
            return "EXECUTE";
        }else if (finalScore <= 66f)
        {
            return "REHABILITATE";
        }
        else
        {
            return "EXTEND TESTS";
        }
    }

    private int setCitizenNo()
    {
        return Random.Range(10000, 99999);
    }
}
