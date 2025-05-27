using Cysharp.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(SkinnedMeshRenderer))]
public abstract class FacialAnimator : MonoBehaviour
{
    protected SkinnedMeshRenderer faceMesh;

    private void Awake()
    {
        faceMesh = GetComponent<SkinnedMeshRenderer>();
    }

    public abstract UniTask Play(float duration);

    public abstract void Stop();
}