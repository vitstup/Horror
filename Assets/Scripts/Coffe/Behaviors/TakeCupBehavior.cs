public class TakeCupBehavior : TakeItemInteractionBehaviour<Cup>
{
    protected override string GetText => "взять стаканчик";

    protected override string SetText => "положить стаканчик";
}