using System;
using Cysharp.Threading.Tasks;

public interface IScenario 
{
    public event Action Completed;

    public UniTask StartScenario();
}