using UnityEngine;

public class PoolableObject : MonoBehaviour
{
    public PoolObject Parent;

    public virtual void OnDisable()
    {
        Parent.ReturnObjectToPool(this);
    }
}