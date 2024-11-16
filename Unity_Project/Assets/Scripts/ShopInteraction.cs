using UnityEngine;

public class ShopBuilding : MonoBehaviour
{
    // Reference to the ShopManager (Singleton)
    public ShopManager shopManager;

    private void Start()
    {
        // Ensure ShopManager is referenced properly
        if (shopManager == null)
        {
            shopManager = ShopManager.Instance;
        }
    }

    private void OnMouseDown()
    {
        // When the player clicks on the building, open the shop
        if (shopManager != null)
        {
            shopManager.OpenShop();
        }
    }
}
