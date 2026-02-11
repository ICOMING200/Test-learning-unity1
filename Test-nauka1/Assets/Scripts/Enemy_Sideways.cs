using UnityEngine;

public class Enemy_Sideways : MonoBehaviour
{
    //we will define how far the enemy is able to move
    [SerializeField]private float movementDistance;
    [SerializeField]private float speed;
    //we get the ammount of damage that enemy deals
    [SerializeField] private float damage;
    private bool movingLeft;
    private float leftEdge;
    private float rightEdge;

    private void Awake()
    {
        //we determine what is the further point on left that enemy can go to
        leftEdge = transform.position.x - movementDistance;
        //we determine what is the further point on right that enemy can go to
        rightEdge = transform.position.x + movementDistance;
    }

    private void Update()
    {
        //we check if the player is moving left
        if (movingLeft)
        {
            //we check if the player didnt touch the left edge so it needs to still move
            if(transform.position.x >  leftEdge)
            {
                //we move the enemy left
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            //if it touch we need to change direction
            else
            {
                movingLeft = false;
            }
        }
        //if not it is moving right
        else
        {
            //we check if the player didnt touch the right edge so it needs to still move
            if (transform.position.x < rightEdge)
            {
                //we move the enemy right
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            //if it touch we need to change direction
            else
            {
                movingLeft = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //we check if collision is between enemy and player
        if (collision.tag == "Player")
        {
            //we deal the damage to player using Health.cs script
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
