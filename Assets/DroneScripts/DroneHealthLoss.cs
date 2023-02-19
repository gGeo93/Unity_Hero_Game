using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DroneHealthLoss : MonoBehaviour
{
    public Image droneImg;
    public Text droneImgText;
    [SerializeField] MeshRenderer blackSphereRenderer;
    
    private Transform DekuPosition;
    float t = 0.0f;
    private void Awake() {
        DekuPosition = GameObject.FindGameObjectWithTag("PlayerPosition").GetComponent<Transform>();
    }
    
    private void Update() {
        if (int.Parse(droneImgText.text) <= 0 )
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision other) 
    {
        if(other.collider.name == "Finger_PS")
        {
            droneImg.rectTransform.localScale -= Vector3.right * (ParticleForces.fingersDamage /150f);
            droneImgText.text = (int.Parse(droneImgText.text)-ParticleForces.fingersDamage).ToString();
        }
        else if (other.collider.name == "Kick_PS")
        {    
            droneImg.rectTransform.localScale -= Vector3.right * (ParticleForces.ShootStyleDamage/150f);
            droneImgText.text = (int.Parse(droneImgText.text)-ParticleForces.ShootStyleDamage).ToString();
        }
        else if (other.collider.name == "Punch_PS")
        {
            droneImg.rectTransform.localScale -= Vector3.right * (ParticleForces.punchDamage/150f);
            droneImgText.text = (int.Parse(droneImgText.text)-ParticleForces.punchDamage).ToString();
        }
        else if(other.collider.name == "Left_BlackWhip_Collider" || other.collider.name == "Right_BlackWhip_Collider")
        {
            StartCoroutine(BlackWhipApplied());
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
