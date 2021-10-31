using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] float waitToLoad = 10f;
    [SerializeField] GameObject winLabel;
    [SerializeField] GameObject loseLabel;
    bool levelTimerFinished = false;

    public void Start()
    {
        winLabel.SetActive(false);
        loseLabel.SetActive(false);
    }

    IEnumerator HandleWinCondition()
    {
        winLabel.SetActive(true);
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(waitToLoad);
        FindObjectOfType<LevelLoader>().LoadNextScene();
    }

    public void HandleLoseCondition()
    {
        loseLabel.SetActive(true);
        Time.timeScale = 0;
    }

    public void LevelTimerFinished()
    {
        
        levelTimerFinished = true;
    }
}
