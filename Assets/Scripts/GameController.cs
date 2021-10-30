using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    System.Random rand = new System.Random();
    static public GameController instance;

    public GameObject textobj;
 	
    OVRInput.Controller LeftCon;
    OVRInput.Controller RightCon;

    [SerializeField] private Transform TrackingSpace;
    private Vector3 _lastPos;
    private Vector3 _velocity;


    public GameObject fuda_prefub;
    public Material[] fuda_material; 
    public GameObject jijin;
    public GameObject tekijin;



    private void initGame(){
        float jijin_center_x = jijin.transform.position.x;
        float jijin_center_y = jijin.transform.position.y;
        float jijin_center_z = jijin.transform.position.z;
        float fuda_w = 0.052f;
        float fuda_h = 0.073f;

        for(int dan=1; dan>=-1; dan--){
            for(int i=-7; i<8; i++){
                float x = jijin_center_x + fuda_w * i;
                float y = 0.5f;
                float z = jijin_center_z + (fuda_h + 0.01f)*dan;

                GameObject fuda = Instantiate(fuda_prefub, new Vector3(x,y,z), Quaternion.Euler(0, 180, 0));
                fuda.GetComponent<Renderer>().material = fuda_material[rand.Next(100)];
            }
        }


        float tekijin_center_x = tekijin.transform.position.x;
        float tekijin_center_y = tekijin.transform.position.y;
        float tekijin_center_z = tekijin.transform.position.z;

        for(int dan=1; dan>=-1; dan--){
            for(int i=-7; i<8; i++){
                float x = tekijin_center_x + fuda_w * i;
                float y = 0.5f;
                float z = tekijin_center_z + (fuda_h + 0.01f)*dan;

                GameObject fuda = Instantiate(fuda_prefub, new Vector3(x,y,z), Quaternion.Euler(0, 0, 0));
                fuda.GetComponent<Renderer>().material = fuda_material[rand.Next(100)];
            }
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        LeftCon = OVRInput.Controller.LTouch;
        RightCon = OVRInput.Controller.RTouch;

        _lastPos = OVRInput.GetLocalControllerPosition(RightCon);
        _velocity = Vector3.zero;

        initGame();
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
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
    
    
        // 右手トリガーを引いたら振動を開始
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            OVRInput.SetControllerVibration(0f, 1f, OVRInput.Controller.RTouch);
            initGame();
        }
        // 右手トリガーを離したら振動を停止
        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            OVRInput.SetControllerVibration(0f, 0f, OVRInput.Controller.RTouch);
        }
    }

    public Vector3 getVelocity(){
        return _velocity;
    }
}
