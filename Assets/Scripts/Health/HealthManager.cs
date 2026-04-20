using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;


public class HealthManager : Health
{
    // References the health on both players\
    //player 1 health refernce
    [SerializeField] public Health _player1Health;

    //player 2 health refernce
    [SerializeField] public Health _player2Health;

    [SerializeField] private TMPro.TextMeshProUGUI _player1Wins;
    [SerializeField] private TMPro.TextMeshProUGUI _player1Loses;
    [SerializeField] private TMPro.TextMeshProUGUI _player2Wins;
    [SerializeField] private TMPro.TextMeshProUGUI _player2Loses;
    
    [SerializeField] private TMPro.TextMeshProUGUI _player1CurrentHealth;
    [SerializeField] private TMPro.TextMeshProUGUI _player2CurrentHealth;
        



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _player1CurrentHealth.text = _player1Health.health.ToString();
        _player2CurrentHealth.text = _player2Health.health.ToString();
         
        if(_player1Health.health <= 0 || _player2Health.health <= 0)
        {
            StartCoroutine(WaitForReloadScene());
        }
    }

    private IEnumerator WaitForReloadScene()
    {
        yield return new WaitForSeconds(5);
        // If the health script is on the player, reset the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}