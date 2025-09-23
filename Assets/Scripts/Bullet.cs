using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField, Tooltip("Speed of this bullet.")]
    private float _speed = 4f;
    [SerializeField, Tooltip("Normalized direction of this bullet.")]
    private Vector3 _direction = Vector3.zero;
    [SerializeField, Tooltip("The speed that object rotates at.")]
    private float _rotationSpeed = 2000.0f;
    [SerializeField, Tooltip("Seconds until this object self-destructs.")]
    float _countdownTimer = 1.5f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // move the bullet 
        Vector3 newPos = transform.position;
        newPos += _direction * (_speed * Time.deltaTime);
        transform.position = newPos;

        Vector3 newRotation = transform.eulerAngles;
        newRotation.z += (_rotationSpeed * Time.deltaTime);
        transform.eulerAngles = newRotation;

        _countdownTimer -= Time.deltaTime;
        if (_countdownTimer <= 0f)
            Destroy(gameObject);

    }
    public void SetDirection(Vector3 direction)
    {
        _direction = direction;

        // rotate to face the direction of movement
        transform.LookAt(transform.position + _direction);
    }
}

