using UnityEngine;

/// <summary>
/// Este script es necesario para que el juego se ejecute solo cuando el image target
/// es detectado por la cámara, aprovechando los eventos del  script DefaultObserverEventHandler
/// </summary>
public class RunnerTargetHandler : MonoBehaviour
{
    public RunnerGeneratorAr runnerGeneratorAR;

    public void EnableRunner()
    {
        runnerGeneratorAR.enabled = true;
    }

    public void DisableRunner()
    {
        runnerGeneratorAR.enabled = false;

        foreach (Transform child in runnerGeneratorAR.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
