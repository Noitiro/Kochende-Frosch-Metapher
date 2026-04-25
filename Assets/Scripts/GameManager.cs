using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour{
    [SerializeField] private int numberOfDays;
    [SerializeField] private Collider2D tv;
    [SerializeField] private GameObject tvEvent;
    [SerializeField] private GameObject randomEvent;


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
        if(other.name == "event") {
            startEvent();
        }
    }
    void startTv()
    {
        Debug.Log ("Start TV minigame");
        tvEvent.GetComponent<TVMinigame>().StartTVMinigame();
        // tvEvent.EventManger.StartEventSequence;
    }
    void startEvent()
    {
        Debug.Log ("Start random evenet");
        randomEvent.GetComponent<EventManger>().StartEventSequence();
    }
}
