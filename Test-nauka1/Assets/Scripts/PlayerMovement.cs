using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //the [SerializeField] will allow e to insert a speed nuber in unity editor that will change the speed variable
    [SerializeField] private float speed;
    //we create a jumpPower variable so we can separatly tell how far we can jump 
    [SerializeField] private float jumpPower;
    //we create groundLayer to put our ground into a specific layer
    [SerializeField] private LayerMask groundLayer;
    //we create groundLayer to put our walls into a specific layer
    [SerializeField] private LayerMask wallLayer;
    //we get a reference for rigidbody
    private Rigidbody2D body;
    //we get reference for animator
    private Animator anim;
    //we get reference for BoxCollider
    private BoxCollider2D boxCollider;
    //we create float variable that will be used as wall jump Cooldown
    private float wallJumpCooldown;
    //we create float variable
    private float horizontalInput;

    
    private void Awake()
    {
        //that section will whenever my script is awake get compontent RigidBody2D(players physic) and use it as body
        body = GetComponent<Rigidbody2D>();
        //that section will whenever my script is awake get compontent Animator(players animation) and use it as anim
        anim = GetComponent<Animator>();
        //that section will whenever my script is awake get compontent BoxCollider2D(players HitBox) and use it as boxCollider
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        //that will check what key is pressed and put  Horizontal(left/right) and put it in horizontalInput variable
        horizontalInput = Input.GetAxis("Horizontal");
        
        //code check if the player is moving right (horizontalInput>0.01(right))
        if(horizontalInput> 0.01f)
            //changing localScale to 1 so player is ,,facing" right
            transform.localScale = Vector3.one;

        //code check if the player is moving left (horizontalInput<-0.01(left))
        else if (horizontalInput < -0.01f)
            //changing localScale to -1 so player is ,,facing" left
            transform.localScale = new Vector3(-1,1,1);

        //we set animator run parameter to true/false when(horizontalInput is NOT 0 so player is moving then run=true(animation playing) or horizontalInput = 0 so player is NOT moving then run=false(animation stop))
        anim.SetBool("run", horizontalInput != 0);
        //we set animator grounded parameter to true/false when player is on the ground(true) or not(false)
        anim.SetBool("grounded", isGrounded());
        //we check if the cooldown id bigger than 0.2 so we can walk or jump
        if(wallJumpCooldown > 0.2f)
        {
           
            //Crateing a Vector2(because Rigidbody is 2D) that will check what key is pressed in horizontalInput and it will be ultiplied by speed variable
            body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);
            //we check if player is on the wall and at the same time not on the ground
            if(onWall() && !isGrounded())
            {
                //we set our gravity and velocity to zero so player sticks to the wall
                body.gravityScale = 0;
                body.linearVelocity = Vector2.zero;
            }
            else
            {
                //if player is not on the wall we ,,unstick" him giving him his default gravity scale 
                body.gravityScale = 7;
            }
            //the code check if space key is pressed
            if (Input.GetKey(KeyCode.Space))
                //whenever player press space then Jump() function will be activated
                Jump();
        }
        else
        {
            wallJumpCooldown += Time.deltaTime;
        }
    }
    //we create Jump() function
    private void Jump()
    {
        //we check if the player is on the ground
        if (isGrounded())
        {
            //the body velocity changes on y axis allowin player to jump
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower);
            //we set trigger ,,jump" so the animator knows that player is jumping and plays jump animation
            anim.SetTrigger("jump");
        }
        //we check if player is on the wall and at the same time not on the ground
        else if (onWall() && !isGrounded())
        {
            if (horizontalInput == 0)
            {
                //when the player jumps off the wall he will be moving away from the wall(falling)
                //we use the mathf function to get 1 when player is facing right and -1 when player is facing left and - to get the player away from the position he is facing and we multiply it by 10 to determine the force that the player will be pushed away from the wall, and the 0 to determine the vector that the player will be pushed upwards because we dont want a player to go up
                body.linearVelocity = new Vector2(-Mathf.Sign(transform.localPosition.x) * 10, 0);
                //we want sprite to not face at the wall
                transform.localScale = new Vector3(-Mathf.Sign(transform.localPosition.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                //when the player jumps off the wall he will be moving away from the wall and up at the same time
                //we use the mathf function to get 1 when player is facing right and -1 when player is facing left and - to get the player away from the position he is facing and we multiply it by 3 to determine the force that the player will be pushed away from the wall, and the 6 to determine the vector that the player will be pushed upwards
                body.linearVelocity = new Vector2(-Mathf.Sign(transform.localPosition.x) * 3, 6);
            }
            //when player jump off the wall we want him to wait before he can jump again
            wallJumpCooldown = 0;
            
        }
    }
    //we chceck if the collision appear betwen two colliders or rigid bodys so (player-ground)
    private void OnCollisionEnter2D(Collision2D collision)
    {

    }
    //we create function that will tell us if the player is grounded
    private bool isGrounded()
    {
        //we use Raycast to draw virtual arrow from player to determine if he is touching the ground and BoxCast function to swap the arrow with a box to prevent bugs from player standing on edge of the map
        //we determine BoxCast(origin(we use a center of boxCollider),size(we determine size of the boxCollider),angle(we dont want a rotate so we keep it as 0,direction(we will use vector direction pointing down),distance(defines how big the raycast will be),layer(we will use groundLayer))
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center,boxCollider.bounds.size, 0, Vector2.down,0.1f, groundLayer);
        //we return raycastHit result( we check if it is not null so the player is standing on the ground so we return true in other scenario we return false do the player is not on the ground)
        return raycastHit.collider != null;
    }
    //we create function that will tell us if the player is touching the wall
    private bool onWall()
    {
        //we use Raycast to draw virtual arrow from player to determine if he is touching the wall
        //we determine BoxCast(origin(we use a center of boxCollider),size(we determine size of the boxCollider),angle(we dont want a rotate so we keep it as 0,direction(we will use vector direction pointing both left and right),distance(defines how big the raycast will be),layer(we will use wallLayer))
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x,0), 0.1f, wallLayer);
        //we return raycastHit result( we check if it is not null so the player is standing on the ground so we return true in other scenario we return false do the player is not on the ground)
        return raycastHit.collider != null;
    }
}
