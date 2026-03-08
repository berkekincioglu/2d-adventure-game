using UnityEngine;
using System.Collections;


public class DamageZone : MonoBehaviour
{
    [SerializeField] private int damagePerTick = 1;
    [SerializeField] private float tickInterval = 1.0f;

    private Coroutine damageCoroutine;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            damageCoroutine = StartCoroutine(DamageTick(player));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null && damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
            damageCoroutine = null;
        }
    }

    private IEnumerator DamageTick(PlayerController player)
    {
        while (true)
        {
            player.ChangeHealth(-damagePerTick);
            yield return new WaitForSeconds(tickInterval);
        }
    }

    // [SerializeField] private int damageAmount = 1;
    // void OnTriggerStay2D(Collider2D collision)
    // {
    //     PlayerController player = collision.GetComponent<PlayerController>();
    //     if (player != null)
    //     {
    //         player.ChangeHealth(-damageAmount);
    //     }

    // }
}
