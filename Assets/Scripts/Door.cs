using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject door;

    private void Awake()
    {
        door.SetActive(true);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();
            if (playerInventory != null && playerInventory.hasSubstrate)
            {
                door.SetActive(false);
                Debug.Log("Protein activated!");
            }
            else
            {
                door.SetActive(true);
                Debug.Log("You need the Substrate to activate channel protein!");
            }
        }
    }
}