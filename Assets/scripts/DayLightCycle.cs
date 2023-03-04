using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayLightCycle : MonoBehaviour
{
    public GameObject sun;
    public GameObject moon;

    public float dayLightSpeed;

    public GameObject sceneManage;
    // Start is called before the first frame update
    void Start()
    {
        sceneManage = GameObject.Find("SCENEMANAGER");
        if(sceneManage != null)
        {
            sceneManage.GetComponent<SceneManage>().player = GameObject.Find("Player");
            if (sceneManage.GetComponent<SceneManage>().returned)
            {
                sceneManage.GetComponent<SceneManage>().Return2();
            } 
        }
    }

    // Update is called once per frame
    void Update()
    {
        sun.transform.Rotate(new Vector3(dayLightSpeed * Time.deltaTime, 0, 0));
        moon.transform.eulerAngles = -sun.transform.eulerAngles;
    }
}
