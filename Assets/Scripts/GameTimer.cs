using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    [Tooltip("Our level timer in SECONDS")]
    [SerializeField] public float levelTime = 20;
    public bool triggeredLevelFinished = false;

    CollisionHandler collisionHandler;
    LevelController levelController;
    

    void Start() {
        collisionHandler = GetComponent<CollisionHandler>();
        levelController = GetComponent<LevelController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(triggeredLevelFinished) { return; }
        FindObjectOfType<Slider>().value = Time.timeSinceLevelLoad / levelTime;

        bool timerFinished = (Time.timeSinceLevelLoad >= levelTime);
        if(timerFinished)
        {
            triggeredLevelFinished = true;
        }
    }
}