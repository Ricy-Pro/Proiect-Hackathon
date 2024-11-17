using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    public GameObject miniGamePanel; // Reference to the MiniGamePanel

    // Function to show the panel
    public void ShowMiniGamePanel()
    {
        if (miniGamePanel != null)
        {
            miniGamePanel.SetActive(true); // Enable the panel
        }
    }

    // Function to hide the panel
    public void CloseMiniGamePanel()
    {
        if (miniGamePanel != null)
        {
            miniGamePanel.SetActive(false); // Disable the panel
        }
    }
}
