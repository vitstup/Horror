using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public class SpeakingAnimator : FacialAnimator
{
    [SerializeField] private int mouthOpenIndex = 0;

    [SerializeField] private Vector2 openRange = new Vector2(10f, 60f); // от и до открытия
    [SerializeField] private Vector2 intervalRange = new Vector2(0.05f, 0.15f); // частота смены цели
    [SerializeField] private Vector2 changeSpeedRange = new Vector2(80f, 160f); // скорость изменения blendshape

    private CancellationTokenSource talkingToken;

    public override async UniTask Play(float duration)
    {
        Stop();

        talkingToken = new CancellationTokenSource();
        var token = talkingToken.Token;

        float elapsed = 0f;
        float currentWeight = faceMesh.GetBlendShapeWeight(mouthOpenIndex);
        float targetWeight = Random.Range(openRange.x, openRange.y);
        float changeSpeed = Random.Range(changeSpeedRange.x, changeSpeedRange.y);
        float timeToNextChange = Random.Range(intervalRange.x, intervalRange.y);

        while (elapsed < duration && !token.IsCancellationRequested)
        {
            currentWeight = Mathf.MoveTowards(currentWeight, targetWeight, changeSpeed * Time.deltaTime);
            faceMesh.SetBlendShapeWeight(mouthOpenIndex, currentWeight);

            timeToNextChange -= Time.deltaTime;

            if (timeToNextChange <= 0f)
            {
                targetWeight = Random.Range(openRange.x, openRange.y);
                changeSpeed = Random.Range(changeSpeedRange.x, changeSpeedRange.y);
                timeToNextChange = Random.Range(intervalRange.x, intervalRange.y);
            }

            elapsed += Time.deltaTime;
            await UniTask.Yield(PlayerLoopTiming.Update, token);
        }

        faceMesh.SetBlendShapeWeight(mouthOpenIndex, 0f); // закрыть рот в конце
    }

    public override void Stop()
    {
        if (talkingToken != null)
        {
            talkingToken.Cancel();
            talkingToken.Dispose();
            talkingToken = null;
        }

        faceMesh.SetBlendShapeWeight(mouthOpenIndex, 0f);
    }
}