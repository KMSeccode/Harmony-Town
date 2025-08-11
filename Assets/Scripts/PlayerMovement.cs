using UnityEngine;
using Nakama;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    // Nakama
    private ISocket socket;
    private string matchId;

    public void InitNakama(ISocket socket, string matchId)
    {
        this.socket = socket;
        this.matchId = matchId;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();

        // Envia posição para outros jogadores
        if (socket != null && !string.IsNullOrEmpty(matchId))
        {
            var pos = new Vector2(transform.position.x, transform.position.y);
            var payload = JsonUtility.ToJson(pos);
            socket.SendMatchStateAsync(matchId, 1, payload); 
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }
}
