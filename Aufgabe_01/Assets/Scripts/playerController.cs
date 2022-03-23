using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{

    public float speed = 10f;
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
        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(movement * speed);

    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Pick Up")) {
            other.gameObject.SetActive(false);
        }
    }
}
