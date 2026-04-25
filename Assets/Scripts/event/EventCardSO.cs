using UnityEngine;

[CreateAssetMenu(fileName = "EventCardSO", menuName = "EventCardSO")]
public class EventCardSO : ScriptableObject
{
    [Header("Informacje")]
    public string EventName;
    [TextArea] public string Story;
    public Sprite Icon;
    public int value;
    [Header("Lewy wybor")]
    [TextArea] public string LeftOption;
    public bool isLeftOptionNegative;
    [TextArea] public string LeftResponse;
    [Header("Prawy wybor")]
    [TextArea] public string RightOption;
    public bool isRightOptionNegative;
    [TextArea] public string RightResponse;
}