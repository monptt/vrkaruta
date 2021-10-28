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

            Vector3 imp = new Vector3(1,0,0);
            rb.AddForce(imp, ForceMode.Impulse);
        }
    }
}
