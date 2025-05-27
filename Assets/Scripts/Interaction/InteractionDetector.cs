using TMPro;
using UnityEngine;

public class InteractionDetector : MonoBehaviour
{
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float interactRange = 3f;
    [SerializeField] private LayerMask interactLayerMask;

    [SerializeField] private Canvas interactionCanvas;
    [SerializeField] private TextMeshProUGUI interactionText;

    private IInteractableBehavior currentTarget;

    void Update()
    {
        currentTarget?.EnableOutline(false);

        CheckForInteractable();

        if (currentTarget != null)
        {
            currentTarget?.EnableOutline(true);

            if (Input.GetKeyDown(inputHandler.interactionKey))
                currentTarget.Interact();
        }
    }

    private void CheckForInteractable()
    {
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, interactRange, interactLayerMask))
        {
            if (hit.collider.TryGetComponent(out IBehaviorHandler behaviorHandler))
            {
                var interactable = behaviorHandler.Handler.GetBehavior<IInteractableBehavior>();

                if (interactable != null && interactable.IsInteractable)
                {
                    currentTarget = interactable;
                    ShowHint($"ֽאזלטעו {inputHandler.interactionKey}, קעמבû {interactable.ToDoText}");
                    return;
                }
            }
        }

        currentTarget = null;
        HideHint();
    }

    private void ShowHint(string message)
    {
        interactionCanvas.gameObject.SetActive(true);

        interactionText.text = message;
    }

    private void HideHint()
    {
        interactionCanvas.gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        if (mainCamera == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawRay(mainCamera.transform.position, mainCamera.transform.forward * interactRange);
    }
}