using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputAction MoveAction;
    private Rigidbody2D rb;
    private Vector2 move;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 10;
        MoveAction.Enable();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        move = MoveAction.ReadValue<Vector2>();
        Debug.Log(move);
    }

    private void FixedUpdate()
    {
        Vector2 position = (Vector2)rb.position + 3.0f * Time.fixedDeltaTime * move;
        rb.MovePosition(position);
    }

}