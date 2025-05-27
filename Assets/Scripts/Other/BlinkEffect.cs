using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class BlinkEffect : MonoBehaviour
{
    [SerializeField] private Image blinkImage;
    [SerializeField] private float fadeDuration = 0.1f;
    [SerializeField] private float holdDuration = 0.05f;

    public async UniTask PlayBlink()
    {
        // Затухание в черное
        await Fade(0f, 1f, fadeDuration);
        await UniTask.Delay((int)(holdDuration * 1000));
        // Затухание обратно
        await Fade(1f, 0f, fadeDuration);
    }

    public async UniTask PlayFromBlackBlink()
    {
        await UniTask.Delay((int)(holdDuration * 1000));
        // Затухание обратно
        await Fade(1f, 0f, fadeDuration);
    }

    private async UniTask Fade(float from, float to, float duration)
    {
        float time = 0f;
        var color = blinkImage.color;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            color.a = Mathf.Lerp(from, to, t);
            blinkImage.color = color;
            await UniTask.Yield();
        }

        color.a = to;
        blinkImage.color = color;
    }
}