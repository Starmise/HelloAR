using UnityEngine;

public class RunnerAnchor : MonoBehaviour
{
    public Transform runnerWorld;
    private bool anchored = false;

    public void AnchorAtTarget()
    {
        if (!anchored)
        {
            runnerWorld.SetParent(null);
            anchored = true;
        }
    }
}

