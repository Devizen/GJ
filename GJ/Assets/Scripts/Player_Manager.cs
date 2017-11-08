using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Manager : MonoBehaviour {

    public GameObject player;
    public LayerMask groundLayer;
    public float speed;
    private Rigidbody rb;
    public float jumpForce;
    public SphereCollider col;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
        col = GetComponent<SphereCollider>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        //float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0f);

        rb.AddForce(movement * speed);

        if (Landed() && Input.GetKeyDown(KeyCode.Space)) {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private bool Landed() {
        //return Physics.CheckSphere(col.bounds.center, new Vector3(col.bounds.center.x,
        //    col.bounds.min.y, col.bounds.center.z), col.radius * .99f, groundLayer);
        return Physics.CheckSphere(this.GetComponent<Rigidbody>().position, 0.5f, groundLayer);
    }
}
