using UnityEngine;

public class PhoneToggle : MonoBehaviour
{
    public GameObject phoneUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            phoneUI.SetActive(!phoneUI.activeSelf);
        }
    }
}
