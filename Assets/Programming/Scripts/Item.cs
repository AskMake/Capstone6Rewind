using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{   
    [SerializeField] private Outline outline;
    [SerializeField] protected ItemInfo itemInfo;
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
        if(!itemInfo)
        {
            Debug.LogError("Give"+ gameObject.name +" ItemInfo");
        }
    }
    public bool IsLooking { get => isLooking; set {// same value ignore to save some work
            if(isLooking == value) return;

            // store the new value in the backing field
            isLooking = value;

            // if one was running cancel the current routine
            if(this && lookingRoutine != null) StopCoroutine(lookingRoutine);

            // start a new routine to apply the outline delayed
            if(this){
            lookingRoutine = StartCoroutine(EnabledOutlineDelayed(value)); }}
}
    public virtual void Interact()
    {
        Debug.Log($"Interacted with {name}", this);
    }
     private IEnumerator EnabledOutlineDelayed(bool enable)
    {
        // wait for the according delay - you can of course adjust this according to your needs
        yield return new WaitForSeconds(enable ? outlineEnableDelay : outlineDisableDelay);     

        // apply state
        outline.enabled = enable; 

        // reset the routine field just to be sure
        lookingRoutine = null;
    }
    
}
