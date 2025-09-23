using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField, Tooltip("The maximum health of this object.")]
    private float _healthMax = 10f; // The maximum health of this object,
                                    // used to determine how much damage it can take before dying.

    [SerializeField, Tooltip("The current health of this object.")]
    private float _healthCur = 10f; // The current health of this object,
                                    // which decreases when the object takes damage.

    [SerializeField, Tooltip("Seconds of damage immunity after being hit.")]
    private float _invincibilityFramesMax = 1f; // The maximum duration of invincibility after being hit,
                                                // allowing the object to avoid taking damage for a short period.

    [SerializeField, Tooltip("Remaining seconds of immunity after being hit.")]
    private float _invincibilityFramesCur = 0f; // The current duration of invincibility after being hit,
                                                // which decreases over time until it reaches zero.

    [SerializeField, Tooltip("Is this object dead.")]
    private bool _isDead = false; // A boolean flag indicating whether the object is dead,
                                  // which can be used to prevent further actions or damage.
    void Start() // Start is called once before the first execution of Update after the MonoBehaviour is created
    {

    }

    void Update() // Update is called once per frame
    {
        if (_invincibilityFramesCur > 0)
        {
            _invincibilityFramesCur -= Time.deltaTime; // Decrease the invincibility frames by the time since the last frame.

            if (_invincibilityFramesCur < 0) // If the invincibility frames have gone below zero,
                _invincibilityFramesCur = 0; // reset it to zero.

            // handle death
            if (isDead())
                GameObject.Destroy(gameObject); // If the object is dead, destroy it.
        }

        // handle visibility.

        if (GetComponent<MeshRenderer>())
        {
            if (_invincibilityFramesCur > 0)
            {
                // toggle rendering on/off
                if (GetComponent<MeshRenderer>().enabled == true)
                    GetComponent<MeshRenderer>().enabled = false; // turn off rendering
                else
                    GetComponent<MeshRenderer>().enabled = true; // turn on rendering
            }
            else
            {
                GetComponent<MeshRenderer>().enabled = true; // ensure rendering is on if we are not invincible.
            }
        }

        // toggle camara shake for the player.

        if (GetComponent<PlayerController>())
        {
            CameraShake camShake = Camera.main.GetComponent<CameraShake>();
            if (camShake)
                camShake.enabled = (bool)(_invincibilityFramesCur > 0); // enable camera shake if we are invincible.
        }

        // insta-death when we're in a endlless pit.
        float yBounds = -25.0f;
        if (transform.position.y < yBounds)
        {
            AdjustCurHealth(-_healthMax); // reduce health to zero to trigger death.
        }

    }

    public float AdjustCurHealth(float change) // This method adjusts the current health of the object by a specified change amount.
    {
        if (_invincibilityFramesCur > 0) // leave early if we have just been hit and we are trying to apply damage.
            return _healthCur;

        _healthCur += change; // adjust the health

        if (_healthCur <= 0) // this object has died, so start the process to destroy it
        {
            onDeath();
        }
        else if (_healthCur >= _healthMax) // this object has more health than it should,
                                           // so cap it to its max.
        {
            _healthCur = _healthMax;
        }

        if (change < 0 && _invincibilityFramesMax > 0)
        {
            _invincibilityFramesCur = _invincibilityFramesMax;
        }

        return _healthCur; // Ensure all code paths return a value
    }
    void onDeath() // This method is called when the object dies.
    {
        if (_healthCur > 0)
        {
            Debug.Log(gameObject.name + "set as dead before health reached 0.");
        }
        _isDead = true; // set the object as dead.
    }
    public bool isDead() // This method returns whether the object is dead.
    {
        // handle death
        if (_isDead)
        {
            if (GetComponent<PlayerController>())
            {
                GameSessionManager.Instance.onPlayerDeath(gameObject); // notify the game session manager that the player has died.
            }
            else
            {
                GameObject.Destroy(gameObject); // destroy non-player objects immediately.
            }

            VFXHandler vfx = GetComponent<VFXHandler>();
            vfx?.SpawnExplosion(); // spawn explosion if we have a VFXHandler component.
        }
        return _isDead; // Ensure all code paths return a value
            
    }

    public void Reset()
    {
        // Reset the health and invincibility frames to their initial values
        _isDead = false;
        _healthCur = _healthMax;
        _invincibilityFramesCur = 0f;
    }

    public float GetHealthCur()
    {
        return _healthCur;
    }

    public float GetHealthMax()
    {
        return _healthMax;
    }

}

