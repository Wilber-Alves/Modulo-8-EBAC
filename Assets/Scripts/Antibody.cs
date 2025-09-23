using UnityEngine;

public class Antibody : MonoBehaviour
{
    public GameObject _antibodyToSpawn;
    private Vector3 _direction = Vector3.zero;
    private float _speed = 4f;
    public float timeToDestroy = 5f; // Time after which the antibody will be destroyed.


    public void Update()
    {
        Vector3 newPos = transform.position;
        newPos += _direction * (_speed * Time.deltaTime);
        transform.position = newPos;
    }
    public void SetDirection(Vector3 direction)
    {
        _direction = direction;

        // rotate to face the direction of movement
        transform.LookAt(transform.position + _direction);
    }
    public void Awake()
    {
        Destroy(_antibodyToSpawn, timeToDestroy);
    }
}
