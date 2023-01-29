using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneAI : MonoBehaviour
{
    DroneController droneController;
    [SerializeField] NavMeshAgent agent;

    void OnEnable() 
    {
        droneController = GetComponent<DroneController>();    
    }
    public void DroneMind()
    {
        if(agent.enabled && !droneController.dronesMainController.PlayerAnimatingConditions.isUsingSmokeQuirk)
        {
            agent.SetDestination(droneController.dronesMainController.DekusRealPosition.transform.position);
        }
        else if(agent.enabled && droneController.dronesMainController.PlayerAnimatingConditions.isUsingSmokeQuirk)
        {
            agent.SetDestination(this.transform.position);
        }
        else if(!agent.enabled)
        {
            Debug.Log("disabled agent!");
            transform.position = this.transform.position;
        }
    }
}
