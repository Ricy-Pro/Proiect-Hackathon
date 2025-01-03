using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    // Define the code blocks
    public enum CodeBlockType 
{   
    FirewallBlock1, 
    FirewallBlock2, 
    FirewallBlock3, 
    FirewallBlock4, 
    
    SQLInjectionBlock1, 
    SQLInjectionBlock2, 
    SQLInjectionBlock3, 
    SQLInjectionBlock4, 
    
    DDoSProtectionBlock1, 
    DDoSProtectionBlock2, 
    DDoSProtectionBlock3,
    DDoSProtectionBlock4,  
    
    TwoFactorAuthenticationBlock1, 
    TwoFactorAuthenticationBlock2, 
    TwoFactorAuthenticationBlock3
}


    // Inventory holds the count of each code block
    public int[] inventory = new int[15];  // 15 code blocks in total

    // UI References
    public GameObject inventoryPanel;
    public GameObject codeBlockImagePrefab;
    public Transform gridLayout;

    // Icons for each type of code block (we now have 4 distinct icons)
    public Sprite[] codeBlockIcons;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        // Hide inventory initially
        inventoryPanel.SetActive(false);

        // Initialize the inventory (all items start at 0)
        for (int i = 0; i < inventory.Length; i++)
        {
            inventory[i] = 0;
        }

        // Update the UI to reflect the inventory state
        UpdateInventoryUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) // Toggle Inventory UI on "I" press
        {   Debug.Log("adadadada");
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        }
    }

    // Update the UI to reflect the current inventory
    public void UpdateInventoryUI()
    {
        // Clear any existing buttons
        foreach (Transform child in gridLayout)
        {
            Destroy(child.gameObject);
        }

        // Loop through each inventory slot (15 items total)
        for (int i = 0; i < inventory.Length; i++)
        {
            // Instantiate a new image for each code block
            GameObject newCodeBlockImage = Instantiate(codeBlockImagePrefab, gridLayout);
            Image imageComponent = newCodeBlockImage.GetComponent<Image>();
            TextMeshProUGUI resourceCountText = newCodeBlockImage.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

            // Assign the correct icon based on the index
            imageComponent.sprite = GetCodeBlockIcon(i);

            // Update the resource count for this item
            resourceCountText.text = inventory[i].ToString();
        }
    }

    // Get the appropriate icon for the code block based on the index
    private Sprite GetCodeBlockIcon(int index)
    {
        // Distribute the icons based on index
        if (index < 4)
        {
            return codeBlockIcons[0];  // Firewall icon
        }
        else if (index < 8)
        {
            return codeBlockIcons[1];  // SQL Injection icon
        }
        else if (index < 12)
        {
            return codeBlockIcons[2];  // DDoS Protection icon
        }
        else
        {
            return codeBlockIcons[3];  // Two-Factor Authentication icon
        }
    }

    // Add a code block to the inventory (for testing purposes)
public void AddCodeBlock(CodeBlockType blockType)
{
    // Update inventory based on the blockType (now we handle 15 distinct items)
    int index = (int)blockType;  // Each blockType directly corresponds to an inventory index

    if (index >= 0 && index < inventory.Length)
    {
        inventory[index]++;  // Increment the specific item in the inventory
    }
    else
    {
        Debug.LogWarning("Invalid blockType index!");
    }

    // Update the UI to reflect the inventory state after adding the item
    UpdateInventoryUI();
}

}
