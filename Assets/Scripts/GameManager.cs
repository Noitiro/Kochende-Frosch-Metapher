using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour{
    [SerializeField] private int numberOfDays;
    [SerializeField] private Collider2D tv;
    [SerializeField] private GameObject tvEvent;
    [SerializeField] private GameObject randomEvent;
    [SerializeField] private EndingCard EndingCard;
    private GrandmaMovement grandmaMovement;
    private TVMinigame tvMinigame;
    private EventManger eventManger;
    private int counterDay;
    private int counterRandomEvent;
    private int counterMinigame;

    [SerializeField] private float time;
    
    IEnumerator Wait(){
        yield return new WaitForSeconds(time);
    }

    void Start(){
        counterMinigame = 1;
        counterRandomEvent = 1;
        counterDay = 1;
        grandmaMovement = GetComponent<GrandmaMovement>();
        tvMinigame = tvEvent.GetComponent<TVMinigame>();
        eventManger = randomEvent.GetComponent<EventManger>();
    }
    void Update(){
        if(tvMinigame._isFinished == true && counterMinigame == counterDay){
            grandmaMovement.isWaiting = false;
            counterMinigame++;
            Debug.Log(counterMinigame);
        }
        if(eventManger.isFinishRandomEvent == true && counterRandomEvent == counterDay){
            grandmaMovement.isWaiting = false;
            counterRandomEvent++;
            endGame();
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
    void endGame(){
        EndingCard.generateEndCard();
        Debug.Log("END GAME");
    }
}
