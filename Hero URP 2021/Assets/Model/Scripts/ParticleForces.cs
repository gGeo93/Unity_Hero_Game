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
    public float explosivePower;
    public ParticleSystem FaJinParticles;
    public ParticleSystem punchParticles;
    public ParticleSystem explosiveParticles;
    public ParticleSystem kickParticles;
    public ParticleSystem fingersParticles;
    public ParticleSystem RightHandBlackWHip;
    public ParticleSystem LeftHandBlackWHip;
    public GameObject fingerCollider;
    public List<GameObject> blackWhipColliders = new List<GameObject>();
    public Image droneImg;
    public Text droneImgText;
    Rigidbody fcrb;
    Rigidbody[] blackWhipRbs = new Rigidbody[2];
    [SerializeField]MeshRenderer[] detroitSmashConcentration;
    [SerializeField]MeshRenderer shootStyleConcentration;
    [SerializeField]MeshRenderer fingerSmashConcentration;
    float explodingForcePower = 10;
    public static int fingersDamage = 20;
    public static int ShootStyleDamage = 30;
    public static int punchDamage = 20;
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
                if(!item.gameObject.CompareTag("Player"))
                {
                    Transform objectToAnimate = item.GetComponent<Transform>();
                
                    objectToAnimate.DOShakeRotation(0.5f, Vector3.up * 180f);
                }
                if(droneImgText != null && droneImg != null && item.CompareTag("Enemy"))
                {
                    droneImgText.text = (int.Parse(droneImgText.text)-explodingForcePower).ToString();
                    droneImg.rectTransform.localScale -= Vector3.right * (explodingForcePower/100f);
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
    public void FaJinParticlesApplied()
    {
        if(playerAnimatingConditions.isUsingFaJin)
        {
            FaJinParticles.Play();
            StartCoroutine(FaJinDuration());
        }
    }
    IEnumerator FaJinDuration()
    {
        int numberOfFaJinConcetrationPower = Random.Range(0, 3);

        switch (numberOfFaJinConcetrationPower)
        {
            case (int)FaJinConcentrationPowerType.DetroitSmash: detroitSmashConcentration[0].enabled = true; detroitSmashConcentration[1].enabled = true; punchDamage *= 2; break;
            case (int)FaJinConcentrationPowerType.ShootStyle: shootStyleConcentration.enabled = true; ShootStyleDamage *= 2; break;
            case (int)FaJinConcentrationPowerType.FingerSmash: fingerSmashConcentration.enabled = true; fingersDamage *= 2; break;
            default: Debug.Log("FaJin not working"); break;
        }

        StartCoroutine(FajinForcedEnd(numberOfFaJinConcetrationPower));
        yield return new WaitForSeconds(20f);
        Debug.Log("didn't work");
        FajinEnd(numberOfFaJinConcetrationPower);
    }

    private void FajinEnd(int numberOfFaJinConcetrationPower)
    {
        switch (numberOfFaJinConcetrationPower)
        {
            case (int)FaJinConcentrationPowerType.DetroitSmash: detroitSmashConcentration[0].enabled = false; detroitSmashConcentration[1].enabled = false; punchDamage /= 2; break;
            case (int)FaJinConcentrationPowerType.ShootStyle: shootStyleConcentration.enabled = false; ShootStyleDamage /= 2; break;
            case (int)FaJinConcentrationPowerType.FingerSmash: fingerSmashConcentration.enabled = false; fingersDamage /= 2; break;
        }
        playerAnimatingConditions.isUsingFaJin = false;
    }

    IEnumerator FajinForcedEnd(int numberOfFaJinConcetrationPower)
    {
        while(true)
        {
            yield return null;
            if(electricityScript.isTired)
            {
                FajinEnd(numberOfFaJinConcetrationPower);
                StopAllCoroutines();
                break;
            }    
        }
    }
}
