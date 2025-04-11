using UnityEngine;
using static Outline;

[RequireComponent(typeof(Outline))]
public class Interactable : MonoBehaviour
{
    [SerializeField]
    protected Outline outline;
    protected virtual void Start()
    {
    }
    protected virtual void Update()
    {
    }
    public virtual void Outline()
    {
        if(outline.OutlineMode != Mode.OutlineAll)
        {
        outline.OutlineMode = Mode.OutlineAll;
        }
    }
}
