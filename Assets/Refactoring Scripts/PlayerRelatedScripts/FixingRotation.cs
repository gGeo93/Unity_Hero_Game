using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixingRotation : MonoBehaviour
{
    public void RotationFix()
    {
        Quaternion q = transform.rotation;
        q.eulerAngles = new Vector3(q.eulerAngles.x, q.eulerAngles.y, Mathf.Clamp(q.eulerAngles.z, -5f, 5f));
        transform.rotation = q;
    }
}
