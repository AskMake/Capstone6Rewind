using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryInfo : MonoBehaviour
{
    public ItemInfo itemInfo;
    [SerializeField]
    private Image image;
    [SerializeField]
    private TMP_Text text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        image.sprite = itemInfo.itemImage;
        text.text = itemInfo.objectName;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
