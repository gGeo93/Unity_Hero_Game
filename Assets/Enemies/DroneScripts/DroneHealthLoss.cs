using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DroneHealthLoss : MonoBehaviour
{
    public Image droneImg;
    public Text droneImgText;
    [SerializeField] MeshRenderer blackSphereRenderer;
    DroneAttacks droneAttacks;
    bool droneIsExplodingBeforeDestroyed = false;
    float t = 0.0f;
    MeshFilter meshFilter;
    
    void OnEnable() 
    {
        droneAttacks = GetComponent<DroneAttacks>();
        meshFilter = GetComponent<MeshFilter>();
    }
    void Update() 
    {
        if (int.Parse(droneImgText.text) <= 0 )
        {
            if(!droneIsExplodingBeforeDestroyed)
            {
                droneIsExplodingBeforeDestroyed = true;
                droneAttacks.droneExplosion.Play();
                droneAttacks.enemySoundEffects.PlayEnemySound(0);
                meshFilter.mesh = null;
                droneImg.enabled = false;
                droneImgText.enabled = false;
                Destroy(gameObject,1f);
            }
        }
    }
    private void OnCollisionEnter(Collision other) 
    {
        FaJinQuirk faJinQuirk = other.collider.GetComponentInParent<FaJinQuirk>();
        if(faJinQuirk != null)
        {
            faJinQuirk.FajinEnergyRelease(this, other.collider.name);
        }
        ParticleForces particleForces = other.gameObject.GetComponentInParent<ParticleForces>();
        if(other.collider.name == "Finger_PS")
        {
            droneImg.rectTransform.localScale -= Vector3.right * (particleForces.fingersDamage /150f);
            droneImgText.text = (int.Parse(droneImgText.text)-particleForces.fingersDamage).ToString();
            return;
        }
        else if (other.collider.name == "Kick_PS")
        {    
            droneImg.rectTransform.localScale -= Vector3.right * (particleForces.ShootStyleDamage/150f);
            droneImgText.text = (int.Parse(droneImgText.text)-particleForces.ShootStyleDamage).ToString();
            return;
        }
        else if (other.collider.name == "Punch_PS")
        {
            droneImg.rectTransform.localScale -= Vector3.right * (particleForces.punchDamage/150f);
            droneImgText.text = (int.Parse(droneImgText.text)-particleForces.punchDamage).ToString();
            return;
        }
        else if(other.collider.name == "Left_BlackWhip_Collider" || other.collider.name == "Right_BlackWhip_Collider")
        {
            StartCoroutine(BlackWhipApplied());
            return;
        }    
    }
    IEnumerator BlackWhipApplied()
    {
        blackSphereRenderer.enabled = true;
        t = 0f;
        while(t < 1f)
        {
            transform.localScale = new Vector3(Mathf.Lerp(0.300f, 0.175f, t),Mathf.Lerp(0.300f, 0.175f, t),Mathf.Lerp(0.300f, 0.175f, t));
            yield return null;
            t += Time.deltaTime;
        }
        t=1f;
        while(t > 0f)
        {
            transform.localScale = new Vector3(Mathf.Lerp(0.300f, 0.175f, t),Mathf.Lerp(0.300f, 0.175f, t),Mathf.Lerp(0.300f, 0.175f, t));
            yield return null;
            t -= Time.deltaTime;
        }
        
        droneImg.rectTransform.localScale -= Vector3.right * 0.1f;
        droneImgText.text = (int.Parse(droneImgText.text)-10).ToString();
        
        blackSphereRenderer.enabled = false;
    }
}
