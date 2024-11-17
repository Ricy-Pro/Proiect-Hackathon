using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    public ShopItem[] shopItems = new ShopItem[]
    {
        new ShopItem { 
            itemName = "Tower1", 
            towerPrefab = null, 
            requiredBlocks = new InventoryManager.CodeBlockType[]
            {   InventoryManager.CodeBlockType.TwoFactorAuthenticationBlock1, 
  
                InventoryManager.CodeBlockType.TwoFactorAuthenticationBlock2,
                InventoryManager.CodeBlockType.TwoFactorAuthenticationBlock3
                
                
            }
        },
        new ShopItem { 
            itemName = "Tower2", 
            towerPrefab = null, 
            requiredBlocks = new InventoryManager.CodeBlockType[]
            {   InventoryManager.CodeBlockType.SQLInjectionBlock1,
                InventoryManager.CodeBlockType.SQLInjectionBlock2,
                InventoryManager.CodeBlockType.SQLInjectionBlock3,
                InventoryManager.CodeBlockType.SQLInjectionBlock4
                
            }
        },
        new ShopItem { 
            itemName = "Tower3", 
            towerPrefab = null, 
            requiredBlocks = new InventoryManager.CodeBlockType[]
            {   InventoryManager.CodeBlockType.DDoSProtectionBlock1,
                InventoryManager.CodeBlockType.DDoSProtectionBlock2,
                InventoryManager.CodeBlockType.DDoSProtectionBlock3,
                InventoryManager.CodeBlockType.DDoSProtectionBlock4

            }
        },
        new ShopItem { 
            itemName = "Tower4", 
            towerPrefab = null, 
            requiredBlocks = new InventoryManager.CodeBlockType[]
            {
                InventoryManager.CodeBlockType.FirewallBlock1,
                InventoryManager.CodeBlockType.FirewallBlock2,
                InventoryManager.CodeBlockType.FirewallBlock3,
                InventoryManager.CodeBlockType.FirewallBlock4
            }
        }
    };

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        shopPanel.SetActive(false);

        if (closeButton != null)
        {
            closeButton.onClick.AddListener(CloseShop);
        }
        else
        {
            Debug.LogWarning("Close button is not assigned!");
        }

        PopulateShop();
    }

    public void CloseShop()
    {
        shopPanel.SetActive(false);
    }

    public void OpenShop()
    {
        shopPanel.SetActive(true);
    }

    private void PopulateShop()
    {
        foreach (Transform child in shopContent)
        {
            Destroy(child.gameObject);
        }

        foreach (var item in shopItems)
        {
            GameObject button = Instantiate(shopItemButtonPrefab, shopContent);
            TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = $"{item.itemName}";

            button.GetComponent<Button>().onClick.AddListener(() => OnBuyItem(item));
        }
    }

    private void OnBuyItem(ShopItem item)
    {
        if (HasRequiredBlocks(item.requiredBlocks))
        {
            UseRequiredBlocks(item.requiredBlocks);
            Debug.Log($"Bought item: {item.itemName}");

            if (item.towerPrefab != null)
            {
                towerPlacementManager.SetTowerToPlace(item.towerPrefab);
            }
            else
            {
                Debug.LogError($"Tower prefab for {item.itemName} is not assigned!");
            }

            CloseShop();
        }
        else
        {
            Debug.Log("Not enough code blocks in inventory!");
        }
    }

    private bool HasRequiredBlocks(InventoryManager.CodeBlockType[] requiredBlocks)
    {
        foreach (var block in requiredBlocks)
        {
            int index = (int)block;
            if (InventoryManager.Instance.inventory[index] <= 0)
            {
                return false; // Missing required block
            }
        }
        return true; // All blocks are available
    }

    private void UseRequiredBlocks(InventoryManager.CodeBlockType[] requiredBlocks)
    {
        foreach (var block in requiredBlocks)
        {
            int index = (int)block;
            InventoryManager.Instance.inventory[index]--; // Deduct the block from inventory
        }

        InventoryManager.Instance.UpdateInventoryUI(); // Update UI after deducting
    }
}
