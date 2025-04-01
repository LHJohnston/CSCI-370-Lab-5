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

    public GameObject monster;

    private Vector2 currentPos;

    private int go;

    private bool up = false;

    private bool right = false;

    private bool left = false;

    private bool down = false;

    private List<bool> directions;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        monsterBod = GetComponent<Rigidbody2D>();
    
        mainGoal = points[0];
        go = 0;
        directions = new List<bool>
        {
            up,
            down,
            left,
            right
        };
        currentPos = new Vector2(monster.transform.position.x, monster.transform.position.y);

    }

    int CheckDirections(){
        bool oneCorrect = false;
        int t = 0;
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
        return t;
    }
    // Update is called once per frame
    void Update()
    {
        
        currentPos = new Vector2(monster.transform.position.x, monster.transform.position.y);
        if(currentPos == mainGoal){
            go += 1;
            mainGoal = points[go%5];
        }
        MoveTowardGoal();
        Chase();
        
    }


    //compare two vectors
    int CompareVectors(Vector2 v1, Vector2 v2){
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

    void MoveTowardGoal(){
        if(CompareVectors(currentPos, mainGoal) == -1 || CheckDirections() == 0 || up == false){
            up = true;
            monsterBod.AddForce(transform.up * speed * Time.deltaTime);
        }
        if(CompareVectors(currentPos, mainGoal) == 1 || CheckDirections() == 1 || down == false){
            down = true;
            monsterBod.AddForce(transform.up * (-speed * Time.deltaTime));
        }
        if(CompareVectors(currentPos, mainGoal) == -2 || CheckDirections() == 2 || left == false){
            left = true;
            monsterBod.AddForce(transform.right * (-speed * Time.deltaTime));
        }
        if(CompareVectors(currentPos, mainGoal) == 2 || CheckDirections() == 3 || right == false){
            right = true;
            monsterBod.AddForce(transform.right * (speed * Time.deltaTime));
        }
    }

    void Turn(){
        int dir = CheckDirections();
        Debug.Log("" + dir);
        if(dir != -1){
        directions[dir] = false;
        directions[(dir + 1) % 4] = true;
       }
    }
    
    void Chase(){
         RaycastHit2D hit = Physics2D.CircleCast(transform.position, 5, Vector2.up, 0, LayerMask.GetMask("Player"));
            if (hit)
            {
                Debug.Log("Hit Something!!" + hit.collider.gameObject.name);

                if (hit.collider.gameObject.TryGetComponent(out GameObject player))
                {
                    mainGoal = new Vector2(player.transform.position.x, player.transform.position.y);
                    //speed += 1;
    
                }
            }
            else{
                mainGoal = points[go%5];
                //speed -= 1;
                }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Turn();
        Debug.Log("Turning");
    }
        
}

