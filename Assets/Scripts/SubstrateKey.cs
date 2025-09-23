using UnityEngine;

public class SubstrateKey : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure your player has the "Player" tag
        {
            PlayerInventory playerInventory = other.GetComponent<PlayerInventory>(); // Assuming a PlayerInventory script on the player
            if (playerInventory != null)
            {
                playerInventory.hasSubstrate = true;
                Destroy(gameObject); // Destroy the key after pickup
            }
        }
    }

    public bool GetSubstrate(PlayerInventory playerInventory)
    {
        if (playerInventory != null)
        {
            return playerInventory.hasSubstrate;
        }
        return false; // Default value if playerInventory is null
    }
}
