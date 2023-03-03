using System.Collections;
using UnityEngine;

public class ElectricityScript : MonoBehaviour
{
    public MeshRenderer electricity;
    public bool isTired;
    PhysicalConditions physicalConditions;
    PlayerMoving playerMoving;
    PlayerAnimatingConditions playerAnimatingConditions;
    void Awake() 
    {
        physicalConditions = GetComponent<PhysicalConditions>();
        playerAnimatingConditions = GetComponent<PlayerAnimatingConditions>();
        playerMoving = GetComponent<PlayerMoving>();
    }
    private void Start() 
    {
        electricity.enabled=false;    
    }
    public void Electricity_On()
    {
        playerAnimatingConditions.canUseOneforAll=true;
        playerMoving.walkingSpeed = 3f;
        playerMoving.sprindSpeed = 6f;
        physicalConditions.AirLimit = 15;
        this.StartCharging();
    }
    public void ElectricityOff()
    {
        physicalConditions.RepairingGravity();
        playerAnimatingConditions.canUseOneforAll = false;
        electricity.enabled = false;
        this.isTired = true;
        playerMoving.walkingSpeed = 1f;
        playerMoving.sprindSpeed = 2.5f;
        physicalConditions.AirLimit = 1.5f;        
    }
    private void StartCharging()
    {
        StartCoroutine(Charge());
    }

    private IEnumerator Charge()
    {
        while(!isTired && !playerAnimatingConditions.isDead)
        {
            electricity.enabled = false;
            yield return new WaitForSeconds(0.5f);
            electricity.enabled = true;
            yield return new WaitForSeconds(0.5f);
        }
        electricity.enabled = false;
    }
}
