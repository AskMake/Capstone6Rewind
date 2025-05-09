using UnityEngine;

public class Key : Dialogue
{
    InventoryManager inventoryManager;
    public override void Interact()
    {
        inventoryManager = InventoryManager.Instance;
        base.Interact();
        inventoryManager.AddToInventory(itemInfo);
        Destroy(gameObject);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {
        
    }
}
