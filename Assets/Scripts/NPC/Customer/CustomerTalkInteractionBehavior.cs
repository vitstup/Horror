using PixelCrushers.DialogueSystem.Wrappers;
using UnityEngine;

[RequireComponent(typeof(DialogueSystemTrigger))]
public class CustomerTalkInteractionBehavior : BasicInteracatiobBehavior
{
    [SerializeField] private Transform player;

    [SerializeField] private DialogueSystemEvents events;

    [field: SerializeField] public string conversationOne { get; private set; }
    [field: SerializeField] public string conversationTwo { get; private set; }
    [field: SerializeField] public string conversationThree { get; private set; }

    private DialogueSystemTrigger dialogSystem;

    public override string ToDoText => "поговорить";

    private bool playerIsInFrontOf;

    public override bool IsInteractable => isInteractable && playerIsInFrontOf; 

    protected override void OnAwake()
    {
        dialogSystem = GetComponent<DialogueSystemTrigger>();
        isInteractable = false;

        ChangeConversationTo(conversationOne);

        events.conversationEvents.onConversationEnd.AddListener(DialogueCompleted);
    }

    private void Update()
    {
        Vector3 toPlayer = (player.position - transform.position).normalized;

        toPlayer.y = 0f;

        float angle = Vector3.Angle(transform.forward, toPlayer);

        playerIsInFrontOf = Mathf.Abs(angle) < 45f;
    }

    private void DialogueCompleted(Transform arg0)
    {
        isInteractable = true;
    }

    public override void Interact()
    {
        dialogSystem.TryStart(transform);

        isInteractable = false;
    }

    public void ChangeConversationTo(string to)
    {
        dialogSystem.conversation = to;
    }
}