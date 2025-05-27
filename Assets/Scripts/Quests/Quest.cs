using System;

public abstract class Quest
{
    public event Action<Quest> OnQuestStarted;
    public event Action<Quest> OnQuestCompleted;
    public event Action<Quest> OnQuestFailed; // логики невыполнения квеста - нет. 

    public abstract string questDescription { get; }

    public void Enter()
    {
        OnQuestStarted?.Invoke(this);
        OnEnter();
    }

    protected virtual void OnEnter() { }

    public virtual void Tick() { }

    protected void Complete()
    {
        OnQuestCompleted?.Invoke(this);
    }
}