using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour{
    [SerializeField] private int numberOfDays;
    [SerializeField] private Collider2D tv;
    [SerializeField] private GameObject tvEvent;
    [SerializeField] private GameObject randomEvent;
    private GrandmaMovement grandmaMovement;
    private TVMinigame tvMinigame;
    private EventManger eventManger;
    public int counterMinigame;
    void Start(){
        counterMinigame = 1;
        grandmaMovement = GetComponent<GrandmaMovement>();
        tvMinigame = tvEvent.GetComponent<TVMinigame>();
        eventManger = randomEvent.GetComponent<EventManger>();
    }
    void Update(){
        if(counterMinigame <= numberOfDays)
        {
            counterMinigame++;
        }
        else{
            //end game
        }

        if(tvMinigame._isFinished == true || eventManger.isFinishRandomEvent == true){
            grandmaMovement.isWaiting = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.name == "Tv" && tvMinigame._isFinished == false) {
            startTv();
        } else if(other.name == "EventManger") {
            startEvent();
        }
    }
    void startTv(){
        grandmaMovement.isWaiting = true;
        Debug.Log ("Start TV minigame");
        tvMinigame.StartTVMinigame();
    }
    void startEvent(){
        grandmaMovement.isWaiting = true;
        Debug.Log ("Start random evenet");
        randomEvent.GetComponent<EventManger>().StartEventSequence();
    }
}
