using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Animations.Rigging;

[RequireComponent(typeof(CarryItemHandler))]
public class CarryingAnimCont : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField] private int layer = 1;

    [SerializeField] private string TakeAnim;
    [SerializeField] private string DropAnim;

    [SerializeField] private Rig rig;
    [SerializeField] private Transform ikTarget;
    [SerializeField] private float initialWeight = 0.3f;

    private CarryItemHandler handler;

    private void Awake()
    {
        handler = GetComponent<CarryItemHandler>();

        handler.OnPickableTakeStarted += ItemWasTaken;
        handler.OnPickableDropStarted += ItemWasDroped;

        animator.SetLayerWeight(layer, 0f);
    }

    private async void ItemWasTaken(Pickable pickable)
    {
        rig.weight = initialWeight;

        ikTarget.transform.position = pickable.transform.position;

        ikTarget.transform.localRotation = Quaternion.Euler(pickable.localRotationOffsetWhileTargeting);

        float duration = handler.timeToPick;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            rig.weight = Mathf.Lerp(initialWeight, 1f, t);
            await UniTask.Yield(); 
        }

        animator.SetLayerWeight(layer, 1f);

        while (elapsed > 0)
        {
            elapsed -= Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            rig.weight = Mathf.Lerp(0, 1f, t);
            await UniTask.Yield();
        }
    }

    private async void ItemWasDroped(Pickable pickable, Transform to)
    {
        //animator.Play(DropAnim, layer: layer, normalizedTime: 0f);

        //rig.weight = initialWeight;

        ikTarget.transform.position = to.position;

        ikTarget.transform.localRotation = Quaternion.Euler(pickable.localRotationOffsetWhileTargeting);

        float duration = handler.timeToPick;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            rig.weight = Mathf.Lerp(initialWeight, 1f, t);
            await UniTask.Yield();
        }

        rig.weight = 0f;
        animator.SetLayerWeight(layer, 0f);
    }

    private void OnDestroy()
    {
        handler.OnPickableTakeStarted -= ItemWasTaken;
        handler.OnPickableDropStarted -= ItemWasDroped;
    }
}