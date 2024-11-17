using UnityEngine;

public class Firewall_Gen : MonoBehaviour
{
    // Range for the random number
    public int minRandomValue = 1;
    public int maxRandomValue = 4;
    public MiniGameManager miniGameManager;
    private void Start()
    {
        // Start the coroutine to generate random numbers every minute
        StartCoroutine(GenerateRandomNumberEveryMinute());
    }
    private void OnMouseDown()
    {
        Debug.Log("The object was clicked!");
        int randomBlockNumber = Random.Range(1, 5);
        // Start the mini-game for this specific block type
        string randomBlockType = "Firewall Block " + randomBlockNumber;
        miniGameManager.StartMiniGame(randomBlockType);
}

    private System.Collections.IEnumerator GenerateRandomNumberEveryMinute()
    {
        while (true)
        {
            // Wait for 60 seconds
            yield return new WaitForSeconds(60f);

            // Generate the random number
            int randomValue = Random.Range(minRandomValue, maxRandomValue);

            // Call the method to handle the random number
            HandleRandomNumber(randomValue);
        }
    }

    // Method to handle the generated random number
    private void HandleRandomNumber(int randomValue)
    {
        Debug.Log("Generated Random Number: " + randomValue);

        switch (randomValue)
        {
            case 1:
            InventoryManager.Instance.AddCodeBlock(InventoryManager.CodeBlockType.FirewallBlock1);
                break;
            case 2:
            InventoryManager.Instance.AddCodeBlock(InventoryManager.CodeBlockType.FirewallBlock2);
                break;
            case 3:
            InventoryManager.Instance.AddCodeBlock(InventoryManager.CodeBlockType.FirewallBlock3);
                break;
            case 4:
            InventoryManager.Instance.AddCodeBlock(InventoryManager.CodeBlockType.FirewallBlock4);
                break;
            default:
                Debug.Log("Unexpected number generated.");
                break;
        }

        // Add your custom logic here to process the random number
    }


    // Custom method for click behavior
    private void PerformClickAction()
    {
        // Example action: Log a message or perform some action
        Debug.Log("Performing click action! Add your custom logic here.");
    }
}