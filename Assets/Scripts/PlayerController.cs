using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidBody; // 1) the rigid body physics component of this object; 2) since I will be accessing it a lot, I will store it as a member

    [SerializeField, Tooltip("how much acceleration is applied to this object when directional input is received.")]
    private float _movementAcceleration = 50.0f; // acceleration applied when directional input is received.

    [SerializeField, Tooltip("The maximum velocity of this object - keeps the player from moving too fast.")]
    private float _movementVelocityMax = 10.0f; // the maximum velocity of this object.

    [SerializeField, Tooltip("Deceleration when no direction input is received.")]
    private float _movementFriction = 0.1f; // the maximum velocity of this object.

    [SerializeField, Tooltip("Upwards force applied when Jump key is pressed.")]
    private float _jumpVelocity = 20.0f; // the upwards force applied when the jump key is pressed.

    [SerializeField, Tooltip("Additional gravitational pull.")]
    private float _extraGravity = 40.0f; // additional gravitational pull applied to the player.

    [SerializeField, Tooltip("the bullet projectile prefab to fire.")]
    private GameObject _bulletToSpawn;

    [SerializeField, Tooltip("the antibody projectile prefab to fire.")]
    private GameObject _antibodyToSpawn;

    public Transform shootPoint; // The point from which the antibody will be spawned.

    [Tooltip("The direction that the Player is facing.")]
    Vector3 _curFacing = Vector3.zero;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>(); // Get the rigid body component attached to this object.
    }

    void Update()
    {
        #region player movement and fire weapon inputs

        Vector3 curSpeed = _rigidBody.linearVelocity; // 1) get the current speed from the rigid body physics component; 2) grabbing this ensures I retain the gravity speed. 

        // check to see if the player is pressing any of the directional keys, if so, adjust the speed of the player.

        if (Input.GetKey(KeyCode.RightArrow))
            curSpeed.x += (_movementAcceleration * Time.deltaTime); // accelerate right.
            _curFacing.x = 1f;
            _curFacing.z = 0f;
        if (Input.GetKey(KeyCode.LeftArrow))
            curSpeed.x -= (_movementAcceleration * Time.deltaTime); // accelerate left.
            _curFacing.x = -1f;
            _curFacing.z = 0f;
        if (Input.GetKey(KeyCode.UpArrow))
            curSpeed.z += (_movementAcceleration * Time.deltaTime); // accelerate forward.
            _curFacing.z = 1f;
            _curFacing.x = 0f;
        if (Input.GetKey(KeyCode.DownArrow))
            curSpeed.z -= (_movementAcceleration * Time.deltaTime); // accelerate backward.
            _curFacing.z = -1f;
            _curFacing.x = 0f;

        // check to see if the player is pressing the jump key, if so, apply an upwards force to the player.

        if (Input.GetKeyDown(KeyCode.Z) && Mathf.Abs(curSpeed.y) < 1) // check if the player is pressing the jump key and if the y speed is close to zero (to prevent double jumps).
            curSpeed.y += _jumpVelocity; // apply the jump velocity to the y speed.
        else
            curSpeed.y -= (_extraGravity * Time.deltaTime); // apply additional gravity to the y speed.

        // 1) store the current facing, 2) do this after speed is adjusted by arrow keys, 3) be before friction is applied.

        if (curSpeed.x != 0 && curSpeed.z != 0)
            _curFacing = curSpeed.normalized;

        // if both left and right keys are simultaneusly pressed (or not pressed), apply friction.

        if (Input.GetKey(KeyCode.LeftArrow) == Input.GetKey(KeyCode.RightArrow))
        {
            curSpeed.x -= (_movementFriction * curSpeed.x);
        }

        // apply similar friction logic to up and down keys.

        if (Input.GetKey(KeyCode.UpArrow) == Input.GetKey(KeyCode.DownArrow))
        {
            curSpeed.z -= (_movementFriction * curSpeed.z);
        }

        // apply the max speed.

        curSpeed.x = Mathf.Clamp(curSpeed.x,
                                 _movementVelocityMax * -1,
                                 _movementVelocityMax); // clamp the x speed to the max velocity.
        curSpeed.z = Mathf.Clamp(curSpeed.z,
                                 _movementVelocityMax * -1,
                                 _movementVelocityMax); // clamp the z speed to the max velocity.

        _rigidBody.linearVelocity = curSpeed; // apply the new speed to the rigid body physics component.

        // Fire the weapon?

        if (Input.GetKeyDown(KeyCode.X))
        {
            GameObject newBullet = Instantiate(_bulletToSpawn, transform.position, Quaternion.identity);
            Bullet bullet = newBullet.GetComponent<Bullet>();
            if (bullet)
                bullet.SetDirection(new Vector3(_curFacing.x, 0f, _curFacing.z));
        }
        
        // Fire the Antibody?

        if (Input.GetKeyDown(KeyCode.C))
        {
            GameObject newAntibody = Instantiate(_antibodyToSpawn, transform);
            Antibody antibody = newAntibody.GetComponent<Antibody>();
            if (antibody)
                antibody.SetDirection(new Vector3(_curFacing.x, 0f, _curFacing.z)); // Add force to the antibody in the specified direction.
            antibody.transform.position = shootPoint.transform.position; // Set the position of the antibody to the shoot point
            
        }

        #endregion
    }
    void OnTriggerEnter(Collider collider)
    {
        // did we collide with a PickUpItem?
        if (collider.gameObject.GetComponent<PickUpItem>())
        {
            // we collided with a valid PickUpItem, so let that item it's been "Picked Up" by this player.
            PickUpItem item = collider.gameObject.GetComponent<PickUpItem>();
            item.onPickedUp(this.gameObject);
        }
    }
}

