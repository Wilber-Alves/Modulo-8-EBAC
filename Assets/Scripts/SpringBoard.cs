using UnityEngine;

public class SpringBoard : MonoBehaviour
{

    [SerializeField, Tooltip ("Velocity change on the Y axis.")]
    float _bounceForce = 2000f;

   private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (playerRigidbody != null)
            {
                playerRigidbody.AddForce(Vector3.up * _bounceForce);
                Debug.Log("Player bounced with force: " + _bounceForce);
            }
        }
    }
}
