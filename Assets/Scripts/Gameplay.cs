using Fsi.Gameplay;
using UnityEngine;
using Environment = Environments.Environment;

public class Gameplay : MbSingleton<Gameplay>
{
    private void OnEnable()
    {
        Environment.Ready += OnEnvironmentReady;
    }

    private void OnDisable()
    {
        Environment.Ready -= OnEnvironmentReady;
    }

    private void OnEnvironmentReady(Environment env)
    {
        Debug.Log($"Gameplay: Environment ready ({env.gameObject.name})");
    }
}
