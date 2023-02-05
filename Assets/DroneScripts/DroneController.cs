using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class DroneController : MonoBehaviour,ISettingUpEnemy
{
    public DronesMainControllerCenter dronesMainController;
    public MeshRenderer enemyMapPosition;
    DroneAttacks droneAttacks;
    DroneAI droneAI;
    
    
    public void DroneSetUp(DronesMainControllerCenter dronesMainController)
    {
        this.dronesMainController = dronesMainController;
    }
    void OnEnable() 
    {
        droneAI = GetComponent<DroneAI>();
        droneAttacks = GetComponent<DroneAttacks>();
        enemyMapPosition.enabled = true;
    }

    void OnDestroy() {
        this.dronesMainController.DroneRemover(this);
        if(this.dronesMainController.numberOfEnemiesSpawned >= 3)
            this.dronesMainController.OneForAllSoundEffects.ChangeBackgroundSound();
    }
    void Update()
    {
        droneAttacks.LaserBeamAttack();
        droneAI.DroneMind();
        enemyMapPosition.enabled = dronesMainController.PlayerAnimatingConditions.isUsingSmokeQuirk ? false : true;
    }
}
