using System.Collections;
using UnityEngine;

public sealed class RagdollDebugger : MonoBehaviour
{
    [SerializeField] HumanController human;
    [SerializeField] Transform hitBone;
    [SerializeField] Vector3 hitForce;
    [SerializeField] float restoreDelay = 0.1f;
    [SerializeField] float restoreTransition = 0.1f;
    IRagdollState state;

    [ContextMenu("Store state")]
    void StoreState()
    {
        state = human.RagdollSystem.CaptureState();
    }
    [ContextMenu("Restore state")]
    void RestoreState()
    {
        StartCoroutine(RestoreStateRoutine(0.5f));
    }
    IEnumerator RestoreStateRoutine(float transition)
    {
        Disable();
        float startTime = Time.time;
        RagdollState startState = (RagdollState)human.RagdollSystem.CaptureState();
        RagdollState.RagdollBlendedState blended = startState.CreateBlended((RagdollState)state);
        var wait = new WaitForFixedUpdate();

        while (Time.time <= startTime + transition)
        {
            blended.Blend = Mathf.Sin((Time.time - startTime) / transition * Mathf.PI * 0.5f);
            human.RagdollSystem.RestoreState(blended);
            yield return wait;
        }

        human.RagdollSystem.RestoreState(state);

        Enable();
    }
    [ContextMenu("Start test")]
    void StartTest()
    {
        StartCoroutine(Start());
    }
    IEnumerator Start()
    {
        StoreState();
        yield return new WaitForSeconds(1f);
        Enable();
        hitBone.GetComponent<Rigidbody>().AddForce(hitForce, ForceMode.VelocityChange);
        yield return new WaitForSeconds(restoreDelay);
        yield return StartCoroutine(RestoreStateRoutine(restoreTransition));
        Disable();
    }
    [ContextMenu("Enable")]
    void Enable()
    {
        human.RagdollSystem.SetEnabled(true);
    }
    [ContextMenu("Disable")]
    void Disable()
    {
        human.RagdollSystem.SetEnabled(false);
    }
}
