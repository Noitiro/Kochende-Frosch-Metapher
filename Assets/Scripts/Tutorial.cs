using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour{
    [SerializeField] private GameObject object1;
    [SerializeField] private GameObject object2;
    [SerializeField] private GameObject object3;
    [SerializeField] private GameObject object4;
    [SerializeField] private GameObject tutorialPanelTv;
    public bool endTutorial = false;
    public void hideObject(int objectNumber){
        switch (objectNumber){
            case 1:
                object1.SetActive(false);
                break;
            case 2:
                object2.SetActive(false);
                break;
            case 3:
                object3.SetActive(false);
                break;
            case 4:
                object4.SetActive(false);
                break;
        }
    }

    public void showObject(int objectNumber){
        switch (objectNumber){
            case 1:
                object1.SetActive(true);
                break;
            case 2:
                object2.SetActive(true);
                break;
            case 3:
                object3.SetActive(true);
                break;
            case 4:
                object4.SetActive(true);
                break;
        }
    }

    public void endTut()
    {
        showObject(0);
        endTutorial = true;
    }
}
