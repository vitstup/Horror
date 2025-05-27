using UnityEngine;

public class ZFightFixer : MonoBehaviour
{
    [Tooltip("������� ����, ������� ����� ������ ��������� �������� �� ������")]
    [SerializeField] private GameObject[] objects;

    [Tooltip("������������ �������� �� Y (� ������)")]
    [SerializeField] private float maxYOffset = 0.001f;

    [ContextMenu("�������� Z fight")]
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