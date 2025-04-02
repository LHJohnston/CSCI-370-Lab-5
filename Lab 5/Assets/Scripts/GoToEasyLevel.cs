using UnityEngine;

public class GoToEasyLevel : MonoBehaviour
{

    public void ReactToClick() {
        Debug.Log("I've been clicked");
        Initiate.Fade("Scene 1", Color.black, 1.0f);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
