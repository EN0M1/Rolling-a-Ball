using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public AudioSource audioSource;
    public GameObject explosionVFX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent <Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
    }

    private void FixedUpdate() 
    {
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
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
            collision.gameObject.GetComponentInChildren<Animator>().SetFloat("speed_f", 0);
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            GameObject.Find("Wall Hit Sound").GetComponent<AudioSource>().Play();
        }
    }
}
