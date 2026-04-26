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
        currentProgress = 50f;
        maxProgress = 100f;
        multiplier = 1f;

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

    public void gameResultHandler(float pointAmount)
    {
        pointAmount /= 10;
        if (pointAmount <= 3.3f)
        {
            pointAmount *= multiplier;
            currentProgress -= pointAmount;
            multiplier = 1;
        }
        else if (pointAmount <= 6.6f)
        {
            currentProgress += pointAmount;
            multiplier = 1;
        }
        else
        {
            pointAmount *= multiplier;
            currentProgress += pointAmount;
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