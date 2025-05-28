using System;
using Cysharp.Threading.Tasks;
using PixelCrushers.DialogueSystem;
using UnityEngine;

public class CustomerScenario : MonoBehaviour, IScenario
{
    public event Action Completed;

    [SerializeField] private QuestHandler questHandler;

    [SerializeField] private Customer customer;

    [SerializeField] private CustomerTalkInteractionBehavior customerTalkBeh;

    [SerializeField] private Transform customerStandPos;

    [SerializeField] private DialogueSystemEvents dialogueEvents;

    [SerializeField] private CarryItemHandler carryingHandler;

    [SerializeField] private CoffeMachine coffeMachine;

    [SerializeField] private BlinkEffect blink;

    public async UniTask StartScenario()
    {
        await UniTask.Delay(500);

        customer.Handler.GetBehavior<IMovableBehavior>().MoveTo(customerStandPos.transform.position);

        blink.PlayFromBlackBlink().Forget();

        var q = new WaitCustomersQuest(customer);

        questHandler.GiveQuest(q);

        questHandler.OnQuestCompleted(q, (cq) => OnQ1Comp(cq));

        await blink.PlayFromBlackBlink();

        await UniTask.Delay(500);

        blink.PlayBlink().Forget();
    }

    private void OnQ1Comp(Quest completed)
    {
        customer.Handler.GetBehavior<IInteractableBehavior>().ChangeInteractability(true);

        var q = new TalkWithCustomerQuest(dialogueEvents);

        questHandler.GiveQuest(q);

        questHandler.OnQuestCompleted(q, (cq) => OnQ2Comp(cq));
    }

    private void OnQ2Comp(Quest completed)
    {
        WaitForHorrorStart().Forget();

        customer.Order();

        customerTalkBeh.ChangeConversationTo(customerTalkBeh.conversationTwo);

        var q = new GetCupQuest(carryingHandler);

        questHandler.GiveQuest(q);

        questHandler.OnQuestCompleted(q, (cq) => OnQ3Comp(cq));
    }

    private void OnQ3Comp(Quest completed)
    {
        var q = new CreateCoffeQuest(coffeMachine);

        questHandler.GiveQuest(q);

        questHandler.OnQuestCompleted(q, (cq) => OnQ4Comp(cq));
    }

    private void OnQ4Comp(Quest completed)
    {
        var q = new GetLidQuest(coffeMachine);

        questHandler.GiveQuest(q);

        questHandler.OnQuestCompleted(q, (cq) => OnQ5Comp(cq));
    }

    private void OnQ5Comp(Quest completed)
    {
        var q = new GetCoffeQuest(customer);

        questHandler.GiveQuest(q);

        // questHandler.OnQuestCompleted(q, (cq) => OnQ6Comp(cq));
    }

    private async UniTask WaitForHorrorStart()
    {
        await UniTask.WaitUntil(() => customer.takeCoffeBeh.CoffeWasTaken);

        Completed?.Invoke();
    }
}