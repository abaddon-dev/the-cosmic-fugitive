using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerateShip : MonoBehaviour
{
    [SerializeField] public float accelerationTime = 10f;
    public bool triggeredAccelerationFinished = false;

    void Update()
    {
        //if(triggeredLevelFinished) { return; }
        //FindObjectOfType<Slider>().value = Time.timeSinceLevelLoad / levelTime;

        bool timerFinished = (Time.timeSinceLevelLoad >= accelerationTime);
        if(timerFinished)
        {
            triggeredAccelerationFinished = true;
            FindObjectOfType<Movement>().mainThrust = 1000f;
        }
    }
    void OnCollisionEnter(Collision other) {
        FindObjectOfType<Movement>().mainThrust = 1200f;
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<Renderer>().enabled = false;
    }
}
