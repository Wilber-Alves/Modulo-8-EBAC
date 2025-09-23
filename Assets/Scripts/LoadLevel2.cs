using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel2 : MonoBehaviour
{

    public string levelToLoad = "LEVEL2_aMAZECells";

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(levelToLoad);
            Debug.Log("Carregando a cena: " + levelToLoad);
        }
    }

}

