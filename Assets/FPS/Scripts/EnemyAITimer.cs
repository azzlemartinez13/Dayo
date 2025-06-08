using UnityEngine;

public class EnemyAITimer : MonoBehaviour
{
    public float TimeToReset = 20f; // default 20 seconds before spider self-despawns
    private float timeSinceSpawn = 0f;

    private Spider _spider;

    private void Awake()
    {
        _spider = GetComponent<Spider>();
    }

    private void OnEnable()
    {
        timeSinceSpawn = 0f;
    }

    private void Update()
    {
        timeSinceSpawn += Time.deltaTime;

        if (timeSinceSpawn >= TimeToReset)
        {
            HandleTimeout();
        }
    }

    private void HandleTimeout()
    {
        // Optional: Play effects/sounds here
        Debug.Log($"Spider {gameObject.name} timed out and is being despawned.");

        //_spider.TakeDamage((int)_spider.currentHP + 1); // ? Missing damageDealer

        _spider.TakeDamage((int)_spider.currentHP + 1, gameObject); // Pass spider itself as damage dealer
    }
}
