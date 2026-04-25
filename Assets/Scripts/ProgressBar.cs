using UnityEngine;

float currentProgress;
float maxProgress;
float multiplier;
public class ProgressBar : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentProgress = 0;
        maxProgress = 100;
        multiplier = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    float getProgress()
    {
        return currentProgress;
    }
    void gameResultHandler(float pointAmmount)
    {
        pointAmmount /= 10;
        if (pointAmmount <= 33)
        {
            pointAmmount *= multiplier;
            currentProgress -= pointAmmount;
        }else if (pointAmmount <= 66)
        {
            currentProgress += pointAmmount;
        }
        else
        {
            pointAmmount *= multiplier;
            currentProgress += pointAmmount;
        }
    }

    void randomEventsHandler(bool isNegative)
    {
        if (isNegative)
        {
            currentProgress -= 5;
        }
        else
        {
            currentProgress += 5;
        }
    }
}
