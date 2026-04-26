using System.Collections;
using UnityEngine;

public class GrandmaMovement : MonoBehaviour{
    [SerializeField] private Transform grandmaTrack;
    [SerializeField] private float grandmaSpeed = 2f;
    [SerializeField] private float waitTime = 2f;
    [SerializeField] private bool loopWaypoints = true;

    private Transform[] waypoints;
    private int currentWaypointIndex;
    public bool isWaiting;
    void Start(){
        waypoints = new Transform[grandmaTrack.childCount];
        for(int i = 0; i < grandmaTrack.childCount; i++)
        {
            waypoints[i] = grandmaTrack.GetChild(i);
        }
    }

    void Update(){
        if (isWaiting){
            return;
        }
        MoveToWaypoint();
    }
    void MoveToWaypoint(){
        Transform target = waypoints[currentWaypointIndex];

        transform.position = Vector2.MoveTowards(transform.position, target.position, grandmaSpeed * Time.deltaTime);

        if(Vector2.Distance(transform.position, target.position) < 0.1f){
            StartCoroutine(WaitAtWaypoint());
        }
    }
    IEnumerator WaitAtWaypoint()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);

        currentWaypointIndex = loopWaypoints ? (currentWaypointIndex + 1) % waypoints.Length : Mathf.Min(currentWaypointIndex + 1, waypoints.Length - 1);

        isWaiting = false;
    }
}
