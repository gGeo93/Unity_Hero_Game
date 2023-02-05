using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneAttacks : MonoBehaviour
{
    DroneController droneController;

    [SerializeField]float fireRange;
    [SerializeField] GameObject bullet;
    [SerializeField] ParticleSystem droneExplosion;
    SphereCollider attackRange;

    float explosionRadius;
    float fireTimer = 2f;
    public bool IsAboutToExplode { get; private set; }
    [SerializeField] MeshRenderer explosionIndicator;
    bool canDodge;

    void OnEnable() 
    {
        droneController = GetComponent<DroneController>();
        attackRange = GetComponent<SphereCollider>();
        explosionRadius = 10f;
    }
    void OnDisable() 
    {
        StopCoroutine(ExplosionAttack());    
    }
    private void Start() 
    {
        StartCoroutine(ExplosionAttack());
    }
    public void LaserBeamAttack()
    {
        if(Vector3.Distance(transform.position, droneController.dronesMainController.DekusRealPosition.transform.position) <= fireRange)
        {
            fireTimer -= Time.deltaTime;
            if (fireTimer <= 0 && !droneController.dronesMainController.PlayerAnimatingConditions.isUsingSmokeQuirk && droneController.dronesMainController.PlayerAnimatingConditions.canGoShiftingSpeed)
            {
                Instantiate(bullet, transform.position, Quaternion.identity);
                fireTimer = 2f;
                canDodge = true;
                if(canDodge)
                {
                    canDodge = false;
                    if(droneController.dronesMainController.PlayerAnimatingConditions.canDodgeWithDangerSense)
                    {
                        droneController.dronesMainController.dangerSenseQuirk.DodgingLaserBeamMoving();
                        StartCoroutine(IntentionallyDangerSensePossibleRate());
                    }
                }
            }
        }
    }
    IEnumerator IntentionallyDangerSensePossibleRate()
    {
        yield return new WaitForSeconds(3f);
        canDodge = true;
    }
    
    IEnumerator ExplosionAttack()
    {
        while(true)
        {
            explosionIndicator.enabled = false;
            yield return new WaitForSeconds(15f);
            IsAboutToExplode = true;
            yield return new WaitForSeconds(30f);
            droneExplosion.Play();
            ExplosiveForce();
            droneController.dronesMainController.PlayExplosionSound();
            IsAboutToExplode = false;
            explosionIndicator.enabled = droneController.dronesMainController.PlayerAnimatingConditions.isUsingSmokeQuirk ? false : true;
            yield return new WaitForSeconds(3f);
            droneExplosion.Stop();
        }
    }
    void ExplosiveForce()
    {
        if((droneController.dronesMainController.DekusRealPosition.transform.position - transform.position).magnitude <= explosionRadius)
        {
            droneController.dronesMainController.PlayerStats.GettingDamage(10f);
            droneController.dronesMainController.SweepFall.sweepFallDirection = (droneController.dronesMainController.DekusRealPosition.transform.position - transform.position).normalized;
            droneController.dronesMainController.PlayerAnimatingConditions.isSweepFalling = true;
        }
    }
}
