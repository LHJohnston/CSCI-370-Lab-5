using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Candle : MonoBehaviour
{

    private Light2D candle;
    public int intensity;
    private SimpleTimer timer;
    public float duration;

    private bool isLit = false;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = new SimpleTimer();
        timer.targetTime = duration;
        candle = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isLit){
            timer.startTimer();
            Debug.Log("Timer Started");

        }
        if(timer.ended){
            blowOut();
        }
        
    }

    void timedLight(){
        if(isLit){
            timer.startTimer();
            Debug.Log("Timer Started");

        }
        if(timer.ended){
            blowOut();
        }
    }

    public void blowOut(){
        candle.intensity = 0;
        isLit = false;
        Debug.Log("Extinguishing");
    }

    public void reLight(){
        candle.intensity = intensity;
        isLit = true;
        Debug.Log("Lighting Candle");
    }
}
