public interface IInteractableBehavior : IBehavior
{
    public bool IsInteractable { get; }

    public string ToDoText { get; }

    public void Interact();

    public void ChangeInteractability(bool interactable);

    public void EnableOutline(bool enable);
}