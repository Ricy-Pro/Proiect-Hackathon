using UnityEngine;

public class ShopBuilding2 : MonoBehaviour
{
    // Reference to the ShopManager2 (Singleton)
    public ShopManager2 shopManager;

    private void Start()
    {
        // Ensure ShopManager2 is referenced properly
        if (shopManager == null)
        {
            shopManager = ShopManager2.Instance; // Correct the type here
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
