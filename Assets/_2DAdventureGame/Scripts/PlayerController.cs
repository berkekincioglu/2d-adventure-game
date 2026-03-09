using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputAction MoveAction;

    [SerializeField] private float speed = 3.0f;
    [SerializeField] private int maxHealth = 5;
    public int MaxHealth => maxHealth;

    private int currentHealth;
    public int CurrentHealth
    {
        get => currentHealth;
    }

    private Rigidbody2D rb;
    private Vector2 move;

    void Start()
    {
        MoveAction.Enable();
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        move = MoveAction.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Vector2 position = rb.position + speed * Time.fixedDeltaTime * move;
        rb.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }

}