using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocation : MonoBehaviour
{
    [SerializeField] MeshRenderer DekuLocation;
    [SerializeField] MeshRenderer DekuDirection;
    void Start()
    {
        DekuLocation.enabled = true;
        DekuDirection.enabled = true;   
    }
    public void SightUnClear(bool canSeeTheEnemysLocations)
    {
        DekuLocation.enabled = canSeeTheEnemysLocations;
        DekuDirection.enabled = canSeeTheEnemysLocations;
    }
}
