using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{   
    [SerializeField] private Outline outline;
    // adjust delays in seconds
    [SerializeField] private float outlineEnableDelay = 0.5f;
    [SerializeField] private float outlineDisableDelay = 0.5f;

    // stores currently running routine (see below)
    private Coroutine lookingRoutine;
    // backing field for the IsLooking property
    private bool isLooking;

    private void Awake()
    {
        if(!outline) outline = GetComponent<Outline>();
        outline.enabled = false;
    }
    public bool IsLooking { get => isLooking; set {// same value ignore to save some work
            if(isLooking == value) return;

            // store the new value in the backing field
            isLooking = value;

            // if one was running cancel the current routine
            if(lookingRoutine != null) StopCoroutine(lookingRoutine);

            // start a new routine to apply the outline delayed
            lookingRoutine = StartCoroutine(EnabledOutlineDelayed(value)); }}

    public virtual void Interact()
    {
        Debug.Log($"Interacted with {name}", this);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
     private IEnumerator EnabledOutlineDelayed(bool enable)
    {
        // wait for the according delay - you can of course adjust this according to your needs
        yield return new WaitForSeconds(enable ? outlineEnableDelay : outlineDisableDelay);     

        // apply state
        outline.enabled = true; 

        // reset the routine field just to be sure
        lookingRoutine = null;
    }
    
}
