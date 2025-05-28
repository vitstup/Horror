using Cysharp.Threading.Tasks;
using UnityEngine;

public class Entry : MonoBehaviour
{
    [SerializeField] private CustomerScenario customerScenario;
    [SerializeField] private HorrorScenario horrorScenario;

    private void Start()
    {
        Enter();
    }

    private void Enter()
    {
        customerScenario.StartScenario().Forget();
        customerScenario.Completed += () => horrorScenario.StartScenario().Forget();
    }

    private void OnDestroy()
    {
        customerScenario.Completed -= () => horrorScenario.StartScenario().Forget();
    }
}