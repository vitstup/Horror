using UnityEngine;

[RequireComponent(typeof(Outline))]
public abstract class BasicInteracatiobBehavior : MonoBehaviour, IInteractableBehavior
{
    protected bool isInteractable = true;

    public virtual bool IsInteractable => isInteractable;

    public abstract string ToDoText { get; }

    protected Outline outline;

    private void Awake()
    {
        outline = GetComponent<Outline>();

        EnableOutline(false);

        OnAwake();
    }

    protected virtual void OnAwake()
    {

    }

    public virtual void ChangeInteractability(bool interactable)
    {
        isInteractable = interactable;
    }

    public void EnableOutline(bool enable)
    {
        if (outline.enabled != enable)
            outline.enabled = enable;
    }

    public abstract void Interact();
}