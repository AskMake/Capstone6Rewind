using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set;}
    [SerializeField]
    private GameObject inventory;
    [SerializeField]
    private GameObject itemGO;
    private List<ItemInfo> items= new();
    private List<GameObject> gameObjects = new();
    void Awake()
    {
        if (Instance == null){
        Instance = this;
        }
        else{Destroy(gameObject);}
    }
    public void Inventory()
    {
        Cursor.visible = !inventory.activeInHierarchy;
        if(inventory.activeInHierarchy)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
        inventory.SetActive(!inventory.activeInHierarchy);
    }
    public void AddToInventory(ItemInfo itemInfo)
    {
        items.Add(itemInfo);
        InventoryItemUI(itemInfo);
    }

    private void InventoryItemUI(ItemInfo item)
    {
            GameObject temp = Instantiate(itemGO,inventory.transform);
            temp.GetComponent<InventoryInfo>().itemInfo = item;
            gameObjects.Add(temp);
            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].GetComponent<RectTransform>().anchoredPosition3D = new Vector3(100+i*200, -50, 0);

            }
    }
    public bool KeyCheck()
    {
        foreach(var i in items)
        {
            if(i.isKey)
            {   
                items.Remove(i);
                return true;    
            }
        }
        return false;
    }
}
