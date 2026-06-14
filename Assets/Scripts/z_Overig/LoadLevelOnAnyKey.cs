using UnityEngine;
using UnityEngine.InputSystem; // Required for the New Input System
using UnityEngine.SceneManagement; // Required for loading scenes

public class LoadLevelOnAnyKey : MonoBehaviour
{
    void Update()
    {
        // Checks ONLY for a keyboard press using the New Input System
        if (Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene(1);
        }
    }
}