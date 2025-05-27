using UnityEngine;

public class Pickable : MonoBehaviour
{
    [field: SerializeField] public Vector3 localPositionOffset { get; private set; }
    [field: SerializeField] public Vector3 localRotationOffset { get; private set; }
    [field: SerializeField] public Vector3 localRotationOffsetWhileTargeting { get; private set; }
}