using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour{
    [SerializeField] private int numberOfDays;
    [SerializeField] private Collider2D tv;
    [SerializeField] private GameObject tvEvent;
    [SerializeField] private GameObject randomEvent;
    [SerializeField] private EndingCard EndingCard;
    [SerializeField] private CameraMovement cameraMovement;
    private GrandmaMovement grandmaMovement;
    private TVMinigame tvMinigame;
    private EventManger eventManger;
    [SerializeField] private int counterDay;
    [SerializeField] private int counterRandomEvent;
    [SerializeField] private int counterMinigame;

    [SerializeField] private float time;
    
    IEnumerator Wait(){
        yield return new WaitForSeconds(time);
    }

    void Start(){
        numberOfDays = 3;
        counterMinigame = 1;
        counterRandomEvent = 1;
        counterDay = 1;
        grandmaMovement = GetComponent<GrandmaMovement>();
        tvMinigame = tvEvent.GetComponent<TVMinigame>();
        eventManger = randomEvent.GetComponent<EventManger>();
    }
    void Update(){
        if(tvMinigame._isFinished == true && counterMinigame == counterDay){
            cameraMovement.canCameraMove = true;
            grandmaMovement.isWaiting = false;
            counterMinigame++;
            Debug.Log(counterMinigame);
            tvMinigame._isFinished = false;
        }
        if(eventManger.isFinishRandomEvent == true && counterRandomEvent == counterDay){
            cameraMovement.canCameraMove = true;
            grandmaMovement.isWaiting = false;
            counterRandomEvent++;
            counterDay++;
            eventManger.isFinishRandomEvent = false;
        }
        if(counterDay == numberOfDays){
            endGame();
            counterDay++;
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
        cameraMovement.canCameraMove = false;
        Debug.Log ("Start TV minigame");
        tvMinigame.StartTVMinigame();
    }
    void startEvent(){
        grandmaMovement.isWaiting = true;
        cameraMovement.canCameraMove = false;
        Debug.Log ("Start random evenet");
        randomEvent.GetComponent<EventManger>().StartEventSequence();
    }
    void endGame(){
        grandmaMovement.isWaiting = true;
        cameraMovement.canCameraMove = false;
        EndingCard.generateEndCard();
        Debug.Log("END GAME");
    }
}
