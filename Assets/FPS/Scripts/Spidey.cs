using UnityEngine.AI;

public class Spidey : PoolableObject
{
    public SpideyMovement Movement;
    public NavMeshAgent Agent;
    public int Health = 100;

    public override void OnDisable()
    {
        base.OnDisable();

        Agent.enabled = false;
    }
}
