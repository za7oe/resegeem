using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text gearText;
    public Text speedText;
    public Text rpmText;
    public CarController controller;
    public GameObject[] rpm_light;
    public float light_on_time;
    public float light_off_time;
    public float time;


    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < rpm_light.Length; i++) 
        {
            rpm_light[i].GetComponent<Image>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        gearText.text = controller.Gear.ToString();
        switch(controller .Gear)
        {
            case 0:
                gearText.text = "R";
                break;
            case 1:
                gearText.text = "N";
                break;
            case 2:
                gearText.text = "1";
                break;
            case 3:
                gearText.text = "2";
                break;
            case 4:
                gearText.text = "3";
                break;
            case 5:
                gearText.text = "4";
                break;
            case 6:
                gearText.text = "5";
                break;
            case 7:
                gearText.text = "6";
                break;
        }
        //gearText.text = controller.Gear switch
        //{
        //    0 => "N",
        //    -1 => "R",
        //    _ => controller.Gear.ToString()
        ///};
       speedText.text = controller.speed.ToString();
        rpmText.text = controller.rpm.ToString("f0");

        if (controller.rpm_rate >95)
        {
            time += Time.deltaTime;
            if (light_on_time > time)
            {
                for (int i = 0; i < rpm_light.Length; i++)
                {
                    rpm_light[i].GetComponent<Image>().enabled = true;
                }
            }
            else
            {
                for (int i = 0; i < rpm_light.Length; i++)
                {
                    rpm_light[i].GetComponent<Image>().enabled = false;
                }
            }
            if (time > light_on_time + light_off_time)
            {
                time = 0;
            }
        }
        else
        {
            time = 0;
            for (int i = 0; i < rpm_light.Length; i++)
            {
                if (controller.rpm_rate >= float.Parse(rpm_light[i].name))
                {
                    rpm_light[i].GetComponent<Image>().enabled = true;
                }
                else
                {
                    rpm_light[i].GetComponent<Image>().enabled = false;
                }

            }
        }


    }
}

