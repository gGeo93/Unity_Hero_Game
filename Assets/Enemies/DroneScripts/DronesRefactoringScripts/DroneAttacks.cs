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
    private OneForAllSoundEffects enemySoundEffects;
    private PlayerStats playerStats;
    private PlayerAnimatingConditions playerAnimatingConditions;
    private SweepFall sweepFall;
    private Transform dekusRealPosition;
    private DangerSenseQuirk dangerSenseQuirk;

    void Awake() 
    {
        enemySoundEffects = GameManager.Instance.AudioManipulator.GetComponent<OneForAllSoundEffects>();
        dekusRealPosition = GameManager.Instance.Player;
        playerStats = GameManager.Instance.Player.GetComponent<PlayerStats>();
        playerAnimatingConditions = GameManager.Instance.Player.GetComponent<PlayerAnimatingConditions>();
        dekusRealPosition = GameManager.Instance.Player;
        sweepFall = GameManager.Instance.Player.GetComponent<SweepFall>();
        dangerSenseQuirk = GameManager.Instance.Player.GetComponent<DangerSenseQuirk>();    
    }
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
        if(Vector3.Distance(transform.position, dekusRealPosition.transform.position) <= fireRange)
        {
            fireTimer -= Time.deltaTime;
            if (fireTimer <= 0 && !playerAnimatingConditions.isUsingSmokeQuirk && playerAnimatingConditions.canGoShiftingSpeed)
            {
                Instantiate(bullet, transform.position, Quaternion.identity);
                fireTimer = 2f;
                canDodge = true;
                if(canDodge)
                {
                    canDodge = false;
                    if(playerAnimatingConditions.canDodgeWithDangerSense)
                    {
                        dangerSenseQuirk.DodgingLaserBeamMoving();
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
            enemySoundEffects.PlayEnemySound(0);
            IsAboutToExplode = false;
            explosionIndicator.enabled = playerAnimatingConditions.isUsingSmokeQuirk ? false : true;
            yield return new WaitForSeconds(3f);
            droneExplosion.Stop();
        }
    }
    void ExplosiveForce()
    {
        if((dekusRealPosition.transform.position - transform.position).magnitude <= explosionRadius)
        {
            playerStats.GettingDamage(10f);
            sweepFall.sweepFallDirection = (dekusRealPosition.transform.position - transform.position).normalized;
            playerAnimatingConditions.isSweepFalling = true;
        }
    }
}
