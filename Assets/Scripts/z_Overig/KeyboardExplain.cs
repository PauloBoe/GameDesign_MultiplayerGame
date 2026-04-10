//Auteur: Paulo
//UserStory: 18 belangrijke UI elementen

using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyboardScene : MonoBehaviour
{
    private void Update()
    {
        StartGame();
    }

    public void StartGame()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(1);
        }
    }
}
