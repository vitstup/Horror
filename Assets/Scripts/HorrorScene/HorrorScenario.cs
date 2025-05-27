using Cysharp.Threading.Tasks;
using PixelCrushers.DialogueSystem;
using UnityEngine;

public class HorrorScenario : MonoBehaviour
{
    [SerializeField] private ControlsHandler controls;

    [SerializeField] private Camera mainCamera;

    [SerializeField] private Transform player;

    [SerializeField] private Screamer screamer;

    [SerializeField] private CanvasesController canvases;

    [SerializeField] private BlinkEffect blink;

    [Header("First Part")]
    [SerializeField] private NPC customer;
    [SerializeField] private CustomerTalkInteractionBehavior customerTalkBehavior;
    [SerializeField] private DialogueSystemEvents events;
    [SerializeField] private AudioSource stairingSound;
    [SerializeField] private Transform stairingAtPoint;
    [SerializeField] private float rotationDurationInPartOne = .1f;
    [SerializeField] private Transform customerGoToPoint;

    [Header("Second Part")]
    [SerializeField] private Transform screamerSpawnPoint; 
    [SerializeField] private float maxScreamerDistance = 1.5f; 
    [SerializeField] private float rotationDurationInPartTwo = .1f;
    [SerializeField] private AudioSource hearbeatSound;
    [SerializeField] private AudioSource doorSound;
    [SerializeField] private AudioSource screamerSound;
    [SerializeField] private AudioSource punchSound;
    [SerializeField] private AudioSource whiteNoiseSound;
    [SerializeField] private GameObject endObj;

    private bool isConversationWithCustomerEnded;

    public async UniTaskVoid StartScenario()
    {
        events.conversationEvents.onConversationEnd.AddListener((t) => OnConverstationEnded());

        await UniTask.Delay(500);

        controls.DisableControls();

        customerTalkBehavior.ChangeConversationTo(customerTalkBehavior.conversationThree);

        customerTalkBehavior.Interact();

        await UniTask.Delay(1000);

        screamer.transform.position = stairingAtPoint.position;
        screamer.transform.rotation = stairingAtPoint.rotation;
        screamer.gameObject.SetActive(true);

        screamer.Stare();

        stairingSound.Play();

        await UniTask.Delay(300);

        hearbeatSound.Play();

        await LookAtTarget(screamer.lookTo, rotationDurationInPartOne, 0.5f);

        screamer.gameObject.SetActive(false);

        await blink.PlayBlink();

        await UniTask.WaitUntil(() => isConversationWithCustomerEnded);

        customer.Handler.GetBehavior<IMovableBehavior>().MoveTo(customerGoToPoint.position);

        await UniTask.WaitForEndOfFrame();

        controls.DisableControls();

        controls.DisableCursor();

        await LookAtTarget(customer.head, 0.2f, 1.5f);

        var density = RenderSettings.fogDensity;

        RenderSettings.fogDensity = 1f;

        await UniTask.Delay(100);

        RenderSettings.fogDensity = density;

        await UniTask.Delay(300);

        RenderSettings.fogDensity = 1f;

        // second part

        await UniTask.Delay(500);

        canvases.DisableCanvases();

        doorSound.Play();

        await UniTask.Delay(1500);

        screamerSound.Play();

        await UniTask.Delay(300);

        screamer.transform.position = screamerSpawnPoint.position;
        screamer.transform.rotation = screamerSpawnPoint.rotation;
        screamer.gameObject.SetActive(true);

        if (Vector3.Distance(screamer.transform.position, player.position) > maxScreamerDistance)
        {
            Vector3 direction = (player.position - screamer.transform.position).normalized;

            Vector3 newPosition = player.position - direction * maxScreamerDistance;

            screamer.transform.position = newPosition;
        }

        screamer.Jumpscare();

        await LookAtTarget(screamer.lookTo, rotationDurationInPartTwo, .5f);

        punchSound.Play();

        endObj.SetActive(true);

        canvases.EnableEnd();

        hearbeatSound.Stop();

        screamer.gameObject.SetActive(false);

        await UniTask.Delay(200);

        whiteNoiseSound.Play();
    }

    private void OnConverstationEnded()
    {
        events.conversationEvents.onConversationEnd.RemoveListener((t) => OnConverstationEnded());

        isConversationWithCustomerEnded = true;
    }

    private async UniTask LookAtTarget(Transform target, float aimDuration, float holdDuration)
    {
        // --- Инициализация начальных поворотов ---
        Quaternion startPlayerRot = player.rotation;

        // направление до цели
        Vector3 toTarget = target.position - player.position;
        Quaternion targetPlayerRot = Quaternion.Euler(0f, Quaternion.LookRotation(toTarget).eulerAngles.y, 0f);

        // Поворот камеры (X) — вычисляем pitch
        float startXRot = mainCamera.transform.localEulerAngles.x;
        startXRot = (startXRot > 180f) ? startXRot - 360f : startXRot; // нормализуем угол

        Vector3 camToTarget = (target.position - mainCamera.transform.position).normalized;
        float targetPitch = -Mathf.Asin(camToTarget.y) * Mathf.Rad2Deg;
        targetPitch = Mathf.Clamp(targetPitch, -80f, 80f); // защита от дикого угла

        // --- Плавный поворот ---
        float elapsed = 0f;
        while (elapsed < aimDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / aimDuration);

            // Игрок (Y)
            player.rotation = Quaternion.Slerp(startPlayerRot, targetPlayerRot, t);

            // Камера (X)
            float currentPitch = Mathf.Lerp(startXRot, targetPitch, t);
            mainCamera.transform.localRotation = Quaternion.Euler(currentPitch, 0f, 0f);

            await UniTask.Yield();
        }

        // Финальное выравнивание
        player.rotation = targetPlayerRot;
        mainCamera.transform.localRotation = Quaternion.Euler(targetPitch, 0f, 0f);

        // --- Удержание взгляда на цели ---
        await StareAt(target, holdDuration);
    }

    private async UniTask StareAt(Transform target, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            // --- Поворот игрока (Y) ---
            Vector3 toTarget = target.position - player.position;
            Quaternion targetPlayerRot = Quaternion.Euler(0f, Quaternion.LookRotation(toTarget).eulerAngles.y, 0f);
            player.rotation = Quaternion.Slerp(player.rotation, targetPlayerRot, 0.2f); // немного сглаживания

            // --- Поворот камеры (X) ---
            Vector3 camToTarget = (target.position - mainCamera.transform.position).normalized;
            float targetPitch = -Mathf.Asin(camToTarget.y) * Mathf.Rad2Deg;
            targetPitch = Mathf.Clamp(targetPitch, -80f, 80f); // защита от дикого угла

            float currentX = mainCamera.transform.localEulerAngles.x;
            currentX = (currentX > 180f) ? currentX - 360f : currentX;
            float newPitch = Mathf.Lerp(currentX, targetPitch, 0.2f); // сглаживаем

            mainCamera.transform.localRotation = Quaternion.Euler(newPitch, 0f, 0f);

            await UniTask.Yield();
        }
    }
}