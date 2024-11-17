using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel; // Panel for in-game game-over screen
    public Button retryButton;       // Button for restarting the game
    public Button quitButton;        // Button for quitting the game

    // References to the panels that should be deactivated when restarting the game
    public GameObject inventoryPanel;
    public GameObject shopPanel;
    public GameObject shopPanel1;  // Assuming the other ShopPanel is named ShopPanel (1)
    public GameObject gameOverPanelForRestart; // Reference to GameOverPanel for deactivation

    private void Start()
    {
        // Initially hide the Game Over Panel
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        // Add listeners for retry and quit buttons
        if (retryButton != null)
        {
            retryButton.onClick.AddListener(RestartGame);
        }

        if (quitButton != null)
        {
            quitButton.onClick.AddListener(QuitGame);
        }
    }

    // Trigger game over and display the Game Over Panel
    public void GameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
        else
        {
            Debug.LogError("GameOverPanel is not assigned!");
        }
    }

    // Method to restart the game
    public void RestartGame()
    {
        // Deactivate specific Canvas child objects
        if (inventoryPanel != null)
        {
            inventoryPanel.SetActive(false);
        }

        if (shopPanel != null)
        {
            shopPanel.SetActive(false);
        }

        if (shopPanel1 != null)
        {
            shopPanel1.SetActive(false);
        }

        if (gameOverPanelForRestart != null)
        {
            gameOverPanelForRestart.SetActive(false);
        }

        // Reload the current scene
        string sceneName = SceneManager.GetActiveScene().name; // Get the current scene
        SceneManager.LoadScene(sceneName); // Reload the current scene
    }

    // Optionally, a quit method to exit the game
    public void QuitGame()
    {
        Debug.Log("Game Quit");
        Application.Quit();
    }
}
