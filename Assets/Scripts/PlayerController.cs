using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Vector3 targetPos;
    [SerializeField] private bool isMoving = false;
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public AudioSource audioSource;
    public GameObject explosionVFX;
    public Button endGameButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent <Rigidbody>();
        count = 0;
        SetCountText();
        endGameButton.gameObject.SetActive(false);
        winTextObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
            Debug.DrawRay(ray.origin, ray.direction * 50, Color.yellow);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) 
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    targetPos = hit.point;
                    isMoving = true;
                }
            }
            else if (Vector3.Distance(rb.position, targetPos) < 0.5f)
            {
                isMoving = false;
            }
        }
    }

    private void FixedUpdate() 
    {
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);

        if (isMoving)
        {
            Vector3 direction = targetPos - rb.position;
            direction.Normalize();
            rb.AddForce(direction * speed);
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("PickUp")) 
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
            GameObject.Find("Pickup Sound").GetComponent<AudioSource>().Play();
        }
    }

    void OnMove (InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x; 
        movementY = movementVector.y;
    }

    void SetCountText() 
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 12)
        {
            GameObject.Find("BackGround Music").GetComponent<AudioSource>().mute = true;
            winTextObject.SetActive(true);
            endGameButton.gameObject.SetActive(true);
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
            GameObject.Find("Win Sound").GetComponent<AudioSource>().Play();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            var fx = Instantiate(explosionVFX, transform.position, Quaternion.identity);
            Destroy(fx, 2f);
            Destroy(gameObject);
            winTextObject.gameObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
            endGameButton.gameObject.SetActive(true);
            collision.gameObject.GetComponentInChildren<Animator>().SetFloat("speed_f", 0);
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            GameObject.Find("Wall Hit Sound").GetComponent<AudioSource>().Play();
        }
    }
}
