using System.Collections;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;
    LevelController levelController;
    GameTimer gameTimer;

    bool isTransitioning = false;
    bool collisionDisabled = false;

    [SerializeField] float waitToLoad = 10f;
    [SerializeField] GameObject winLabel;
    [SerializeField] GameObject loseLabel;
    
    void Start() {
        audioSource = GetComponent<AudioSource>();
        gameTimer = GetComponent<GameTimer>();
        winLabel.SetActive(false);
        loseLabel.SetActive(false);
    }
    void Update() 
    {
        Debug.Log(gameTimer.triggeredLevelFinished);
        if(gameTimer.triggeredLevelFinished == true)
        {
            // Brakowało referencji między skryptami GameTimer, a CollisionHandler. W chwili, gdy dodałem skrypt CollisionHandler do obiektu Slider, zaczął się (WRESZCIE) pojawiać ekran porażki po upłynięciu wyznaczonego czasu
            StartCrashSequence();
        }
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        } 
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled; // toggle collision
        } 
    }
    void OnCollisionEnter(Collision other)
    {
        if(isTransitioning || collisionDisabled) { return; } 

        {
            switch(other.gameObject.tag)
            {
            case "Friendly":
                Debug.Log("This thing is friendly");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
            }
        }
    }

    void StartSuccessSequence()
    {
        isTransitioning = true;
        winLabel.SetActive(true);
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        //Invoke("LoadNextLevel", levelLoadDelay);
    }

    public void StartCrashSequence()
    {
        isTransitioning = true;
        loseLabel.SetActive(true);
        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        //Invoke("ReloadLevel", levelLoadDelay);   
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
        
    }

    IEnumerator HandleWinCondition()
    {
        winLabel.SetActive(true);
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(waitToLoad);
        FindObjectOfType<LevelLoader>().LoadNextScene();
    }
}
