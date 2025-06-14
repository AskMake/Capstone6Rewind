using UnityEngine;
using TMPro;

public class PhoneAndTutorialManager : MonoBehaviour
{
    public GameObject phoneUI;
    public GameObject noteUI;
    public TextMeshProUGUI tutorialText;
    public TextMeshProUGUI noteTutText;
    public float tutorialDuration = 15f;

    private float tutorialTimer;
    [SerializeField]
    private float noteTutTimer;
    private bool tutorialActive = true;
    private bool noteTut = true;
    private bool phoneIsActive = false;
    bool  noteBookActive ;
    public bool isGiven;

    void Start()
    {
        phoneUI.SetActive(false); // Hide phone on start
        tutorialText.gameObject.SetActive(true);
        noteUI.SetActive(false);
        tutorialTimer = tutorialDuration;
    }

    void Update()
    {
        // Tutorial countdown (only if not hidden by phone)
        if (tutorialActive && !phoneIsActive)
        {
            tutorialTimer -= Time.deltaTime;
            if (tutorialTimer <= 0f)
            {
                tutorialText.gameObject.SetActive(false);
                tutorialActive = false;
            }
        }

        // Toggle phone on P key press
        if (Input.GetKeyDown(KeyCode.P) && !noteBookActive)
        {
            phoneIsActive = !phoneIsActive;
            phoneUI.SetActive(phoneIsActive);

            // Handle tutorial text visibility
            if (phoneIsActive)
            {
                tutorialText.gameObject.SetActive(false);
            }
            else if (tutorialActive)
            {
                tutorialText.gameObject.SetActive(true);
            }
        }
         if (isGiven && Input.GetKeyDown(KeyCode.N) && !phoneIsActive)
        {
            noteBookActive = !noteBookActive;
            noteUI.SetActive(noteBookActive);

            // Handle tutorial text visibility
            if (noteBookActive)
            {
                tutorialText.gameObject.SetActive(false);
            }
            else if (tutorialActive)
            {
                tutorialText.gameObject.SetActive(true);
            }
        }
        if ((isGiven && noteTut) && !phoneIsActive)
        {
            noteTutText.gameObject.SetActive(true);
            noteTutTimer -= Time.deltaTime;
            if (noteTutTimer <= 0f)
            {
                noteTutText.gameObject.SetActive(false);
                noteTut = false;
            }
    }
}
}