using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour
{
    public bool isRewinding;
    List<DronePointInTime> dronePointsInTime = new List<DronePointInTime>();
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightShift))
            StartRewinding();
        if(Input.GetKeyUp(KeyCode.RightShift))
            StopRewinding();
    }
    void FixedUpdate() {
        if(isRewinding)
            Rewind();
        else
            Record();    
    }
    void Rewind()
    {
        if(dronePointsInTime.Count > 0)
        {
            transform.position = dronePointsInTime[0].dronePosition;
            transform.rotation = dronePointsInTime[0].droneRotation;
            dronePointsInTime.RemoveAt(0);
        }
        else
        {
            StopRewinding();
        }
    }
    void Record()
    {
        if(dronePointsInTime.Count > Mathf.Round(20/Time.fixedDeltaTime))
        {
            dronePointsInTime.RemoveAt(dronePointsInTime.Count - 1);
        }
        dronePointsInTime.Insert(0, new DronePointInTime(transform.position, transform.rotation));
    }
    public void StartRewinding()
    {
        isRewinding = true;
        Events.onToggleRewinding?.Invoke(isRewinding);
    }    
    public void StopRewinding()
    {
        isRewinding = false;
        Events.onToggleRewinding?.Invoke(isRewinding);    
    }
}
