using UnityEngine;
using TMPro;

public class MainGameHUD : MonoBehaviour
{
    [SerializeField, Tooltip("TMP object displaying the player's Life")]
    TextMeshProUGUI _lifeValueText;

    [SerializeField, Tooltip("TMP object displaying the player's collected ATP")]
    TextMeshProUGUI _aTPValueText;

    [SerializeField, Tooltip("TMP object displaying the player's Lives")]
    TextMeshProUGUI _livesValueText;

    [SerializeField, Tooltip("TMP object displaying the player's current level")]
    TextMeshProUGUI _levelValueText;

    [SerializeField, Tooltip("TMP object displaying the player's collected key Substrate")]
    TextMeshProUGUI _substrateValueText;

    [SerializeField, Tooltip("Reference to the HealthManager component")]
    HealthManager _healthManager;

    [SerializeField, Tooltip("Reference to the PlayerInventory component")]
    PlayerInventory _playerInventory; // Added a reference to PlayerInventory

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        int curHealth = Mathf.RoundToInt(_healthManager.GetHealthCur());
        int maxHealth = Mathf.RoundToInt(_healthManager.GetHealthMax());
        _lifeValueText.text = curHealth + "/" + maxHealth;

        _aTPValueText.text = GameSessionManager.Instance.GetATP().ToString();

        _livesValueText.text = GameSessionManager.Instance.GetLives().ToString();

        //_levelValueText.text = LevelManager.Instance.GetCurrentLevel().ToString();

        _substrateValueText.text = _playerInventory.hasSubstrate.ToString(); // Updated to use the instance reference

    }
}
