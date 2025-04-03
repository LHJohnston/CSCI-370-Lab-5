using System.Collections;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public GameObject dialougePanel;

    void Update()
    {
        interact();
        
        
    } 
    

    void interact() { 
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 2, Vector2.up, 1, LayerMask.GetMask("Player"));
        if (hit){ 
            dialougePanel.SetActive(true);
        }
        if (!hit){
            dialougePanel.SetActive(false);
        }

    }

}
