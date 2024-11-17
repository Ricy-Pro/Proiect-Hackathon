using UnityEngine;
using UnityEngine.SceneManagement;

public  class GameOverManager : MonoBehaviour
{
    public GameObject gameOverUI; // Reference to the Game Over screen UI

    public void ShowGameOverScreen()
    {
        Debug.Log("ShowGameOverScreen called");
        gameOverUI.SetActive(true); // Activate the Game Over screen
        Time.timeScale = 0f; // Pause the game (optional)
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Reset the time scale
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload the current scene
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f; // Reset the time scale
        SceneManager.LoadScene("MainMenu"); // Load the Main Menu scene (adjust the name as necessary)
    }
}
