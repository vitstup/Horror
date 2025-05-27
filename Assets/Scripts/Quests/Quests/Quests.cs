using System;
using Cysharp.Threading.Tasks;
using PixelCrushers.DialogueSystem;

public class WaitCustomersQuest : Quest
{
    private NPC customer;

    public WaitCustomersQuest(NPC customer)
    {
        this.customer = customer;

        WaitCustomerRoutine().Forget();
    }

    private async UniTask WaitCustomerRoutine()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f), DelayType.Realtime);

        await UniTask.WaitUntil(() => customer.Handler.GetBehavior<IMovableBehavior>().IsTargetReached);

        Complete();
    }

    public override string questDescription { get => "��������� ��������"; }
}

public class TalkWithCustomerQuest : Quest
{
    public TalkWithCustomerQuest(DialogueSystemEvents events)
    {
        events.conversationEvents.onConversationEnd.AddListener((t) => Complete());
    }

    public override string questDescription => "���������� � ��������";
}

public class GetCupQuest : Quest
{
    public override string questDescription => "�������� ���������";

    public GetCupQuest(CarryItemHandler carryItems)
    {
        WaitForCupTaken(carryItems).Forget();
    }

    private async UniTask WaitForCupTaken(CarryItemHandler carryItems)
    {
        await UniTask.WaitUntil(() => carryItems.HasItem && carryItems.carriedItem.TryGetComponent<Cup>(out var cup)); // ��� ���� ������������������, �� � ����� ����������. ���� �� ��������� ��� ������� ���� ������� ���.

        Complete();
    }
}

public class CreateCoffeQuest : Quest
{
    public override string questDescription => "�������� ����������, ������� ����";

    public CreateCoffeQuest(CoffeMachine coffeMachine)
    {
        WaitForComplete(coffeMachine).Forget();
    }

    private async UniTask WaitForComplete(CoffeMachine coffeMachine)
    {
        await UniTask.WaitUntil(() => (coffeMachine.Handler.GetBehavior<IInteractableBehavior>() is SetUpLidBehavior));

        Complete();
    }
}

public class GetLidQuest : Quest
{
    public override string questDescription => "�������� ��������, �������� �� ���������";

    public GetLidQuest(CoffeMachine coffeMachine)
    {
        WaitForComplete(coffeMachine).Forget();
    }

    private async UniTask WaitForComplete(CoffeMachine coffeMachine)
    {
        await UniTask.WaitUntil(() => (coffeMachine.Handler.GetBehavior<IInteractableBehavior>() is GetCoffeBehavior));

        Complete();
    }
}

public class GetCoffeQuest : Quest
{
    public override string questDescription => "�������� ���� �������";

    public GetCoffeQuest(NPC customer)
    {
        WaitForComplete(customer).Forget();
    }

    private async UniTask WaitForComplete(NPC customer)
    {
        await UniTask.WaitUntil(() => (customer.Handler.GetBehavior<IInteractableBehavior>() is CustomerTakeCofeInteractionBehavior takeCoffeBeh) && takeCoffeBeh.CoffeWasTaken);

        Complete();
    }
}