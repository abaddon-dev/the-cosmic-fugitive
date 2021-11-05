using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowerShip : MonoBehaviour
{
    [SerializeField] public float slowTime = 10f;
    public bool triggeredSlowFinished = false;

    void Update()
    {
        //if(triggeredLevelFinished) { return; }
        //FindObjectOfType<Slider>().value = Time.timeSinceLevelLoad / levelTime;

        bool timerFinished = (Time.timeSinceLevelLoad >= slowTime);
        if(timerFinished)
        {
            triggeredSlowFinished = true;
            FindObjectOfType<Movement>().mainThrust = 1000f;
        }
    }
    void OnCollisionEnter(Collision other) {
        FindObjectOfType<Movement>().mainThrust = 800f;
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<Renderer>().enabled = false;
    }
}
