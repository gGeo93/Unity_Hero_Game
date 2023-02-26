using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class DroneController : MonoBehaviour
{
    DronesMainControllerCenter dronesMainController;
    public MeshRenderer enemyMapPosition;
    OneForAllSoundEffects oneForAllSoundEffects;
    PlayerAnimatingConditions playerAnimatingConditions;
    DroneAttacks droneAttacks;
    DroneAI droneAI;
    
    void OnEnable() 
    {
        oneForAllSoundEffects = GameManager.Instance.AudioManipulator.GetComponent<OneForAllSoundEffects>();
        playerAnimatingConditions = GameManager.Instance.Player.GetComponent<PlayerAnimatingConditions>();
        droneAI = GetComponent<DroneAI>();
        droneAttacks = GetComponent<DroneAttacks>();
        enemyMapPosition.enabled = true;
    }

    void OnDestroy() 
    {
        this.dronesMainController.DroneRemover(this);
        if(this.dronesMainController.numberOfEnemiesSpawned >= 3)
            oneForAllSoundEffects.ChangeBackgroundSound();
    }
    void Update()
    {
        droneAttacks.LaserBeamAttack();
        droneAI.DroneMind();
        enemyMapPosition.enabled = playerAnimatingConditions.isUsingSmokeQuirk ? false : true;
    }
    public void SetUpDrone(DronesMainControllerCenter dronesMainController)
    {
        this.dronesMainController = dronesMainController;
    }
}
