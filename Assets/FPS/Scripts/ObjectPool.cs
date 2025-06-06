using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IPoolable
{
    void OnGetFromPool();
    void OnReturnToPool();
}

public class ObjectPool<T> where T : Component
{
    private readonly T prefab;
    private readonly Transform parent;

    private readonly Queue<T> pool = new Queue<T>();
    private readonly HashSet<T> inUse = new HashSet<T>();

    public ObjectPool(T prefab, int initialSize, Transform parent = null)
    {
        this.prefab = prefab;
        this.parent = parent;

        for (int i = 0; i < initialSize; i++)
        {
            T obj = Object.Instantiate(prefab, parent);
            obj.gameObject.SetActive(true);
            pool.Enqueue(obj);
        }
    }

    public T Get()
    {
        T obj;
        if (pool.Count > 0)
        {
            obj = pool.Dequeue();
            obj.gameObject.SetActive(true);
        }
        else
        {
            obj = Object.Instantiate(prefab, parent);
        }

        if (obj is IPoolable poolable)
        {
            poolable.OnGetFromPool();
        }

        inUse.Add(obj);
        return obj;
    }

    public void Return(T obj)
    {
        if (obj is IPoolable poolable)
        {
            poolable.OnReturnToPool();
        }

        obj.gameObject.SetActive(false);
        pool.Enqueue(obj);
        inUse.Remove(obj);
    }

    public void ReturnAll()
    {
        foreach (T obj in inUse.ToList())
        {
            Return(obj);
        }
    }

    public void Clear()
    {
        foreach (T obj in pool)
        {
            Object.Destroy(obj.gameObject);
        }
        pool.Clear();

        foreach (T obj in inUse)
        {
            Object.Destroy(obj.gameObject);
        }
        inUse.Clear();
    }
}
