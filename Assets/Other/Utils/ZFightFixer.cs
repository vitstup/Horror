using UnityEngine;

public class ZFightFixer : MonoBehaviour
{
    [Tooltip("Объекты пола, которым нужно задать небольшое смещение по высоте")]
    [SerializeField] private GameObject[] objects;

    [Tooltip("Максимальное смещение по Y (в метрах)")]
    [SerializeField] private float maxYOffset = 0.001f;

    [ContextMenu("Починить Z fight")]
    private void ApplyZFightOffset()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i] == null) continue;

            float offset = i * maxYOffset;
            Vector3 pos = objects[i].transform.position;
            pos.y += offset;
            objects[i].transform.position = pos;
        }
    }
}