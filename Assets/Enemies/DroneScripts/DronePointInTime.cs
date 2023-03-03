using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DronePointInTime
{
    public Vector3 dronePosition;
    public Quaternion droneRotation;
    public DronePointInTime(Vector3 _dronePos, Quaternion _dronQuat)
    {
        dronePosition = _dronePos;
        droneRotation = _dronQuat;        
    }
}
