using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ShopManager2 : MonoBehaviour
{
    public static ShopManager2 Instance;

    public GameObject shopPanel;
    public GameObject shopItemButtonPrefab;
    public Transform shopContent;
    public Button closeButton;

    public TowerPlacementManager towerPlacementManager;

    [System.Serializable]
    public class ShopItem
    {
        public string itemName;
        public GameObject towerPrefab;
        public InventoryManager.CodeBlockType[] requiredBlocks; // Blocks needed for this tower
    }

    public ShopItem[] shopItems = new ShopItem[] { /* your items here */ };

    private void Awake()
    {
        // Ensure that only one ShopManager2 instance exists.
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Automatically find and assign the required components or objects
        if (shopPanel == null) 
            shopPanel = GameObject.Find("ShopPanel"); // Find the shop panel in the scene
        if (shopItemButtonPrefab == null)
            shopItemButtonPrefab = Resources.Load<GameObject>("ShopItemButtonPrefab"); // Use Resources if prefabs are not assigned
        if (shopContent == null)
            shopContent = GameObject.Find("ShopContent").transform; // Find the shop content object in the scene
        if (closeButton == null)
            closeButton = GameObject.Find("CloseButton").GetComponent<Button>(); // Find the close button
        if (towerPlacementManager == null)
            towerPlacementManager = GameObject.FindObjectOfType<TowerPlacementManager>(); // Find the TowerPlacementManager in the scene

        // Proceed with setup and UI population
        PopulateShop();  // Populate the shop items
        closeButton.onClick.AddListener(CloseShop);  // Add listener for closing the shop
    }

    // Opens the shop panel
    public void OpenShop()
    {
        if (shopPanel != null)
        {
            shopPanel.SetActive(true); // Show the shop panel
        }
    }

    // Closes the shop panel
    public void CloseShop()
    {
        if (shopPanel != null)
        {
            shopPanel.SetActive(false); // Hide the shop panel
        }
    }

    // Populate the shop panel with items
    private void PopulateShop()
    {
        // Clear any existing items in the shop content
        foreach (Transform child in shopContent)
        {
            Destroy(child.gameObject);
        }

        // Instantiate new buttons for each shop item
        foreach (var item in shopItems)
        {
            GameObject button = Instantiate(shopItemButtonPrefab, shopContent);
            TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = $"{item.itemName}";

            button.GetComponent<Button>().onClick.AddListener(() => OnBuyItem(item));  // Add listener for each item button
        }
    }

    // Handle buying an item
    private void OnBuyItem(ShopItem item)
    {
        if (HasRequiredBlocks(item.requiredBlocks))
        {
            UseRequiredBlocks(item.requiredBlocks);
            Debug.Log($"Bought item: {item.itemName}");

            if (item.towerPrefab != null)
            {
                towerPlacementManager.SetTowerToPlace(item.towerPrefab);  // Set the tower to be placed
            }
            else
            {
                Debug.LogError($"Tower prefab for {item.itemName} is not assigned!");
            }

            CloseShop(); // Close the shop after buying
        }
        else
        {
            Debug.Log("Not enough code blocks in inventory!");
        }
    }

    // Check if the player has all required blocks to buy the item
    private bool HasRequiredBlocks(InventoryManager.CodeBlockType[] requiredBlocks)
    {
        foreach (var block in requiredBlocks)
        {
            int index = (int)block;
            if (InventoryManager.Instance.inventory[index] <= 0)
            {
                return false;  // Missing required block
            }
        }
        return true;  // All blocks are available
    }

    // Deduct the blocks from inventory after purchasing an item
    private void UseRequiredBlocks(InventoryManager.CodeBlockType[] requiredBlocks)
    {
        foreach (var block in requiredBlocks)
        {
            int index = (int)block;
            InventoryManager.Instance.inventory[index]--; // Deduct the block from inventory
        }

        InventoryManager.Instance.UpdateInventoryUI();  // Update inventory UI after deducting blocks
    }

    private void OnEnable()
    {
        shopPanel.SetActive(false);  // Ensure it starts hidden
    }

    private void OnDisable()
    {
        // Remove listeners to avoid duplicates
        if (closeButton != null)
        {
            closeButton.onClick.RemoveListener(CloseShop);
        }
    }
}
