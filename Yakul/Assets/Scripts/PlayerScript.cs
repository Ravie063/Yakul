using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    GameObject gameManager;
    GameManagerScript GMScript;

    [SerializeField]
    Transform[] MoveSpot;
    int spot;

    Rigidbody playerRB;
    Animator anim;
    Vector3 moveStrife, jump;
    bool isGrounded, hasJumped = false;

    [Header("Player UI")]
    [SerializeField]
    Image gauge;
    [SerializeField]
    TMP_Text coin;
    [SerializeField]
    TMP_Text[] tCoin;
    float coinNum = 0;

    AudioManagerScript audio;
    private void Awake()
    {
        spot = Random.Range(0, 4);
    }
    void Start()
    {
        GMScript = gameManager.GetComponent<GameManagerScript>();
        anim = GetComponent<Animator>();
        playerRB = GetComponent<Rigidbody>();
        jump = new Vector3(0, 1, 0); 
        audio = FindObjectOfType<AudioManagerScript>();
    }

    void Update()
    {
        //Yakul Gauge
        if (gauge.fillAmount <= 0)
        {
            anim.SetTrigger("Dead");
            Death();
        }
        else
        {
            gauge.fillAmount -= 0.0002f;
        }
        //left
        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            && (playerRB.position.x == MoveSpot[spot].position.x))
        {
            if (spot != 0)
            {
                audio.Play("Move");
                spot--;
            }
        }
        //right
        else if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) 
            && (playerRB.position.x == MoveSpot[spot].position.x))
        {
            if (spot != 3)
            {
                audio.Play("Move");
                spot++;
            }
        }
        //jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            audio.Play("Jump");
            anim.SetTrigger("Jump");
            hasJumped = true;
        }
    }
    private void FixedUpdate()
    {
        //playerRB.MovePosition(playerRB.position + movement.normalized
        //* moveSpeed * Time.fixedDeltaTime);
        moveStrife = Vector3.MoveTowards
             (playerRB.position, new Vector3(MoveSpot[spot].position.x,
             playerRB.position.y, playerRB.position.z),
             GMScript.strifeSpeed * Time.fixedDeltaTime);

        playerRB.MovePosition(moveStrife + playerRB.transform.forward
            * GMScript.forwardSpeed * Time.fixedDeltaTime);

        //playerRB.MovePosition(playerRB.position + transform.forward 
        //    * moveSpeed * Time.fixedDeltaTime);

        //playerRB.position = Vector3.MoveTowards
        //    (playerRB.position, new Vector3(MoveSpot[spot].position.x,
        //    playerRB.position.y, playerRB.position.z),
        //    moveSpeed * Time.fixedDeltaTime);
        if (hasJumped)
        {
            playerRB.AddForce(jump.normalized * GMScript.jumpForce, 
                ForceMode.Impulse);
            
            //playerRB.velocity = new Vector3(playerRB.velocity.x,
            //    jump.y + jumpForce, playerRB.velocity.z);
            
            hasJumped = false;
            StartCoroutine(NormalVelocity(2));
        }
    }
    private void OnCollisionStay(Collision col)
    {
        isGrounded = true;
    }
    private void OnCollisionExit(Collision collision)
    {
            isGrounded = false;
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Yakult")
        {
            Destroy(col.gameObject);
            ObtainedYakult();
        }
        if (col.tag == "Coins")
        {
            Destroy(col.gameObject);
            ObtainedCoin();
        }
        //Collide Obstacles
        if (col.tag == "Obstacles")
        {
            audio.Play("HitObstacles");
            Hit();
        }else if(col.tag == "Cars" || col.tag == "CarStatic")
        {
            audio.Play("HitCar");
            Hit();
        }
        //Level COMPLETE
        if (col.name == "EndTrigger")
        {
            GMScript.NextLevel();
            audio.Stop("Theme");
            audio.Stop("Theme2");
            audio.Play("LevelComplete");
            this.enabled = false;
        }
        if (col.name == "CityBlockSpawner")
        {
            GMScript.CityBlockSpawner(col.transform);
        }
    }
    public void Death()
    {
        Physics.IgnoreLayerCollision(8, 9, false);
        this.GetComponent<CapsuleCollider>().direction = 2;
        audio.Play("Dead");
        GMScript.LoseScreen();
        GMScript.CarSpawner(false);
        this.enabled = false;
    }
    public void Hit()
    {
        gauge.fillAmount -= 0.10f;
        MinusCoin();
        GMScript.forwardSpeed = 7f;
        anim.SetTrigger("Hit");
        anim.SetBool("Blink", true);
        CameraShake.Instance.ShakeCam(5f, .1f);
        Physics.IgnoreLayerCollision(8, 9, true);
        StartCoroutine(ObstacleCollision(2));
        //Debug.Log("Decrease GAUGE");
        //Debug.Log("Decrease SPEED");
    }
    void MinusCoin()
    {
        if (coinNum <= 0)
        {
            coinNum = 0;
        }
        else
        {
            coinNum -= 5;
        }
        coin.text = coinNum.ToString();
    }
    public void ObtainedYakult()
    {
        audio.Play("Yakul");
        gauge.fillAmount += 0.15f;
    }
    public void ObtainedCoin()
    { //+1 coin //+5 money
        audio.Play("Money");
        coinNum += 5;
        coin.text = coinNum.ToString();
        tCoin[0].text = coin.text;
        tCoin[1].text = coin.text;
    }
    public void ObtainedBurger()
    {
        //potential power up to boost speed
    }
    IEnumerator ObstacleCollision(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Physics.IgnoreLayerCollision(8, 9, false);
        anim.SetBool("Blink", false);
        GMScript.forwardSpeed = GMScript.maxForwardSpeed;
    }
    IEnumerator NormalVelocity(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        GMScript.forwardSpeed = GMScript.maxForwardSpeed;
    }
}
