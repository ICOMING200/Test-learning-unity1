using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //the [SerializeField] will allow e to insert a speed nuber in unity editor that will change the speed variable
    [SerializeField]private float speed;
    private Rigidbody2D body;
    //that section will whenever my script is awake get compontent RigidBody2D(players physic) and use it as body
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //Crateing a Vector2(because Rigidbody is 2D) that will check if key is pressed and put  Horizontal(left/right) that will be ultiplied by speed variable
        body.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.linearVelocity.y);
        //the code check if space key is pressed
        if (Input.GetKey(KeyCode.Space))
            //the body velocity changes on y axis allowin player to jump
            body.linearVelocity = new Vector2(body.linearVelocity.x, speed);
    }
}
