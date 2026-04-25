using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour{
    [SerializeField] private int numberOfDays;
    [SerializeField] private Collider2D tv;
    [SerializeField] private GameObject tvEvent;

    public int counterMinigame;
    void Start(){
        counterMinigame = 1;
    }
    void Update(){
        if(counterMinigame <= numberOfDays)
        {
            counterMinigame++;
        }
        else{
            //end game
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.name == "Tv") {
            startTv();
        }
    }
    void startTv()
    {
        Debug.Log ("Start TV minigame");
        tvEvent.GetComponent<EventManger>().StartEventSequence();
        // tvEvent.EventManger.StartEventSequence;
    }
}
