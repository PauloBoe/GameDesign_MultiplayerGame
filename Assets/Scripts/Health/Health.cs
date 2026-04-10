using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;



// Auteur: Stijn Grievink
// User Story: Als speler wil ik een aantal keer geraakt kunnen worden zonder meteen dood te gaan zodat ik de feedback van de hits kan gebruiken om mijn speelstijl aan te passen.
public class Health : MonoBehaviour
{
    public int health = 100;
    [SerializeField] private GameObject _deadVersion;
    private EnemyBehaviour _behaviour;
    [SerializeField] private VolumeProfile _volume;
    private Vignette _vignette;

    // Vignette Colors
    [Header("Vignette Colors")]
    [SerializeField] private ColorParameter _black;
    [SerializeField] private ColorParameter _red;
    [SerializeField] private UnityEngine.UI.Image _playerHealthbar;
    private bool isDead = false;
    private int _maxHealth;

    private void Awake()
    {
        // Set max health to the health set to this entity
        _maxHealth = health;
        // Get the enemy behaviour
        _behaviour = GetComponent<EnemyBehaviour>();

        // If post processing volume is set, change vignette to black at the start
        if (_volume != null)
        {
            _volume.TryGet<Vignette>(out _vignette);
            _vignette.color.Override(Color.black);
        }
    }

    public void Damage(float pAmount)
    {
        // Floor the amount to an int
        int amount = Mathf.FloorToInt(pAmount);
        health -= amount;
        // Check if health is on Enemy when taking damage, then rush
        if (_behaviour != null)
        {
            _behaviour.SetToHunt();
        }
        // Else the health is on the player
        else if (_vignette != null)
        {
            // Change the healthbar
            _playerHealthbar.fillAmount = (float)health / (float)_maxHealth;
            // Set the color of the vignette to red for damage feedback and Invoke set black function after 0.5 seconds
            _vignette.color.Override(Color.red);
            Invoke("SetVignetteBlack", 0.5f);
        }


        // If the health is lower or equal to 0 and the player is not already dead (preventing multiple dead body spawns).
        if (health <= 0 && !isDead)
        {
            // Check if the entity has a dead version of itself set, then spawn it in
            if (_deadVersion != null)
            {
                // Spawn dead object
                GameObject obj = Instantiate(_deadVersion, transform.position, transform.rotation);
                // Apply some random rotation to the dead object so it falls over
                obj.GetComponent<Rigidbody>().angularVelocity = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), Random.Range(-5, 5));
                // Destroy the current object
                Destroy(gameObject);
                isDead = true;
            }
            else
            {
                // If the health script is on the player, reset the scene
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    // Set the vignette to black
    public void SetVignetteBlack()
    {
        _vignette.color.Override(Color.black);
    }
}
