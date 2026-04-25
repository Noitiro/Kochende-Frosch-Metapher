using UnityEngine;

[CreateAssetMenu(fileName = "EventCardSO", menuName = "EventCardSO")]
public class EventCardSO : ScriptableObject
{
    [Header("Informacje")]
    public string EventName;
    [TextArea] public string Story;
    public Sprite Backgroud;
    public int value;
    [Header("Lewy wybór")]
    [TextArea] public string LeftOption;
    public bool isLeftOptionNegative;
    [TextArea] public string LeftResponse;
    [Header("Prawy wybór")]
    [TextArea] public string RightOption;
    public bool isRightOptionNegative;
    [TextArea] public string RightResponse;
}