using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSessionManager : MonoBehaviour
{
    [SerializeField, Tooltip("Remaining player lives.")]
    private int _playerLives = 3;

    [SerializeField, Tooltip("where the player will respawn.")]
    private Transform _respawnPosition;

    [SerializeField, Tooltip("Object to dispaly when the game is over.")]
    private GameObject _gameOverObj;

    [SerializeField, Tooltip("Title Menu countdown after game is over.")]
    private float _returnToMenuCountdown = 0f;

    static public GameSessionManager Instance;


    void Awake()
    {
        // 1) the GameSessionManageer is a Singleton. 2) store this as the instance of this object.
        Instance = this;
    }
    public void onPlayerDeath(GameObject player)
    {
        if (_playerLives <= 0)
        {
            //player is out of lives
            GameObject.Destroy(player.gameObject);
            Debug.Log("Game over!");
            _gameOverObj.SetActive(true);
            _returnToMenuCountdown = 4f;
        }
        else
        {
            // use a life to respawn the player
            _playerLives--;

            // reset health
            HealthManager playerHealth = player.GetComponent<HealthManager>();
            if (playerHealth)
                playerHealth.Reset();
            if (_respawnPosition)
                player.transform.position = _respawnPosition.position;

            Debug.Log("Player lives remaining:" + _playerLives);
        }
        // clear velocity of this object
        Rigidbody rb = player.transform.GetComponent<Rigidbody>();
        if (rb)
            rb.linearVelocity = Vector3.zero;
    }
    public int GetLives()
    {
        return _playerLives;
    }
    public int GetATP()
    {
        return PickUpItem.s_objectsCollected;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_returnToMenuCountdown > 0)
        {
            _returnToMenuCountdown -= Time.deltaTime;
            if (_returnToMenuCountdown < 0)           
            SceneManager.LoadScene("TitleMenu");
        }
    }

}