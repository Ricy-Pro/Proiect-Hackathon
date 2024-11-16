using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GoldUI : MonoBehaviour
{
    public TMP_Text goldText; // Reference to the Text UI that will display the gold amount

    void OnEnable()
    {
        // Subscribe to the GoldChanged event when this script is enabled
        GoldManager.GoldChanged += UpdateGoldUI;
    }

    void OnDisable()
    {
        // Unsubscribe when this script is disabled to prevent memory leaks
        GoldManager.GoldChanged -= UpdateGoldUI;
    }

    // This method is called whenever the gold changes
    void UpdateGoldUI(int newGoldAmount)
    {
        // Update the Text UI with the new amount of gold
        goldText.text = "Gold: " + newGoldAmount.ToString();
    }
}
