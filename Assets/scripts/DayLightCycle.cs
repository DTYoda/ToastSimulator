using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayLightCycle : MonoBehaviour
{
    public GameObject sun;
    public GameObject moon;

    public float dayLightSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sun.transform.Rotate(new Vector3(dayLightSpeed * Time.deltaTime, 0, 0));
        moon.transform.eulerAngles = -sun.transform.eulerAngles;
    }
}
