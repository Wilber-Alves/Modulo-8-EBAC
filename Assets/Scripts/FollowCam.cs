using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [SerializeField, Tooltip("The object to follow.")]
    private GameObject _camTarget;
    [SerializeField, Tooltip("Target offset.")]
    private Vector3 _targetOffset;
    [SerializeField, Tooltip("The height off the ground to follow from.")]
    private float _camheight = 9.0f;
    [SerializeField, Tooltip("The distance from the target to follow from.")]
    private float _camDistance = -16.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // make sure we have a target to follow
        if (!_camTarget)
            return;

        // use tthe target object and offsets to calculate our target position
        Vector3 targetPos = _camTarget.transform.position;
        targetPos += _targetOffset;
        targetPos.y += _camheight;
        targetPos.z += _camDistance;

        // move the camera towards target position
        Vector3 camPos = transform.position;
        transform.position = Vector3.Lerp(camPos, targetPos, Time.deltaTime * 5.0f);
    }
}
