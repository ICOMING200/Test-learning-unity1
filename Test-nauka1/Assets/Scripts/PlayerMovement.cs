using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //the [SerializeField] will allow e to insert a speed nuber in unity editor that will change the speed variable
    [SerializeField]private float speed;
    //we get a rigidbody as body
    private Rigidbody2D body;
    //we get animator as anim
    private Animator anim;
    //we create variable grounded that checks if player is on the ground
    private bool grounded;
    
    private void Awake()
    {
        //that section will whenever my script is awake get compontent RigidBody2D(players physic) and use it as body
        body = GetComponent<Rigidbody2D>();
        //that section will whenever my script is awake get compontent Animator(players animation) and use it as anim
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        //that will check what key is pressed and put  Horizontal(left/right) and put it in horizontalInput variable
        float horizontalInput = Input.GetAxis("Horizontal");
        //Crateing a Vector2(because Rigidbody is 2D) that will check what key is pressed in horizontalInput and it will be ultiplied by speed variable
        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);
        //code check if the player is moving right (horizontalInput>0.01(right))
        if(horizontalInput> 0.01f)
            //changing localScale to 1 so player is ,,facing" right
            transform.localScale = Vector3.one;

        //code check if the player is moving left (horizontalInput<-0.01(left))
        else if (horizontalInput < -0.01f)
            //changing localScale to -1 so player is ,,facing" left
            transform.localScale = new Vector3(-1,1,1);

        //the code check if space key is pressed and if the player is on the ground (to avoid infinite jumps)
        if (Input.GetKey(KeyCode.Space) && grounded)
            //whenever player press space then Jump() function will be activated
            Jump();
            

        //we set animator run parameter to true/false when(horizontalInput is NOT 0 so player is moving then run=true(animation playing) or horizontalInput = 0 so player is NOT moving then run=false(animation stop))
        anim.SetBool("run", horizontalInput != 0);
        //we set animator grounded parameter to true/false when player is on the ground(true) or not(false)
        anim.SetBool("grounded", grounded);
    }
    //we create Jump() function
    private void Jump()
    {
        //the body velocity changes on y axis allowin player to jump
        body.linearVelocity = new Vector2(body.linearVelocity.x, speed);
        //we set trigger ,,jump" so the animator knows that player is jumping and plays jump animation
        anim.SetTrigger("jump");
        //when player jumps he is not on the ground so we set grounded to false
        grounded = false;
    }
    //we chceck if the collision appear betwen two colliders or rigid bodys so (player-ground)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //we check if the object that player touch tag is Ground
        if(collision.gameObject.tag == "Ground")
            //if true then we know that we touch the ground so we set grounded to true
            grounded = true;
    }
}
