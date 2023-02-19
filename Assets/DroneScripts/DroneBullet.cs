using UnityEngine;

public class DroneBullet : MonoBehaviour
{
    public float speed = 7f;
    public float amountOfDamage = 2.5f;
    Transform playerPos;
    Vector3 dir;

    void Start() 
    {
        Destroy(gameObject,5f);
        playerPos = GameObject.FindGameObjectWithTag("PlayerPosition").transform;
        dir = playerPos.position - transform.position;
    }
    void FixedUpdate()
    {
        if (playerPos != null)
        {
            transform.Translate(dir * Time.deltaTime * speed, Space.World);
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("PlayerPosition"))
        {
            other.GetComponentInParent<PlayerStats>().GettingDamage(amountOfDamage);
        }
    }
}
