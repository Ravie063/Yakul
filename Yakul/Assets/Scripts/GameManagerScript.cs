using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public int maxLevel = 1, count = 0;

    [Header("Player Status")]
    public float strifeSpeed;
    public float forwardSpeed;
    public float maxForwardSpeed;
    public float jumpForce;

    CarSpawner carSpawner;
    CityBlockSpawnerScript citySpawner;

    [SerializeField]
    GameObject loseUI, levelCompUI;
    [SerializeField]
    GameObject[] IntroOutroVid;
    [SerializeField]
    Animator fadeAnim;
    public float waitTime = 1;
    bool vidplaying;
    int isOutro = 0;
    private void Start()
    {
        maxForwardSpeed = forwardSpeed;
        carSpawner = this.GetComponent<CarSpawner>();
        citySpawner = this.GetComponent<CityBlockSpawnerScript>();
    }
    private void Update()
    {
        if (Input.anyKey && vidplaying == true)
        {
            if (isOutro == 0)
            {
                ButtonNextLevel();
            }
            else
            {
                ButtonMenu();
            }
            vidplaying = false;
        }
    }
    IEnumerator LevelLoader(int levelIndex)
    {
        fadeAnim.SetTrigger("FadeOut");
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(levelIndex);
    }
    IEnumerator Fade(int vid)
    {
        fadeAnim.SetTrigger("FadeOut");
        yield return new WaitForSeconds(waitTime);
        IntroOutroVid[vid].SetActive(true);
        vidplaying = true;
        isOutro = vid;
    }
    public void CarSpawner(bool isActive)
    {
        carSpawner.enabled = isActive;
    }
    public void CityBlockSpawner(Transform col)
    {
        if (count != maxLevel)
        {
            count++;
            if (count == maxLevel)
            {
                CarSpawner(false);
                citySpawner.endZone(col.transform);
                citySpawner.spawnCity(col.transform);
            }
            else
            {
                citySpawner.spawnCity(col.transform);
            }
        }
    }
    public void LoseScreen()
    {//Game Over
        loseUI.SetActive(true);
    }
    public void WinScreen()
    {//Play Outro  
        StartCoroutine(Fade(1));
    }
    public void ButtonStart()
    {//Play intro
        StartCoroutine(Fade(0));
    }
    public void ButtonMenu()
    {//Back to main menu
        StartCoroutine(LevelLoader(0));
    }
    public void ButtonRestart()
    {
        StartCoroutine(LevelLoader(SceneManager.GetActiveScene().buildIndex));
    }
    public void ButtonNextLevel()
    {
        StartCoroutine(LevelLoader(SceneManager.GetActiveScene().buildIndex + 1));
    }
    public void NextLevel()
    {
        levelCompUI.SetActive(true);
    }
}
