using UnityEngine;
using TMPro;

public class TutorialMessage : MonoBehaviour
{
    public TextMeshProUGUI tutorialText; // Assign in inspector
    public float displayTime = 15f;

    void Start()
    {
        if (tutorialText != null)
        {
            tutorialText.gameObject.SetActive(true);
            Invoke("HideMessage", displayTime);
        }
    }

    void HideMessage()
    {
        tutorialText.gameObject.SetActive(false);
    }
}
