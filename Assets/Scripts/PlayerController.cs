using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 800f;
    public int health = 5;
    public Text scoreText;
    private int score = 0;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints.FreezePositionY;
            rb.linearDamping = 1.2f;
        }
        SetScoreText();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed * Time.deltaTime);
    }

    void Update()
    {
        if (health == 0)
        {
            Debug.Log("Game Over!");
            health = 5;
            score = 0;
            SetScoreText();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void SetScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            score++;
            SetScoreText();
            other.gameObject.SetActive(false);
        }
        else if (other.CompareTag("Trap"))
        {
            health--;
            Debug.Log("Health: " + health);
        }
        else if (other.CompareTag("Goal"))
        {
            Debug.Log("You win!");
        }
    }
}
