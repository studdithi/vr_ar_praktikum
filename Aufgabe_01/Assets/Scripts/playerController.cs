using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{

    public float speed = 10f;
    public float gravity = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 1f, moveVertical);

        Rigidbody rb = GetComponent<Rigidbody>();

        rb.AddForce(movement * speed);
        if (Input.GetKeyDown(KeyCode.Space)) {
            rb.AddForce(Vector3.up * 10f, ForceMode.Impulse);
        }
        rb.AddForce(new Vector3(0, -gravity, 0));


    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Pick Up")) {
            other.gameObject.SetActive(false);
        }
    }
}
