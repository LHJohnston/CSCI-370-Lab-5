using UnityEngine;
using System; 

public class Interact : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interacting();
        }
        
    }

    void Interacting() {
        Debug.Log("Looking for Candle");
            RaycastHit2D hit = Physics2D.CircleCast(transform.position, 5, Vector2.up, 0, LayerMask.GetMask("Candles"));
            if (hit)
            {
                Debug.Log("Hit Something!!" + hit.collider.gameObject.name);

                if (hit.collider.gameObject.TryGetComponent(out Candle recipient))
                {
                    recipient.reLight();
                }
            }
    }
}


/*
using System;
using UnityEngine;

public class Give : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject obj;
    public string item;
    public Inventory inventory;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            Interact();
        }
        
    }

    void Interact() {
        Debug.Log("Looking for NPC");
            RaycastHit2D hit = Physics2D.CircleCast(transform.position, 2, Vector2.up, 0, LayerMask.GetMask("NPC"));
            if (hit)
            {
                Debug.Log("Hit Something!!" + hit.collider.gameObject.name);

                if (hit.collider.gameObject.TryGetComponent(out NPC recipient))
                {
                    if(recipient.name == "Coffee Shop Owner"){
                        item = "Keys";
                    }
                    else{
                    item = "Coffee";
                 }
                    if(inventory.HasItem(item)){
                    recipient.inv.AddItem(item);
                    //inventory.RemoveItem(item);
                    //Renderer myRenderer = obj.GetComponent<Renderer>();
                    //myRenderer.sortingOrder = 0;
                    Debug.Log("Item Tranferred");
                    
                    }
                }
            }
    }
    
}
*/