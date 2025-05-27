using Cysharp.Threading.Tasks;
using PixelCrushers.DialogueSystem;
using UnityEngine;

public class FacialAnimatorController : MonoBehaviour
{
    [SerializeField] private FacialAnimator SpeakingAnim;

    public void OnSubtitle(Subtitle subtitle)
    {
        if (subtitle.speakerInfo.IsNPC)
            SpeakingAnim.Play(2f).Forget();
    }
}