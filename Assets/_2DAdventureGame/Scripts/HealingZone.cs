using System.Collections;
using UnityEngine;

public class HealingZone : MonoBehaviour
{
    [SerializeField] private int healPerTick = 1;
    [SerializeField] private float tickInterval = 1.0f;

    private Coroutine healCoroutine;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            healCoroutine = StartCoroutine(HealTick(player));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null && healCoroutine != null)
        {
            StopCoroutine(healCoroutine);
            healCoroutine = null;
        }
    }

    private IEnumerator HealTick(PlayerController player)
    {
        while (true)
        {
            player.ChangeHealth(healPerTick);
            yield return new WaitForSeconds(tickInterval);
        }
    }
}
