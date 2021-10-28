using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FudaController : MonoBehaviour
{
    Rigidbody rb;
    public GameObject textobj;
 	
    OVRInput.Controller LeftCon;
    OVRInput.Controller RightCon;

    [SerializeField] private Transform TrackingSpace;
    private Vector3 _lastPos;
    private Vector3 _velocity;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        LeftCon = OVRInput.Controller.LTouch;
        RightCon = OVRInput.Controller.RTouch;

        _lastPos = OVRInput.GetLocalControllerPosition(RightCon);
        _velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        // get Controller position and calc velocity
        Vector3 localPos = OVRInput.GetLocalControllerPosition(RightCon);
        Vector3 pos = TrackingSpace.TransformPoint(localPos);
        _velocity = (pos - _lastPos) / Time.deltaTime;
        _lastPos = pos;
        textobj.GetComponent<TextMesh>().text = $"X:{_velocity.x}, Y:{_velocity.y}, Z:{_velocity.z}";
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Player"){
            GetComponent<Renderer>().material.color = Color.red;

            Vector3 force = new Vector3(_velocity.x, 0, _velocity.z);
            rb.AddForce(force, ForceMode.Impulse);


        }
    }
}
