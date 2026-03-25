using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputAction MoveAction;
    public InputAction LaunchAction;
    public InputAction TalkAction;

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

    private Animator animator;
    private Vector2 moveDirection = new Vector2(1, 0);

    [SerializeField] private GameObject projectilePrefab;
    private NonPlayerCharacter lastNonPlayerCharacter;

    private AudioSource audioSource;
    [SerializeField] private AudioClip hitClip;

    void Start()
    {
        MoveAction.Enable();
        LaunchAction.Enable();
        TalkAction.Enable();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        move = MoveAction.ReadValue<Vector2>();

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            moveDirection.Set(move.x, move.y);
            moveDirection.Normalize();
        }

        animator.SetFloat("Look X", moveDirection.x);
        animator.SetFloat("Look Y", moveDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if (LaunchAction.WasPressedThisFrame())
        {
            Launch();
        }

        RaycastHit2D hit = Physics2D.Raycast(rb.position + Vector2.up * 0.2f, moveDirection, 1.5f, LayerMask.GetMask("NPC"));
        if (hit.collider != null)
        {

            NonPlayerCharacter npc = hit.collider.GetComponent<NonPlayerCharacter>();
            npc.dialogueBubble.SetActive(true);
            lastNonPlayerCharacter = npc;
            FindFriend();
        }
        else
        {
            if (lastNonPlayerCharacter != null)
            {
                lastNonPlayerCharacter.dialogueBubble.SetActive(false);
                lastNonPlayerCharacter = null;
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = rb.position + speed * Time.fixedDeltaTime * move;
        rb.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHandler.instance.UpdateHealthBar((float)currentHealth / maxHealth);
        if (amount < 0)
        {
            PlaySound(hitClip);
        }
    }

    public void TriggerHitAnimation()
    {
        animator.SetTrigger("Hit");
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    private void Launch()
    {
        GameObject projectile = Instantiate(projectilePrefab, rb.position + Vector2.up * 0.5f, Quaternion.identity);
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        projectileScript.Launch(moveDirection, 300.0f);
        animator.SetTrigger("Launch");
    }

    private void FindFriend()
    {
        if (TalkAction.WasPressedThisFrame())
        {
            UIHandler.instance.DisplayDialogue();
        }
    }

}