using UnityEngine;

public class HealthModifier : MonoBehaviour
{
    [SerializeField, Tooltip("Change to Health when applied to an object.")]

    float _healthChange = 0f; // this is amount of damage that will be applied when a valid target is collided with.

    [SerializeField, Tooltip("The class of object that shoul be damaged.")]

    DamageTarget _applyToTarget = DamageTarget.Player; // _applyToTarget = While our initial implementation of damage component is intended to hurt the player object,
                                                       // we eventually be hooking up weapons that can also damage enemies. 
                                                       // To allow for this, our damage dealing component should take into account what game objects it should apply damage to.
                                                       // The "_applyToTarget" member, along with the "DamageTarget", will be used to determine the validity of the collided object.
    public enum DamageTarget
    {
        Player,
        Enemies,
        All,
        None
    }

    [SerializeField, Tooltip("Should object self-destruct on collision?")]

    bool _destroyOnCollision = false; // If this is true,this object will destroy itself when a valid object is collided with.
                                      // This will be TRUE for bullets, but FALSE for Spikes and other more permanet health modifying objects. 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject hitObj = collision.gameObject;

        // Get the HealthManager of the object we have hit
        HealthManager healthManager = hitObj.GetComponent<HealthManager>();

        if (healthManager && IsValidTarget(hitObj))
        {
            // Apply the damage as negative health to this object
            healthManager.AdjustCurHealth(_healthChange);

            // Should we self-destruct after dealing damage?
            if (_destroyOnCollision)
                GameObject.Destroy(gameObject);
        }
    }

    private bool IsValidTarget(GameObject possibleTarget)
    {
        if (_applyToTarget == DamageTarget.All)
            return true;

        if (_applyToTarget == DamageTarget.None)
            return false;

        if (_applyToTarget == DamageTarget.Player && possibleTarget.GetComponent<PlayerController>())
            return true;

        if (_applyToTarget == DamageTarget.Enemies && possibleTarget.GetComponent<AIBrain>())
            return true;

        // Not a valid target
        return false;
    }
}