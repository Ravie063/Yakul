using System.Collections;
using UnityEngine;

public class CarScript : MonoBehaviour
{
    Rigidbody carRB;
    [SerializeField]
    float randSpeed;
    void Start()
    {
        FindObjectOfType<AudioManagerScript>().Play("Car");
        this.carRB = GetComponent<Rigidbody>();
        randSpeed = Random.Range(35, 40);
    }

    private void FixedUpdate()
    {
        carRB.MovePosition(carRB.position + carRB.transform.forward
            * randSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider col)
    { 
        if (col.tag == "Cars")
        {
            this.randSpeed = 0;
            StartCoroutine(CarCollision(2));
        }
        if(col.name == "EndTrigger")
        {
            Destroy(this.gameObject);
        }
    }
    IEnumerator CarCollision(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        this.randSpeed = 40;
    }
}
