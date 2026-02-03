using UnityEngine;
using System.Collections;
[CreateAssetMenu(menuName = "AI/Combat/Counter")]
public class EnemyCombatCounter : AICombatSubState
{
    public float recoveryTime = 0.6f;

    public override void OnEnter()
    {
        brain.agent.isStopped = true;
        brain.characterModel.animator.SetTrigger("Attack0");

        brain.RunCoroutine(CounterRoutine());
    }

    private IEnumerator CounterRoutine()
    {
        yield return new WaitForSeconds(recoveryTime);
        Emit(SubstateSignal.CounterTimeout);
    }

    public override void Tick() { }
    public override void OnExit()
    {
        brain.agent.isStopped = false;
    }
}


