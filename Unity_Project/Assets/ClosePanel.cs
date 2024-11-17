using UnityEngine;

public class ClosePanel : MonoBehaviour
{
    public GameObject miniGamePanel; // Reference to the MiniGamePanel

    public void CloseMiniGame()
    {
        if (miniGamePanel != null)
        {
            miniGamePanel.SetActive(false); // Hide the panel
        }
        else
        {
            Debug.LogWarning("MiniGamePanel is not assigned in the Inspector!");
        }
    }
}
