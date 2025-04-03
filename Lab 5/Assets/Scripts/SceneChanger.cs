using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] string scene;

    public void ReactToClick() {
        Debug.Log("I've been clicked" + scene);
        Initiate.Fade(scene, Color.black, 1.0f);
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
