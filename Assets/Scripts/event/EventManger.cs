using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class EventManger : MonoBehaviour
{
    [Header("Ustawienia Talii")]
    public GameObject cardPrefab;
    public Transform cardParent;

    public List<EventCardSO> masterDeck;
    private List<EventCardSO> playingDeck;

    [Header("Ustawienia Rozgrywki")]
    public int maxEvents = 5;
    private int eventsPlayed = 0;

    [Header("Zewn�trzne Systemy")]
    public GameObject progressBarObject;

    private GameObject currentCardObject;
    private EventCardSO currentCardData;

    void Update()
    {

    }

    public void StartEventSequence()
    {
        playingDeck = new List<EventCardSO>(masterDeck);
        eventsPlayed = 0;

        ShuffleDeck();
        LoadNextCard();
    }

    private void ShuffleDeck()
    {
        for (int i = 0; i < playingDeck.Count; i++)
        {
            EventCardSO temp = playingDeck[i];
            int randomIndex = Random.Range(i, playingDeck.Count);
            playingDeck[i] = playingDeck[randomIndex];
            playingDeck[randomIndex] = temp;
        }
    }

    private void LoadNextCard()
    {
        if (currentCardObject != null) Destroy(currentCardObject);

        if (playingDeck.Count > 0 && eventsPlayed < maxEvents)
        {
            currentCardData = playingDeck[0];
            playingDeck.RemoveAt(0);

            currentCardObject = Instantiate(cardPrefab, cardParent);

            EventCardUI ui = currentCardObject.GetComponent<EventCardUI>();
            EventSwipe swipe = currentCardObject.GetComponent<EventSwipe>();

            ui.LoadCardData(currentCardData);

            swipe.OnSwipedLeft += HandleLeftDecision;
            swipe.OnSwipedRight += HandleRightDecision;
            swipe.OnDragProgress += ui.UpdateDragVisuals;
            swipe.OnDragCanceled += ui.ResetAlphas;

            eventsPlayed++;
        }
        else
        {
            Debug.Log("Koniec sekwencji event�w!");
        }
    }

    private void HandleLeftDecision()
    {
        Debug.Log("Lewo!");

        if (progressBarObject != null)
        {
            progressBarObject.SendMessage("randomEventsHandler", currentCardData.isLeftOptionNegative, SendMessageOptions.DontRequireReceiver);
        }

        Invoke(nameof(LoadNextCard), 0.5f);
    }

    private void HandleRightDecision()
    {
        Debug.Log("Prawo!");

        if (progressBarObject != null)
        {
            progressBarObject.SendMessage("randomEventsHandler", currentCardData.isRightOptionNegative, SendMessageOptions.DontRequireReceiver);
        }

        Invoke(nameof(LoadNextCard), 0.5f);
    }
}