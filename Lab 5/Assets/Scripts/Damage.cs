using UnityEngine;

public class Damage : MonoBehaviour
{
    public int damage = 2;
    private Health playerHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OlisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player"){
            if(playerHealth == null){
                playerHealth = collision.gameObject.GetComponent<Health>();
            }
            playerHealth.TakeDamage(damage);
        }
    }
}
