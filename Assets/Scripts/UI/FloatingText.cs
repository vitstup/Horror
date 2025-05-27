using System.Collections;
using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private float lifetime = 1.5f;
    [SerializeField] private float floatHeight = 1f;
    [SerializeField] private float moveSpeed = 1f;

    private Transform cam;

    public void Init(string text)
    {
        cam = FindObjectOfType<Camera>(false).transform;
        textMesh.text = text;
        StartCoroutine(Floating());
    }

    private IEnumerator Floating()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + Vector3.up * floatHeight;
        float elapsed = 0f;

        while (elapsed < lifetime)
        {
            // Всегда поворачивается к камере
            transform.rotation = Quaternion.LookRotation(transform.position - cam.position);

            // Поднимается вверх
            transform.position = Vector3.Lerp(startPos, endPos, elapsed / lifetime);

            elapsed += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}