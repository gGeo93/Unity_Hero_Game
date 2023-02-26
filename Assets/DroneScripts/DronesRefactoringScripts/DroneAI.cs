using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneAI : MonoBehaviour
{
    DroneController droneController;
    [SerializeField] NavMeshAgent agent;
    PlayerAnimatingConditions playerAnimatingConditions;
    Transform dekusRealPosition;

    void OnEnable() 
    {
        playerAnimatingConditions = GameManager.Instance.Player.GetComponent<PlayerAnimatingConditions>();
        dekusRealPosition = GameManager.Instance.Player;
        droneController = GetComponent<DroneController>();    
    }
    public void DroneMind()
    {
        if(agent.enabled && !playerAnimatingConditions.isUsingSmokeQuirk)
        {
            agent.SetDestination(dekusRealPosition.transform.position);
        }
        else if(agent.enabled && playerAnimatingConditions.isUsingSmokeQuirk)
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
