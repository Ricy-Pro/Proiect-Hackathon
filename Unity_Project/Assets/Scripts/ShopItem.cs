using UnityEngine;

[System.Serializable] // This attribute allows us to see this class in the Inspector
public class ShopItem
{
    public string itemName; // Name of the item
    public int price; // Price of the item in gold
    public InventoryManager.CodeBlockType blockType; // Type of code block that is purchased
}
