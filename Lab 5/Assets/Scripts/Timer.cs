using UnityEngine;

public class SimpleTimer: MonoBehaviour {

public float targetTime; 

private bool timerStart = false;

public bool ended = false;

void Update(){

}

public void timedLight(){
    Debug.Log("" + targetTime);
    if(timerStart == true){

    targetTime -= Time.deltaTime;
    

    if (targetTime <= 0.0f)
        {
            timerEnded();
        }

    }
}

void timerEnded()
{
    Debug.Log("Timer ended");
   ended = true;
   timerStart = false;
}

public void startTimer(){
    timerStart = true;
    ended = false;
    Debug.Log("Starting Timer");
    timedLight();
}

}