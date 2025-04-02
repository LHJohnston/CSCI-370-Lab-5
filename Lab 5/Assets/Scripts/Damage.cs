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
    public void interact() { 
            RaycastHit2D hit = Physics2D.CircleCast(transform.position, 2, Vector2.up, 1, LayerMask.GetMask("Player"));
            if (hit){
                if(playerHealth == null){
                playerHealth = hit.collider.gameObject.GetComponent<Health>();
            }
            playerHealth.TakeDamage(damage);
        }

            }
        



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player"){
            if(playerHealth == null){
                playerHealth = collision.gameObject.GetComponent<Health>();
            }
            playerHealth.TakeDamage(damage);
        }
    }
}

