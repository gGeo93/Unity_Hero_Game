using UnityEngine;

public class AttackCameraScript : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public bool useOffsetValue;
    public float rotateSpeed;
    public Transform pivot;
    void Start()
    {
        if (!useOffsetValue)
        {
            offset = target.position - transform.position;
        }
        pivot.transform.position = target.transform.position;
        pivot.transform.parent = target.transform;

        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        if(!PauseMenu.theGameIsPaused)
        {
            float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
            target.Rotate(0, horizontal, 0);
            float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
            //pivot.Rotate(-vertical, 0, 0);
            target.Rotate(-vertical,0,0);
            //target.Rotate(0,0,-vertical);

            float desiredYAngle = target.eulerAngles.y-95f;//target -45
            float desiredXAngle = pivot.eulerAngles.x;

            Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);
            transform.position = target.position - (rotation*offset);

            if (transform.position.y < target.position.y)
            {
                transform.position = new Vector3(transform.position.x,target.position.y + 0.3f,transform.position.z);
            }
            //transform.LookAt(target);
        }
    }
}
