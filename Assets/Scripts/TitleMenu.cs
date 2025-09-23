using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{
    
    public void StartGame()
    {
        SceneManager.LoadScene("LEVEL1_aMAZECells");
    }
}
