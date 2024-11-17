using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverUI; // Reference to the Game Over screen UI

    // Show the Game Over screen
    public void ShowGameOverScreen()
    {
        gameOverUI.SetActive(true); // Activate the Game Over screen
        Time.timeScale = 0f; // Pause the game (optional)
    }

    // Restart the game by reloading the current scene
    public void RestartGame()
    {
        Time.timeScale = 1f; // Reset the time scale to normal
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload the current scene
    }

    // Quit the game
    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stop playing in the editor
        #else
        Application.Quit(); // Quit the application
        #endif
    }
}
