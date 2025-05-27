using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovementAnimCont : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float walkSpeed = 1f;
    [SerializeField] private float runSpeed = 3f;
    [SerializeField] private float smoothing = 10f;

    private Rigidbody rb;
    private float forwardParam;
    private float sideParam;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 velocity = rb.linearVelocity;

        // Убираем вертикальную составляющую (прыжки, падения и т.п.)
        velocity.y = 0;

        // Перевод в локальные координаты (относительно направления персонажа)
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);

        // Нормализация под диапазон -3..+3
        forwardParam = Mathf.Lerp(forwardParam, GetParamValue(localVelocity.z), Time.deltaTime * smoothing);
        sideParam = Mathf.Lerp(sideParam, GetParamValue(localVelocity.x), Time.deltaTime * smoothing);

        animator.SetFloat("Forward", forwardParam);
        animator.SetFloat("Side", sideParam);
    }

    private float GetParamValue(float speed)
    {
        float abs = Mathf.Abs(speed);
        float sign = Mathf.Sign(speed);

        if (abs < 0.1f) return 0f;
        if (abs < (walkSpeed + 0.1f)) return sign * 1f;
        return sign * 3f;
    }
}