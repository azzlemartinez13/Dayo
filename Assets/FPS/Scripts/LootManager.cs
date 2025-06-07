using UnityEngine;
using UnityEngine.UI;

public class LootManager : MonoBehaviour
{
    public static LootManager Instance;

    public GameObject lootDropPanel; // Assign in inspector
    public GameObject healthLootPrefab;
    public GameObject ammoLootPrefab;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void DropLoot(Vector3 worldPosition)
    {
        int random = Random.Range(0, 2);
        GameObject loot = Instantiate(
            random == 0 ? healthLootPrefab : ammoLootPrefab,
            lootDropPanel.transform
        );

        // Optionally position near target location in screen space
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPosition);
        loot.GetComponent<RectTransform>().position = screenPos;
    }
}