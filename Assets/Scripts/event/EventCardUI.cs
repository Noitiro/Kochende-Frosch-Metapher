using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventCardUI : MonoBehaviour
{
    public Image iconImage;
    public TMP_Text nameText;
    public TMP_Text storyText;

    public CanvasGroup leftPanel;
    public CanvasGroup rightPanel;

    public void LoadCardData(EventCardSO card)
    {
        if (card != null)
        {
            if (iconImage != null) iconImage.sprite = card.Icon;

            nameText.text = card.EventName;
            storyText.text = card.Story;

            leftPanel.GetComponentInChildren<TMP_Text>().text = card.LeftOption;
            rightPanel.GetComponentInChildren<TMP_Text>().text = card.RightOption;

            ResetAlphas();
        }
    }

    public void UpdateDragVisuals(float progress)
    {
        if (progress < 0)
        {
            leftPanel.alpha = Mathf.Abs(progress);
            rightPanel.alpha = 0;
        }
        else if (progress > 0)
        {
            rightPanel.alpha = progress;
            leftPanel.alpha = 0;
        }
        else
        {
            ResetAlphas();
        }
    }

    public void ResetAlphas()
    {
        if (leftPanel != null) leftPanel.alpha = 0;
        if (rightPanel != null) rightPanel.alpha = 0;
    }
}