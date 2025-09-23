using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel3 : MonoBehaviour
{

    public string levelToLoad = "LEVEL3_aMAZECells";

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(levelToLoad);
            Debug.Log("Carregando a cena: " + levelToLoad);
        }
    }

}