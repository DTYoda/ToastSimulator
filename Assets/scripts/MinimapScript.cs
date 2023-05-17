using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapScript : MonoBehaviour
{
    Transform player;
    public int maxSize;
    public int minSize;

    public int maxFullSize;
    public int minFullSize;

    public GameObject minimapImage;
    public GameObject fullMinimapImage;

    public RenderTexture minimapTexture;
    public RenderTexture fullMinimapTexture;

    bool isFullScreen;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        if(PlayerPrefs.GetFloat("MinimapSize") == 0)
        {
            PlayerPrefs.SetFloat("MinimapSize", 20);
        }
        if (PlayerPrefs.GetFloat("FullMinimapSize") == 0)
        {
            PlayerPrefs.SetFloat("FullMinimapSize", 80);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            if(isFullScreen)
            {
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            isFullScreen = !isFullScreen;
        }

        FullScreen();

    }

    private void FullScreen()
    {
        if(isFullScreen)
        {
            minimapImage.SetActive(false);
            fullMinimapImage.SetActive(true);
            this.GetComponent<Camera>().orthographicSize = PlayerPrefs.GetFloat("FullMinimapSize");
            this.GetComponent<Camera>().targetTexture = fullMinimapTexture;
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if(Input.GetKey(KeyCode.Mouse0))
            {
                this.transform.position += new Vector3(-Input.GetAxis("Mouse X") * (PlayerPrefs.GetFloat("FullMinimapSize")/20), 0, -Input.GetAxis("Mouse Y") * (PlayerPrefs.GetFloat("FullMinimapSize")/20));
            }

            if (Input.GetAxis("Mouse ScrollWheel") > 0f && PlayerPrefs.GetFloat("FullMinimapSize") > minFullSize) // forward
            {
                PlayerPrefs.SetFloat("FullMinimapSize", PlayerPrefs.GetFloat("FullMinimapSize") - 5 );
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f && PlayerPrefs.GetFloat("FullMinimapSize") < maxFullSize) // backwards
            {
                PlayerPrefs.SetFloat("FullMinimapSize", PlayerPrefs.GetFloat("FullMinimapSize") + 5 );
            }
        }
        else
        {
            minimapImage.SetActive(true);
            fullMinimapImage.SetActive(false);
            this.GetComponent<Camera>().orthographicSize = PlayerPrefs.GetFloat("MinimapSize");
            this.GetComponent<Camera>().targetTexture = minimapTexture;

            if (Input.GetKey(KeyCode.Equals) && PlayerPrefs.GetFloat("MinimapSize") > minSize)
            {
                PlayerPrefs.SetFloat("MinimapSize", PlayerPrefs.GetFloat("MinimapSize") - 5 * Time.deltaTime * 2);
            }
            if (Input.GetKey(KeyCode.Minus) && PlayerPrefs.GetFloat("MinimapSize") < maxSize)
            {
                PlayerPrefs.SetFloat("MinimapSize", PlayerPrefs.GetFloat("MinimapSize") + 5 * Time.deltaTime * 2);
            }

            this.transform.position = new Vector3(player.position.x, player.position.y + 20, player.position.z);
        }
        
    }
}
