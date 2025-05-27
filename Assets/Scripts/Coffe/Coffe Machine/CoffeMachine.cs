using UnityEngine;

[RequireComponent(typeof(SetCupToCoffeMachineBehavior))]
[RequireComponent(typeof(StartCreatiingCoffeBehavior))]
[RequireComponent(typeof(SetUpLidBehavior))]
[RequireComponent(typeof(GetCoffeBehavior))]
public class CoffeMachine : MonoBehaviour, IBehaviorHandler
{
    private BehaviorHandler handler = new BehaviorHandler();

    public BehaviorHandler Handler => handler;

    private SetCupToCoffeMachineBehavior p1;
    private StartCreatiingCoffeBehavior p2;
    private SetUpLidBehavior p3;
    private GetCoffeBehavior p4;

    private void Awake()
    {
        p1 = GetComponent<SetCupToCoffeMachineBehavior>();
        p2 = GetComponent<StartCreatiingCoffeBehavior>();
        p3 = GetComponent<SetUpLidBehavior>();
        p4 = GetComponent<GetCoffeBehavior>();

        handler.RegisterBehavior(p1);
    }

    public void ChangeToPartTwo(Cup cup)
    {
        p2.SetInitData(cup);

        handler.RegisterBehavior(p2);
    }

    public void ChangeToPartThree(Cup cup)
    {
        p3.SetInitData(cup);

        handler.RegisterBehavior(p3);
    }

    public void ChangeToPartFour(Cup cup)
    {
        p4.SetInitData(cup);

        handler.RegisterBehavior(p4);
    }

    public void ChangeToPartOne()
    {
        handler.RegisterBehavior(p1);
    }
}