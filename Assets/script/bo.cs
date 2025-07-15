using UnityEngine;
using UnityEngine.SceneManagement;

public class bo : MonoBehaviour
{
    private Rigidbody2D playerBody;
    public float moveSpeed = 5.0f;
    public float jumpForce = 5.0f;

    public int maxLives = 3;
    private int currentLives;

    // CAMBIO AQUÍ: Hacemos startPosition pública para asignarla en el Inspector
    public Vector3 startPosition;
    // Opcional: Si quieres arrastrar un GameObject vacío como punto de reaparición
    // public Transform spawnPoint; 

    private void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
        currentLives = maxLives;
        Debug.Log("Juego iniciado. Vidas iniciales: " + currentLives);

        // DESCOMENTA LA SIGUIENTE LÍNEA SI QUIERES QUE LA POSICIÓN INICIAL SE GUARDE AUTOMÁTICAMENTE
        // DESDE LA POSICIÓN DEL JUGADOR EN EL EDITOR AL INICIAR EL JUEGO.
        // Si la dejas comentada, DEBES asignar startPosition en el Inspector.
        startPosition = transform.position;
    }

    private void Update()
    {
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        var horizontalSpeed = (horizontalInput * moveSpeed);
        playerBody.velocity = new Vector2(horizontalSpeed, playerBody.velocity.y);

        if (Input.GetButtonDown("Jump") && Mathf.Abs(playerBody.velocity.y) < 0.01f)
        {
            playerBody.velocity = new Vector2(horizontalSpeed, jumpForce);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Obstaculo"))
        {
            Debug.Log("¡Jugador entró en un trigger de obstáculo!");
            TakeDamage();
        }
    }

    void TakeDamage()
    {
        currentLives--;
        Debug.Log("Vida perdida. Vidas restantes: " + currentLives);

        if (currentLives <= 0)
        {
            Debug.Log("¡Todas las vidas perdidas! Reiniciando el juego...");
            RestartGame();
        }
        else
        {
            ResetPlayerPosition();
        }
    }

    void ResetPlayerPosition()
    {
        playerBody.velocity = Vector2.zero;
        transform.position = startPosition; // Usa la posición definida
        Debug.Log("Jugador regresó a la posición inicial.");
    }

    void RestartGame()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}

