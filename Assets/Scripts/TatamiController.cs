using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TatamiController : MonoBehaviour
{
    public GameObject textobj;

    // Start is called before the first frame update
    void Start()
    {
        textobj.GetComponent<TextMesh>().text = "start";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Player"){
            OVRInput.SetControllerVibration(0f, 1f, OVRInput.Controller.RTouch);
            OVRInput.SetControllerVibration(0f, 1f, OVRInput.Controller.LTouch);
            textobj.GetComponent<TextMesh>().text = "atari";
        }
    }
}
