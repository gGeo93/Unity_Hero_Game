using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ParticleForces : MonoBehaviour
{
    enum FaJinConcentrationPowerType
    {
        DetroitSmash = 0, ShootStyle = 1, FingerSmash = 2
    }
    public float radius;
    public ParticleSystem FaJinParticles;
    public ParticleSystem punchParticles;
    public ParticleSystem explosiveParticles;
    public ParticleSystem kickParticles;
    public ParticleSystem fingersParticles;
    public ParticleSystem RightHandBlackWHip;
    public ParticleSystem LeftHandBlackWHip;
    public GameObject fingerCollider;
    public List<GameObject> blackWhipColliders = new List<GameObject>();
    Rigidbody fcrb;
    Rigidbody[] blackWhipRbs = new Rigidbody[2];
    public MeshRenderer[] detroitSmashConcentration;
    public MeshRenderer shootStyleConcentration;
    public MeshRenderer fingerSmashConcentration;
    float explodingForcePower = 10;
    public int fingersDamage = 20;
    public int ShootStyleDamage = 30;
    public int punchDamage = 20;
    PlayerAnimatingConditions playerAnimatingConditions;
    ElectricityScript electricityScript;

    private void Awake() 
    {
        playerAnimatingConditions = GetComponent<PlayerAnimatingConditions>();
        fcrb = fingerCollider.GetComponent<Rigidbody>();
        blackWhipRbs[0] = blackWhipColliders[0].GetComponent<Rigidbody>();
        blackWhipRbs[1] = blackWhipColliders[1].GetComponent<Rigidbody>();
        electricityScript = GetComponent<ElectricityScript>();        
    }
    private void Start() 
    {
        detroitSmashConcentration[0].enabled = false;
        detroitSmashConcentration[1].enabled = false;

        shootStyleConcentration.enabled = false;
        
        fingerSmashConcentration.enabled = false;
        
        BlackWhipStopped();
    }
    public void ExplodingForce()
    {
        Collider[] objectsToAffect = Physics.OverlapSphere(transform.position, radius);
        foreach (var item in objectsToAffect)
        {
            if (item.TryGetComponent(out Rigidbody rb))
            {
                if(rb != null && item.gameObject.CompareTag("Enemy"))
                {
                    Transform objectToAnimate = item.GetComponent<Transform>();
                    objectToAnimate.DOShakeRotation(0.5f, Vector3.up * 180f);
                    rb.AddForce(Vector3.forward, ForceMode.Force);
                    Text droneImgText = item.gameObject.GetComponent<DroneHealthLoss>().droneImgText;
                    Image droneImg = item.gameObject.GetComponent<DroneHealthLoss>().droneImg;
                    if(droneImgText != null && droneImg != null && item.CompareTag("Enemy"))
                    {
                        droneImgText.text = (int.Parse(droneImgText.text)-explodingForcePower).ToString();
                        droneImg.rectTransform.localScale -= Vector3.right * (explodingForcePower/100f);
                    }
                }
            }
        }
    }
    public void PunchEffect()
    {
        fcrb.AddRelativeForce(-Vector2.up * 4000f);
        punchParticles.Play();
    }

    public void KickEffect()
    {
        fcrb.AddRelativeForce(-Vector2.up * 3000f);
        kickParticles.Play();
    }

    public void FingersEffect()
    {
        fcrb.AddRelativeForce(-Vector3.up*2000f);
        fingersParticles.Play();
    }
    public void BlackWhipApplied()
    {
        RightHandBlackWHip.gameObject.SetActive(true);
        LeftHandBlackWHip.gameObject.SetActive(true);
        RightHandBlackWHip.Play();
        LeftHandBlackWHip.Play();
    }
    public void BlackWhipStopped()
    {
        RightHandBlackWHip.Stop();
        LeftHandBlackWHip.Stop();
        RightHandBlackWHip.gameObject.SetActive(false);
        LeftHandBlackWHip.gameObject.SetActive(false);
    }
}
