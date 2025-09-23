using UnityEngine;

public class RotationObjects : MonoBehaviour
{
    public GameObject _level;
    public float _rotationSpeed = 500.0f;
    public float velocityAlignment = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime);

        if (_level != null)
        {
            Vector3 targetPosition = new Vector3(_level.transform.position.x, transform.position.y, _level.transform.position.z);
            Vector3 directionToTarget = targetPosition - transform.position;
            if (directionToTarget != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, velocityAlignment * Time.deltaTime);
            }
        }

    }
}
