using UnityEngine;

public interface IMovableBehavior : IBehavior
{
    public bool IsTargetReached { get; }

    public void MoveTo(Vector3 to);
}