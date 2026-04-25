using UnityEngine;

public class EndingCard : MonoBehaviour
{
    private float finalScore;
    private char grade;
    private int citizenNo;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        finalScore = ProgressBar.getProgress();
        grade = getGradeFromScore();
        citizenNo = setCitizenNo();
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
