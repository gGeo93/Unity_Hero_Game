using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class PauseMenu : MonoBehaviour,IMenuOptions
{
   public static bool theGameIsPaused = false;
   [SerializeField] GameObject pausingPanel;
   [SerializeField] GameObject howToPlayPanel;
   public Text instructions;
   [SerializeField] string originalMessage;
   public CinemachineVirtualCamera mainVirtualCamera;
   public CinemachineVirtualCamera attackVirtualCamera;
   public Camera maincamera;
   public Camera mapcamera;
   
   void Awake() 
   {
       Time.timeScale = 1;
   }
   void Start() 
   {
        pausingPanel.SetActive(false);    
        instructions.text = originalMessage;
   }
   void Update() 
   {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            theGameIsPaused = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
        if(theGameIsPaused)
        {
            maincamera.GetComponent<AudioListener>().enabled = false;
            mapcamera.GetComponent<AudioListener>().enabled = true;
        

            if(mainVirtualCamera.enabled == true)
                mainVirtualCamera.gameObject.SetActive(false);
            if(attackVirtualCamera.enabled == true)
                attackVirtualCamera.gameObject.SetActive(false);
            
            pausingPanel.SetActive(true);             
            Time.timeScale = 0;
        }
   }
   public void Play()
   {
        Time.timeScale = 1;
        theGameIsPaused = false;
        maincamera.GetComponent<AudioListener>().enabled = true;
        mapcamera.GetComponent<AudioListener>().enabled = false;
        
        if(mainVirtualCamera.enabled == false)
            mainVirtualCamera.gameObject.SetActive(true);
        if(attackVirtualCamera.enabled == false)
            attackVirtualCamera.gameObject.SetActive(true);
        
        pausingPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
   }
   public void Instructions(string guidelines)
   {
        pausingPanel.SetActive(false);
        howToPlayPanel.SetActive(true);
        howToPlayPanel.GetComponentInChildren<Text>().fontSize = 17;
        howToPlayPanel.GetComponentInChildren<Text>().text = guidelines;
   }
   public void Quit() => Application.Quit();
   public void Back()
    {
        howToPlayPanel.SetActive(false);
        pausingPanel.SetActive(true);
    }
}
