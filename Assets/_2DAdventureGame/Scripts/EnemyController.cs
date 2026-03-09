using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float speed = 2.0f;
    [SerializeField] private bool vertical;
    [SerializeField] private float changeTime = 3.0f;

    [SerializeField] private int attackDamage = 1;

    private Rigidbody2D rb;
    private float timer;
    private int direction = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timer = changeTime;
        HandleRandomizeVertical();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            direction = -direction;
            timer = changeTime;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 position = rb.position;
        if (vertical)
        {
            position.y += speed * Time.fixedDeltaTime * direction;
        }
        else
        {
            position.x += speed * Time.fixedDeltaTime * direction;
        }
        rb.MovePosition(position);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            player.ChangeHealth(-attackDamage);
        }
    }

    private void HandleRandomizeVertical()
    {
        vertical = Random.value > 0.5f;

    }
}
