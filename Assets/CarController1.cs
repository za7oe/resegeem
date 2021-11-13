using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Vehicles.Car;

public class CarController1 : MonoBehaviour
{
    public WheelCollider frontLeftWheel;
    public WheelCollider frontRightWheel;
    public WheelCollider rearLeftWheel;
    public WheelCollider rearRightWheel;
    public int Gear;
    public Rigidbody rb;
    public int speed;
    public float rpm;
    public float resitance;
    public readonly float RevLimit = 7500;
    public float gearratio;
    public bool stopAccelFlag;
    public float minRPM;
    public float maxRPM;
    public float rpm_rate;
    public float[] gearratios;
    private CarController m_Car;

    // Start is called before the first frame update
    void Start()
    {
        Gear = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (rpm > 7500) 
            {
                rpm -= 10000 * Time.deltaTime;
            }
            else
            {
                rpm += 15619 * Time.deltaTime;
            }
        }
        else
        {
            if(rpm > 1877)
            {
                rpm -= 1 * Time.deltaTime*rpm;
            }
        }
        rpm = Mathf.Clamp(rpm, 1877, 7700);

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
        Gear = Mathf.Clamp(Gear, 0, 7);
        rpm_rate = (rpm - minRPM) / (maxRPM - minRPM) * 100;
    }
    private void Awake()
    {
        //get the car contoroller 
        m_Car = GetComponent<CarController>();
    }
    public void FixedUpdate()
    {
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        float v = CrossPlatformInputManager.GetAxis("Vertical");
        float handbrake = CrossPlatformInputManager.GetAxis("Jump");
        m_Car.Move(h, v,v, handbrake, gearratios[Gear]);
    }

}

    
        //rpm = Mathf.Clamp(rpm, 1877, 7500);
    
