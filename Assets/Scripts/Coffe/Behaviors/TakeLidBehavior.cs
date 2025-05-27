public class TakeLidBehavior : TakeItemInteractionBehaviour<Lid>
{
    protected override string GetText => "взять крышку";

    protected override string SetText => "положить крышку";
}