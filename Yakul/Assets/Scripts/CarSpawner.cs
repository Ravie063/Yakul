using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField]
    Transform[] CarSpawnPoint;
    [SerializeField]
    GameObject[] Car;
    public float waitTime, maxTime;
    void Start()
    {
        maxTime = waitTime;
    }

    void Update()
    {
        waitTime -= 0.1f;
        if (waitTime <= 0)
        {
            spawnCar();
            waitTime = maxTime;
        }
    }

    void spawnCar()
    {
        //GameObject carPrefab =
           Instantiate(Car[Random.Range(0,2)], CarSpawnPoint[0].position,
               Quaternion.identity); 
         Instantiate(Car[Random.Range(0,2)], CarSpawnPoint[1].position,
             Quaternion.Euler(0, 180, 0));
    }
}
