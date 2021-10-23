using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class SimpleCarController : MonoBehaviour
{
    public WheelCollider frontLeftWheel;
    public WheelCollider frontRightWheel;
    public WheelCollider rearLeftWheel;
    public WheelCollider rearRightWheel;
    public float maxMotorTorque;
    public float maxSteeringAngle;
    public int Gear;
    public Rigidbody rb;
    public int Maxspeed;
    public int speed;
    public float rpm;
    public float resitance;
    public readonly float RevLimit = 7500;
    public float gearratio;
    public bool stopAccelFlag;





    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0)
        {
            return;
        }

        Transform visualWheel = collider.transform.GetChild(0);

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }

    public void FixedUpdate()
    {
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");
        Debug.Log(rb.velocity.magnitude * 3.6);


        speed = (int)(rb.velocity.magnitude * 3.6);
        if (Input.GetKey(KeyCode.W))
        {
            rpm += RevLimit * Time.deltaTime;
        }
        else
        {
            rpm -= RevLimit * Time.deltaTime;
        }

        if (rpm == 7500)
        {
            
        }
        rpm = Mathf.Clamp(rpm, 1877, 7500);

        rb.AddForce(transform.forward * rpm * gearratio);

        //rb.velocity = transform.forward * (rpm * gearratio);


        frontLeftWheel.steerAngle = steering;
        frontRightWheel.steerAngle = steering;

        
        if (Input.GetKey(KeyCode.S))
        {
            rearLeftWheel.brakeTorque = 100;
            rearRightWheel.brakeTorque = 100;

        }
        else
        {
            rearLeftWheel.brakeTorque = 0;
            rearRightWheel.brakeTorque = 0;
        }


        ApplyLocalPositionToVisuals(rearLeftWheel);
        ApplyLocalPositionToVisuals(rearRightWheel);


        
        if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            Gear++;
            if (speed >= 10)
            {
                rpm = speed / gearratio;
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            Gear--;
            if (speed >= 10)
            {
                rpm = speed / gearratio;
            }
        }
        Gear = Mathf.Clamp(Gear,-1, 6);

        if (Gear == -1)
        {
            gearratio = -0.0124f;//93kph
        }
        if (Gear == 0)
        {
            gearratio = 0.0f;//0kph
        }
        if (Gear == 1) 
        {
            gearratio = 0.0158f;//119kph
        }
        if (Gear == 2)
        {
            gearratio = 0.0208f;//156kph
        }
        if (Gear == 3)
        {
            gearratio = 0.0253f;//190kph
        }
        if (Gear == 4)
        {
            gearratio = 0.0301f;//226kph
        }
        if (Gear == 5)
        {
            gearratio = 0.0346f;//260kph
        }
        if (Gear == 6)
        {
            gearratio = 0.0392f;//294kph
        }



        //rearLeftWheel.motorTorque = rpm * gearratio;
        //rearRightWheel.motorTorque = rpm * gearratio;

        
    }
}


//エンジン回転数の概念を追加　それぞれのギアと対応させる　エンジン回転数に応じて音を変化させる　
//"Maxspeed"という変数を作ってそれを「もしギアが1ならMaxspeedを117にする」見たな感じでやる
//Gear の値によって抵抗の値が変わる感じの実装にするか
//Maxspeedの代わりに抵抗（英語だとresistance）を
//を変えるような感じがいいかな
