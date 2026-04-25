using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class EndingCard : MonoBehaviour
{
    private float finalScore;
    private char grade;
    private int citizenNo;
    [SerializeField] private TextMeshProUGUI CitizenNumber;
    [SerializeField] private TextMeshProUGUI PlayerScore;
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
    }

    char getGradeFromScore()
    {
        if (finalScore <= 16.66)
        {
            return 'F';
        }else if (finalScore <= 33.33)
        {
            return 'E';
        }else if (finalScore <= 50)
        {
            return 'D';
        }else if (finalScore <= 66.66)
        {
            return 'C';
        }else if (finalScore <= 83.33)
        {
            return 'B';
        }
        else
        {
            return 'A';
        }
    }

    private int setCitizenNo()
    {
        return Random.Range(10000, 99999);
    }
}
