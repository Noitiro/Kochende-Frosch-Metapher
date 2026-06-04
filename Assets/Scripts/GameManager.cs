using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour{
    [SerializeField] private int numberOfDays;
    [SerializeField] private Collider2D tv;
    [SerializeField] private GameObject tvEvent;
    [SerializeField] private GameObject radioEvent;
    [SerializeField] private GameObject randomEvent;
    [SerializeField] private EndingCard EndingCard;
    [SerializeField] private CameraMovement cameraMovement;
    [SerializeField] private GameObject tutorialTv;
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private GameObject tutorialRadio;
    [SerializeField] private GameObject tutorialRadioPanel;


    private GrandmaMovement grandmaMovement;
    private TVMinigame tvMinigame;
    private RadioMinigameLogic radioMinigame;
    private FadeInOut fadeInOut;
    private EventManger eventManger;
    [SerializeField] private int counterDay;
    [SerializeField] private int counterRandomEvent;
    [SerializeField] private int counterMinigame;
    [SerializeField] private Animator animTv;
    [SerializeField] private TextMeshProUGUI scoreText;

    private Animator anim;

    IEnumerator Wait(){
        yield return new WaitForSeconds(1f);
        animTv.SetBool("TvOn",true);
        yield return new WaitForSeconds(1f);
        cameraMovement.canCameraMove = false;
        if(counterDay == 1 && tutorialTv.GetComponent<Tutorial>().endTutorial == false){
            tutorialPanel.SetActive(true);
            Debug.Log(tutorialTv.GetComponent<Tutorial>().endTutorial);
        }else{
            tvMinigame.StartTVMinigame();
        }
    }

    private void noWait()
    {
        animTv.SetBool("TvOn",true);
        cameraMovement.canCameraMove = false;
        if(counterDay == 1 && tutorialTv.GetComponent<Tutorial>().endTutorial == false){
            tutorialPanel.SetActive(true);
            Debug.Log(tutorialTv.GetComponent<Tutorial>().endTutorial);
        }else{
            tvMinigame.StartTVMinigame();
        }
    }

    IEnumerator Sleep(){
        fadeInOut.FadeOut();
        cameraMovement.canCameraMove = false;
        grandmaMovement.isWaiting = true;
        yield return new WaitForSeconds(3f);
        fadeInOut.FadeIn();
        yield return new WaitForSeconds(3f);
        grandmaMovement.isWaiting = false;
        cameraMovement.canCameraMove = true;
    }

    void Start(){
        numberOfDays = 4;
        counterMinigame = 1;
        counterRandomEvent = 1;
        counterDay = 1;
        grandmaMovement = GetComponent<GrandmaMovement>();
        tvMinigame = tvEvent.GetComponent<TVMinigame>();
        radioMinigame = radioEvent.GetComponent<RadioMinigameLogic>();
        eventManger = randomEvent.GetComponent<EventManger>();
        anim = GetComponent<Animator>();
        fadeInOut = GetComponent<FadeInOut>();
    }
    void Update(){
        if(tvMinigame._isFinished == true && counterMinigame == counterDay){
            animTv.SetBool("TvOn",false);
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
            eventManger.isFinishRandomEvent = false;
        }
        if(radioMinigame.isFinished == true){
            cameraMovement.canCameraMove = true;
            grandmaMovement.isWaiting = false;
            radioMinigame.isFinished = false;
        }
        if(counterDay == numberOfDays){
            endGame();
            counterDay++;
        }
        scoreText.text = "Day: " + counterDay + "/" + (numberOfDays-1);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.name == "Tv" && tvMinigame._isFinished == false) {
            startTv();
        } else if(other.name == "EventManger" && counterDay == 1) {
            startEvent();
        }else if(other.name == "Radio" || other.name == "Radio1" ) {
            startRadio();
        }else if(other.name == "Bed" || other.name == "Bed1"){
            counterDay++;
            StartCoroutine(Sleep());
        }else if(other.name == "EventManger1") {
            startEvent();
        }else if(other.name == "EventManger2") {
            startEvent();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.name == "Bed") {
            fadeInOut.FadeIn();
        }
    }

    public void startTv(){
        grandmaMovement.isWaiting = true;
        anim.SetTrigger("GrandmaTvOn");
        StartCoroutine(Wait());
    }
    public void startTv2(){
        grandmaMovement.isWaiting = true;
        noWait();
    }
    void startEvent(){
        grandmaMovement.isWaiting = true;
        cameraMovement.canCameraMove = false;
        Debug.Log ("Start random evenet");
        randomEvent.GetComponent<EventManger>().StartEventSequence();
    }
    public void startRadio(){
        grandmaMovement.isWaiting = true;
        cameraMovement.canCameraMove = false;
        if(counterDay == 2 && tutorialRadio.GetComponent<Tutorial>().endTutorial == false){
            tutorialRadioPanel.SetActive(true);
        }
        else
        {
            radioMinigame.StartRadioMinigame();
        }
    }
    void endGame(){
        grandmaMovement.isWaiting = true;
        cameraMovement.canCameraMove = false;
        EndingCard.generateEndCard();
        Debug.Log("END GAME");
    }
}
