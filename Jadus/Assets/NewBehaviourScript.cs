using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NewBehaviourScript : MonoBehaviour
{

    public float speed = 11f; // Geschwindigkeit des Charakters
    public Rigidbody2D rb; // Rigidbody-Komponente des Charakters
    public float jump = 10;
    private bool isgroundet = false;

    private Animator anim;
    private Quaternion originalRotation;

    private gemManager g;

    public GameObject panel;


    public GameObject kamera;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody-Komponente des Charakters finden
        rb.gravityScale = 0.5f;
        anim = GetComponent<Animator>();
        originalRotation = transform.rotation;

        g = GameObject.FindGameObjectWithTag("text").GetComponent<gemManager>();

    }

    void Update()
    {

        // Horizontale Bewegung erfassen
        float moveHorizontal = Input.GetAxisRaw("Horizontal");

        if(moveHorizontal != 0)
        {
            anim.SetBool("IsRunning", true);
        }
        else
        {
            anim.SetBool("IsRunning", false);
        }
        if(moveHorizontal < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0) * originalRotation;
        }
        else if (moveHorizontal > 0)
        {
            transform.rotation = originalRotation;
        }

        // Bewegung berechnen
        Vector2 movement = new Vector2(moveHorizontal * speed, rb.velocity.y);
        rb.velocity = movement;

        if (Input.GetKeyDown(KeyCode.Space) && isgroundet)
        {
            rb.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
            isgroundet = false;
        }

        kamera.transform.position = new Vector3(transform.position.x, 0, -10);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("ground"))
        {
            isgroundet = true;
        }
        if (collision.collider.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            panel.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "dia")
        {
            g.AddMoney();
              Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Finish")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

    }

}
