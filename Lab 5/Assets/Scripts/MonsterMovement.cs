using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MonsterMovement : MonoBehaviour
{
    private Rigidbody2D monsterBod;
    public float speed;

    public GameObject monster;
    private string direction;

    private bool moving = false;

    private bool playerNear = false;

    public GameObject player;

    private bool cont = false;

    Animator animator;
    private Vector2 lastMoveDirection;


    // the distance that the monster can detect the player from
    public int dis;



     void Start()
    {
        monsterBod = GetComponent<Rigidbody2D>();
        direction = "";
        MoveUp();

        animator = GetComponent<Animator>();
    }


    void Update()
    {

        Chase();
        if(playerNear){
            monsterBod.linearVelocity = new Vector2(0,0);

            Debug.Log("Chasing");
            monster.transform.position = Vector2.MoveTowards(monster.transform.position, player.transform.position, 1 * Time.deltaTime);

        }
        else{
            if(moving == false || cont == true){
            MoveTowardGoal();
            cont = false;
            Debug.Log("" + direction);
            }
        }


        
    }


    void MoveUp(){
         monsterBod.AddForce(transform.up * speed);
         direction = "Up";
         moving = true;

        lastMoveDirection = new Vector2(0, 1);
        SetAnimatorParameters(lastMoveDirection);
    }

    void MoveDown(){
        monsterBod.AddForce(transform.up *-speed );
        direction = "Down";
        moving = true;

        lastMoveDirection = new Vector2(0, -1);
        SetAnimatorParameters(lastMoveDirection);

    }

    void MoveLeft(){
        monsterBod.AddForce(transform.right * -speed );
        direction = "Left";
        moving = true;

        lastMoveDirection = new Vector2(-1, 0);
        SetAnimatorParameters(lastMoveDirection);
    }

    void MoveRight(){
        monsterBod.AddForce(transform.right * speed );
        direction = "Right";
        moving = true;

        lastMoveDirection = new Vector2(1, 0);
        SetAnimatorParameters(lastMoveDirection);
    }

    void SetAnimatorParameters(Vector2 moveDirection)
    {
        animator.SetFloat("MoveX", moveDirection.x);
        animator.SetFloat("MoveY", moveDirection.y);
        animator.SetFloat("MoveMagnitude", moveDirection.magnitude);

        animator.SetFloat("LastMoveX", lastMoveDirection.x);
        animator.SetFloat("LastMoveY", lastMoveDirection.y);
    }




    void Chase(){
         RaycastHit2D hit = Physics2D.CircleCast(transform.position, dis, Vector2.up, 0, LayerMask.GetMask("Player"));
            if (hit)
            {
                Debug.Log("Hit Something!!" + hit.collider.gameObject.name);

                
                    //speed += 10;
                    playerNear = true;
                    cont = true;
                    
    
                
            }
            else{
                //speed -= 10;
                playerNear = false;
                //cont = true;
                }
    }


     void MoveTowardGoal(){
        monsterBod.linearVelocity = new Vector2(0,0);


        if(direction == "Up"){
            MoveUp();
        }
        if(direction == "Down"){
            MoveDown();
        }
        if(direction == "Left"){
            MoveLeft();
        }
        if(direction == "Right"){
            MoveRight();
        }
        
    }


    void Turn(){
        Debug.Log("Turning");
        //moving = false;
        monsterBod.linearVelocity = new Vector2(0,0);
        if(direction == "Up"){
            monsterBod.AddForce(transform.up *-speed );
            direction = "Left";
            monsterBod.linearVelocity = new Vector2(0,0);
            moving = false;

        }
        else{
            if(direction == "Left"){
                monsterBod.AddForce(transform.right * speed );
                direction = "Down";
                monsterBod.linearVelocity = new Vector2(0,0);
                moving = false;
            }
            else{
                if(direction == "Down"){
                    monsterBod.AddForce(transform.up * speed);
                    direction = "Right";
                    monsterBod.linearVelocity = new Vector2(0,0);
                    moving = false;
            }
                else{
                    if(direction == "Right"){
                        monsterBod.AddForce(transform.right * -speed );
                        direction = "Up";
                        monsterBod.linearVelocity = new Vector2(0,0);
                         moving = false;
                    }
                }
            }
        }

        lastMoveDirection = new Vector2((direction == "Left" || direction == "Right") ? (direction == "Left" ? -1 : 1) : 0, 
                                        (direction == "Up" || direction == "Down") ? (direction == "Up" ? 1 : -1) : 0);
        SetAnimatorParameters(lastMoveDirection);
        

    }

     void OnCollisionEnter2D(Collision2D collision)
    {
        Turn();
    }
    

}

    /*private Rigidbody2D monsterBod;
    public List<Vector2>  points;

    private Vector2 mainGoal;

    public float speed;

    private bool hitWall = false;

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

        if(hitWall){
            Turn();
        }

         
        
    }


    //compare two vectors
    int CompareVectorsX(Vector2 v1, Vector2 v2){
        if(v1.x - v2.x < 0){
            return -1;
        }
        if(v1.x - v2.x > 0){
            return 1;
        }
        else{
            return 0;
        }
    }
    int CompareVectorsY(Vector2 v1, Vector2 v2){
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
        //Debug.Log("" + CheckDirections());
        //Debug.Log("" + up);

        if((CompareVectorsX(currentPos, mainGoal) == -1 || CheckDirections() == 0) && up == false){
            up = true;
            monsterBod.AddForce(transform.up * speed * Time.deltaTime);
        }
        if((CompareVectorsX(currentPos, mainGoal) == 1 || CheckDirections() == 1) && down == false){
            down = true;
            monsterBod.AddForce(transform.up * (-speed * Time.deltaTime));
        }
        if((CompareVectorsY(currentPos, mainGoal) == -2 || CheckDirections() == 2) && left == false){
            left = true;
            monsterBod.AddForce(transform.right * (-speed * Time.deltaTime));
        }
        if((CompareVectorsY(currentPos, mainGoal) == 2 || CheckDirections() == 3) && right == false){
            right = true;
            monsterBod.AddForce(transform.right * (speed * Time.deltaTime));
        }
    }

    void Turn(){
        int dir = CheckDirections();
        monsterBod.totalForce = new Vector2(0, 0);
        hitWall = false;
        
        //Debug.Log("" + dir);
        if(dir != -1){
        directions[dir] = false;
        //directions[go % 4] = true;
        Debug.Log("" + CheckDirections());
        Debug.Log("" + directions[dir + 1]);
        
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
         RaycastHit2D hit = Physics2D.CircleCast(transform.position, 1, Vector2.up, 0, LayerMask.GetMask("Walls"));
            if (hit)
            {
                Debug.Log("Hit Something!!" + hit.collider.gameObject.name);

                if (hit.collider.gameObject.TryGetComponent(out GameObject walls))
                {
                    hitWall = true;

    
                }
            }
            
        //Turn();
        Debug.Log("Turning");
    }
        
}
*/
