using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FudaController : MonoBehaviour
{
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Player"){
            GetComponent<Renderer>().material.color = Color.red;


            Vector3 velocity = GameController.instance.getVelocity();
            Vector3 force = new Vector3(velocity.x, 0, velocity.z);
            rb.AddForce(force, ForceMode.Impulse);


        }
    }
}
