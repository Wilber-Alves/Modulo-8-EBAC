using UnityEngine;

public class SpinnerMoviment : MonoBehaviour
{
    [SerializeField, Tooltip("The speed that object rotates at.")]
    private float _rotationSpeed = 500.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        // grab the currennt rotatation, increment it, and re-apply it.
        Vector3 newRotation = transform.eulerAngles;
        newRotation.y += (_rotationSpeed * Time.deltaTime);
        transform.eulerAngles = newRotation;
    }
}
