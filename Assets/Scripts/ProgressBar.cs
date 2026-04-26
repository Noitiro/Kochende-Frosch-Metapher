using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private float fillSpeed = 1.5f;

    static float currentProgress;
    float maxProgress;
    float multiplier;

    void Start()
    {
        currentProgress = 0;
        maxProgress = 100;
        multiplier = 1;

        if (fillImage != null)
            fillImage.fillAmount = 0f;
    }

    void Update()
    {
        // Prevent going out of bounds
        currentProgress = Mathf.Clamp(currentProgress, 0, maxProgress);

        float targetFill = currentProgress / maxProgress;

        // Smoothly animate fill left -> right at a constant rate
        if (fillImage != null)
            fillImage.fillAmount = Mathf.MoveTowards(
                fillImage.fillAmount,
                targetFill,
                fillSpeed * Time.deltaTime
            );
    }

    public static float getProgress()
    {
        return currentProgress;
    }

    public void gameResultHandler(float pointAmmount)
    {
        pointAmmount /= 10;
        if (pointAmmount <= 3.3f)
        {
            pointAmmount *= multiplier;
            currentProgress -= pointAmmount;
            multiplier = 1;
        }
        else if (pointAmmount <= 6.6f)
        {
            currentProgress += pointAmmount;
            multiplier = 1;
        }
        else
        {
            pointAmmount *= multiplier;
            currentProgress += pointAmmount;
            multiplier += 0.4f;
        }
    }

    void randomEventsHandler(bool isNegative)
    {
        if (isNegative)
            currentProgress -= 5;
        else
            currentProgress += 5;
    }
}