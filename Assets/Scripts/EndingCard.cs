using System;
using System.Collections;
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

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        // Get or auto-add a CanvasGroup to control visibility
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();

        // Hide the card and place it off-screen before anything is shown
        canvasGroup.alpha = 0f;
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, 1080f);
    }

    void Start()
    {
        citizenNo = setCitizenNo();
        CitizenNumber.text = citizenNo.ToString();
        finalScore = ProgressBar.getProgress();
        grade = getGradeFromScore();
        int rounded = (int)MathF.Round(finalScore);
        PlayerScore.text = rounded.ToString();
        resolution = getResolution();
        Resolution.text = resolution;
        ScoreGrade.text = grade.ToString();
    }

    void Update()
    {
        
    }

    public void SlideIn()
    {
        StartCoroutine(SlideInCoroutine());
    }

    private IEnumerator SlideInCoroutine()
    {
        float duration = 4f;
        float elapsed = 0f;

        Vector2 startPos = new Vector2(rectTransform.anchoredPosition.x, 1080f);
        Vector2 endPos   = new Vector2(rectTransform.anchoredPosition.x, 0f);

        rectTransform.anchoredPosition = startPos;
        canvasGroup.alpha = 1f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            float smoothT = 1f - Mathf.Pow(1f - t, 3f);

            rectTransform.anchoredPosition = Vector2.LerpUnclamped(startPos, endPos, smoothT);
            yield return null;
        }

        rectTransform.anchoredPosition = endPos;
    }

    char getGradeFromScore()
    {
        if (finalScore <= 16.66f) return 'F';
        else if (finalScore <= 33.33f) return 'E';
        else if (finalScore <= 50f)    return 'D';
        else if (finalScore <= 66.66f) return 'C';
        else if (finalScore <= 83.33f) return 'B';
        else return 'A';
    }

    String getResolution()
    {
        if (finalScore <= 33f)  return "EXECUTE";
        else if (finalScore <= 66f) return "REHABILITATE";
        else return "EXTEND TESTS";
    }

    private int setCitizenNo()
    {
        return Random.Range(10000, 99999);
    }
}
