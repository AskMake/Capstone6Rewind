using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance {
        get;
        private set;
    }
    [SerializeField]
    Player player;
    InputAction interactAction;
    public GameObject dialogueBox;
    public TMP_Text character;
    public TMP_Text dialogue;
    [SerializeField]
    private float textSpeed;
    [SerializeField]
    private string[] lines;
    private int index;
    private bool finished;
    [SerializeField]
    PhoneAndTutorialManager phone;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else{
            Destroy(gameObject);
        }
        interactAction = InputSystem.actions.FindAction("Continue");
    }

    // Update is called once per frame
    void Update()
    {
        if (interactAction.IsPressed() && lines.Length > 0)
        {
            if (dialogue.text == lines[index] && finished)
            {
                NextLine();
            }
            // else
            // {
            //     StopAllCoroutines();
            //     dialogue.text = lines[index];
            // }
        }
    }

    public void StartDialogue(ItemInfo itemInfo)
    {
        dialogueBox.SetActive(true);
        player.ChangeButtonMap();
        player.canMove = false;
        player.inDialogue = true;
        dialogue.text = string.Empty;
        character.text = itemInfo.objectName;
        lines = itemInfo.lines;
        index = 0;
        if(lines.Length > 0){
            StartCoroutine(TypeLine());
        }
    }
    public void StartDialogue(ItemInfo itemInfo, bool givePhone)
    {
        StartDialogue(itemInfo);
        phone.isGiven= givePhone;
    }

    IEnumerator TypeLine()
    {
        finished = false;
        foreach (char c in lines[index].ToCharArray())
        {
            dialogue.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        finished = true;
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            dialogue.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            player.ChangeButtonMap();
            player.canMove = true;
            dialogueBox.SetActive(false);
            index = 0;
            player.inDialogue = false;
        }
    }
}
