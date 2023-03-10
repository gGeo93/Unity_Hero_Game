using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DronesMainControllerCenter : MonoBehaviour
{
    public MeshRenderer DroneLocation;
    [SerializeField]DroneController initialDrone;
    public List<Transform> spawnPoints = new List<Transform>();
    public List<DroneController> activeDrones = new List<DroneController>();
    [SerializeField] GameObject dronePrefab;
    
    public RawImage FelonyImg;
    public Text felonyMessage;
    private float felonyMultiplier = 0.025f;
    private float felonyAdditionalMultiplier = 1f;
    [SerializeField] int numberOfSpawningEnemies = 5;
    public float explosionRadius = 10f;
    public int numberOfEnemiesSpawned;

    void Start()
    {
        int index = Random.Range(0,9);
        Vector3 initDroneRndLocation = spawnPoints[index].position;
        initialDrone.GetComponent<DroneController>().SetUpDrone(this);
        initialDrone.transform.position = initDroneRndLocation;
        activeDrones.Add(initialDrone);
        DroneLocation.enabled = true;
        numberOfEnemiesSpawned=0;
        StartCoroutine(SpawningRate(numberOfSpawningEnemies));
    }
    private void Update() {
        if(!PauseMenu.theGameIsPaused)
        {
            if (activeDrones.Count > 0)
            {
                if(Events.onSunsetArrival.Invoke() == true)
                {
                    felonyAdditionalMultiplier = 2f;
                }
                FelonyImg.rectTransform.localScale += Vector3.right * felonyAdditionalMultiplier * 0.0125f / 100f;
                if(FelonyImg.rectTransform.localScale.x >= 1f)
                {
                    felonyMessage.color = Color.red;
                    felonyMessage.fontSize = 18;
                    felonyMessage.text = "Felony has overwhelmed Otaku_City. You've failed";
                    Invoke("QuitGame" , 4f);
                }
            }
            else
            {   
                FelonyImg.rectTransform.localScale -= Vector3.right * felonyMultiplier / 100f;
            }
        }
        FixFelonyBar();
        if(activeDrones.Count == 0 && numberOfEnemiesSpawned == 5)
            PeaceRestored();
    }
    public void DroneRemover(DroneController d)
    {
        activeDrones.Remove(d);
    }
    private void FixFelonyBar()
    {
        if(FelonyImg.rectTransform.transform.localScale.x < 0)
        {
            FelonyImg.rectTransform.transform.localScale= new Vector3(0f,1f,1f);
        }
        else if (FelonyImg.rectTransform.transform.localScale.x > 1)
        {
            FelonyImg.rectTransform.transform.localScale= new Vector3(1f,1f,1f);
        }
    }
    private IEnumerator SpawningRate(int numberOfTotalSpawningEnemies)
    {
        do       
        {
            yield return new WaitForSeconds(30f);
            Debug.Log(numberOfEnemiesSpawned);
            GameObject go = Instantiate(dronePrefab, spawnPoints[RandomSpawnPointIndex()].position, Quaternion.identity);
            go.GetComponent<DroneController>().SetUpDrone(this);
            activeDrones.Add(go.GetComponent<DroneController>());
            numberOfEnemiesSpawned++;
        }while(numberOfEnemiesSpawned<numberOfTotalSpawningEnemies);
    }
    private void PeaceRestored()
    {
        felonyMessage.color = Color.green;
        felonyMessage.fontSize = 18;
        felonyMessage.text = "Congratulations! You've restored peace in Otaku_City\nPress 'Esc' to exit";
        if(Input.GetKeyDown(KeyCode.Escape))
            Invoke("QuitGame",1f);
    }
    private int RandomSpawnPointIndex()
    {
        System.Random rn = new System.Random();
        return rn.Next(0,spawnPoints.Count);
    }
    private void QuitGame(){Application.Quit();}

}
