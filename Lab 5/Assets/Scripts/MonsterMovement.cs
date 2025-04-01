using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{

    private Rigidbody2D monsterBod;
    public List<Vector2>  points;

    private Vector2 mainGoal;

    public float speed;
    private BoxCollider2D hitbox;

    private GameObject monster;

    private Vector2 currentPos;

    private int go;

    private bool up;

    private bool right;

    private bool left;

    private bool down;

    private List<bool> directions;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        monsterBod = GetComponent<Rigidbody2D>();
    
        hitbox = GetComponent<BoxCollider2D>();
        mainGoal = points[0];
        go = 0;
        directions.Add(up);
        directions.Add(down);
        directions.Add(left);
        directions.Add(right);
    }

    int checkDirections(){
        bool oneCorrect = false;
        int t = -1;
        for(int i = 0; i < 4; i ++){
            if(directions[i] == true){
                if(oneCorrect == false){
                    oneCorrect = true;
                    t = i;

                }
                else{
                    return -1;
                }
            }
        }
        return 0;
    }
    // Update is called once per frame
    void Update()
    {
        currentPos = new Vector2(monster.transform.position.x, monster.transform.position.y);
        if(currentPos == mainGoal){
            go += 1;
            mainGoal = points[go%5];
        }
        moveTowardGoal();
        chase();
        
    }


    //compare two vectors
    int compareVectors(Vector2 v1, Vector2 v2){
        if(v1.x - v2.x < 0){
            return -1;
        }
        if(v1.x - v2.x > 0){
            return 1;
        }
        if(v1.y - v2.y < 0){
            return -2;
        }
        if(v1.y - v2.y > 0){
            return 2;
        }
        else{
            return 0;
        }
    }

    void moveTowardGoal(){
        if(compareVectors(currentPos, mainGoal) == -1 || checkDirections() != -1){
            up = true;
            monsterBod.AddForce(transform.up * speed);
        }
        if(compareVectors(currentPos, mainGoal) == 1 || checkDirections() != -1){
            down = true;
            monsterBod.AddForce(transform.up * -1 * speed);
        }
        if(compareVectors(currentPos, mainGoal) == -2 || checkDirections() != -1){
            left = true;
            monsterBod.AddForce(transform.right * -1 * speed);
        }
        if(compareVectors(currentPos, mainGoal) == 2 || checkDirections() == -1){
            right = true;
            monsterBod.AddForce(transform.right * speed);
        }
    }

    void turn(){
       int dir = checkDirections();
       if(dir != -1){
       directions[dir] = false;
       directions[dir + 1] = true;
       }
    }
    
    void chase(){
         RaycastHit2D hit = Physics2D.CircleCast(transform.position, 5, Vector2.up, 0, LayerMask.GetMask("Player"));
            if (hit)
            {
                Debug.Log("Hit Something!!" + hit.collider.gameObject.name);

                if (hit.collider.gameObject.TryGetComponent(out GameObject player))
                {
                    mainGoal = new Vector2(player.transform.position.x, player.transform.position.y);
                    speed += 5;
    
                }
            }
            else{
                mainGoal = points[go%5];
                speed -= 5;
                }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        turn();
    }
        
}
