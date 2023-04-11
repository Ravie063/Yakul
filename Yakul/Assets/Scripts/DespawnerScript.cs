using UnityEngine;

public class DespawnerScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (this.gameObject.name == "FrontDespawner")
        {
            if (col.tag == "Cars")
            {
                Destroy(col.gameObject);
            }
        }
        else if(this.gameObject.name == "BackDespawner")
        {
            if (col.name == "CityBlockSpawner")
            {
                GameObject city = col.transform.parent.gameObject;
                Destroy(city);
            }
            Destroy(col.gameObject);
        }
    }
}
