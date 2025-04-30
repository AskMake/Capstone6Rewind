using UnityEngine;

public class Door : Item
{
    InventoryManager inventoryManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Interact()
    {
        if(inventoryManager.KeyCheck())
        {
            
        }
        base.Interact();
    }
    void Start()
    {
        inventoryManager = InventoryManager.Instance;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
