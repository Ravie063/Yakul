using UnityEngine;

public class CityBlockSpawnerScript : MonoBehaviour
{
    [SerializeField]
    GameObject cityBlock, cityEmpty, cityPop;
    [SerializeField]
    GameObject endZoneBlock;
    //public void spawnCity(Transform cityBlockPos)
    //{
    //    GameObject city = Instantiate(cityBlock, new Vector3
    //        (cityBlock.transform.position.x,
    //        cityBlock.transform.position.y,
    //        cityBlockPos.position.z + 576/*guesstimate*/),
    //         Quaternion.Euler(0,270,0));
    //}
    public void spawnCity(Transform cityBlockPos)
    {
        GameObject city = null;
        int rand = Random.Range(0, 3);
        switch (rand)
        {
            case 0:
                city = cityEmpty;
                break;
            case 1:
                city = cityPop;
                break;
            case 2:
                city = cityBlock;
                break;
        }
        Instantiate(city, new Vector3
            (cityBlock.transform.position.x,
            cityBlock.transform.position.y,
            cityBlockPos.position.z + 576/*guesstimate*/),
             Quaternion.Euler(0, 270, 0));
    }
    public void endZone(Transform cityBlockPos)
    {
        GameObject end = Instantiate(endZoneBlock, new Vector3
            (endZoneBlock.transform.position.x+ -49.8f,
            endZoneBlock.transform.position.y + .01f,
            cityBlockPos.position.z + 747.2f/*guesstimate*/),
             Quaternion.identity);
    }
}
