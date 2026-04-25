using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class EventCardUI : MonoBehaviour{
    public EventCardSO currentCard;
    public Image iconSprite;
    public TMP_Text nameText;
    public TMP_Text storyText;
    public TMP_Text leftOptionText;
    public TMP_Text rightOptionText;

    void Start()
    {
        UpdateCardUI();
    }

    public void UpdateCardUI()
    {
        if (currentCard != null)
        {

            nameText.text = currentCard.EventName;
            storyText.text = currentCard.Story;
            leftOptionText.text = currentCard.LeftOption;
            rightOptionText.text = currentCard.RightOption;
        }
        else
        {
            Debug.LogWarning("brak karty kurwa");
        }
    }
}