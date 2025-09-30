using UnityEngine;

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
