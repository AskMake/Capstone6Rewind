using UnityEngine;

public class Dialogue : Item
{
    DialogueManager manager;
    bool interacted;
    [SerializeField]
    bool Notebook;
    public override void Interact()
    {
    {
        interacted = true;
        manager.StartDialogue(itemInfo,Notebook);
        base.Interact();

    }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       manager = DialogueManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
