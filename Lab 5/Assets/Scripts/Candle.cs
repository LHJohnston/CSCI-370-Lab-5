using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Candle : MonoBehaviour
{

    private Light2D candle;
    public int intensity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        candle = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void blowOut(){
        candle.intensity = 0;
    }

    void reLight(){
        candle.intensity = intensity;
    }
}
