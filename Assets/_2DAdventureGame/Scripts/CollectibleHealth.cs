using Unity.VisualScripting;
using UnityEngine;

public class CollectibleHealth : MonoBehaviour
{
    [SerializeField] private int healthAmount = 1;
    public AudioClip collectedClip;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null && player.CurrentHealth < player.MaxHealth)
        {
            player.ChangeHealth(healthAmount);
            player.PlaySound(collectedClip);
            Destroy(gameObject);
        }
    }

}

