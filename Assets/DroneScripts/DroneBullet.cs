using UnityEngine;

public class DroneBullet : MonoBehaviour
{
    public float speed;
    public float amountOfDamage;
    Transform playerPos;
    Vector3 dir;

    private void Start() {
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

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("PlayerPosition"))
        {
            other.GetComponentInParent<PlayerStats>().GettingDamage(amountOfDamage);
        }
    }
}
