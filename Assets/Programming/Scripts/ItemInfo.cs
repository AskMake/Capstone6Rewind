using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ItemInfo", menuName = "Scriptable Objects/ItemInfo")]
public class ItemInfo : ScriptableObject
{
    public Sprite itemImage;
    public bool isKey;
    public bool isDialogue;
    public string[] lines;
    public string objectName;
    public string itemDescription;

}
