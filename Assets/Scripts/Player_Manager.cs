using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Manager : MonoBehaviour {

    public GameObject player;
    public LayerMask groundLayer;
    public float speed;
    private Rigidbody rb;
    public float jumpForce;
    public SphereCollider col;
    private Vector3 defaultPosition;
    private Vector3 mousePosition;
    public int playerHealth;
    public Text textHealth;
    private bool canJump;
    Animator animator;
    // Use this for initialization
    void Start () {
		rb = GetComponent<Rigidbody>();
        col = GetComponent<SphereCollider>();
        defaultPosition = player.GetComponent<Transform>().position;
        canJump = false;
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        animator = gameObject.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(mousePosition.x, transform.position.y, transform.position.z), speed * Time.deltaTime);

        //GetComponent<SpriteRenderer>().sprite = Resources.Load("SHEEP_ROTATE_RIGHT_0", typeof(Sprite)) as Sprite;
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        //float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0f);

        rb.AddForce(movement * speed);

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && canJump)
        {
            player.GetComponent<Transform>().rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
            rb.velocity = new Vector3(0f, 0f, 0f);
            rb.angularVelocity = new Vector3(0f, 0f, 0f);
            rb.AddForce(Vector3.up * jumpForce * 2f, ForceMode.Impulse);
            canJump = false;
            animator.runtimeAnimatorController = Resources.Load("SHEEP_ROTATE_RIGHT_0.controller") as RuntimeAnimatorController;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Parry_Box"))
        {
            canJump = true;
        }
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Platform"))
        {
            player.GetComponent<Transform>().position = defaultPosition;
            player.GetComponent<Rigidbody>().position = defaultPosition;
            player.GetComponent<Transform>().rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
            rb.velocity = new Vector3(0f, 0f, 0f);
            rb.angularVelocity = new Vector3(0f, 0f, 0f);
            canJump = false;
            --playerHealth;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private bool Landed() {
        //return Physics.CheckSphere(col.bounds.center, new Vector3(col.bounds.center.x,
        //    col.bounds.min.y, col.bounds.center.z), col.radius * .99f, groundLayer);
        return Physics.CheckSphere(this.GetComponent<Rigidbody>().position, 0.5f, groundLayer);
    }

    void OnBecameInvisible()
    {
        Debug.Log(defaultPosition);
        player.GetComponent<Transform>().position = defaultPosition;
        player.GetComponent<Rigidbody>().position = defaultPosition;
        player.GetComponent<Transform>().rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        rb.velocity = new Vector3(0f, 0f, 0f);
        rb.angularVelocity = new Vector3(0f, 0f, 0f);
        --playerHealth;
    }
}
