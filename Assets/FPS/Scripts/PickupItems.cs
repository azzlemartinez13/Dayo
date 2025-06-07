//using UnityEngine;

//public class PickupItem : MonoBehaviour
//{
//    public enum ItemType { HealthPotion, Ammo }
//    public ItemType itemType;

//    public int healthRestoreAmount = 20;
//    public int ammoAmount = 15;

//    private AudioManager audioManager;

//    private void Awake()
//    {
//        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
//    }

//    private void OnTriggerEnter2D(Collider2D other)
//    {
//        if (!other.CompareTag("Base")) return;

    
//        Base playerBase = FindObjectOfType<Base>();

//        GunInventory inventory = other.GetComponent<GunInventory>();
//        GunScript gun = inventory != null && inventory.currentGun != null
//            ? inventory.currentGun.GetComponent<GunScript>()
//            : null;

//        if (itemType == ItemType.HealthPotion)
//        {
//            if (playerBase != null)
//            {
//                playerBase.RestoreHealth(healthRestoreAmount);
//            }
//            else if (playerBase != null)
//            {
//                playerBase.Health_Current = Mathf.Min(playerBase.Health_Current + healthRestoreAmount, playerBase.Health_Start);
//                playerBase.OnHealthChanged?.Invoke();
//            }
//        }
//        else if (itemType == ItemType.Ammo && gun != null)
//        {
//            gun.RestoreAmmo(ammoAmount);
//        }

//        if (audioManager != null)
//            audioManager.PlaySFX(audioManager.ItemPickUpClip);

//        Destroy(gameObject);
//    }
//}
