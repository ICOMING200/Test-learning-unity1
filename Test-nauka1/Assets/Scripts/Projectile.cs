using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    //we create variable that deterine in which direction will the fireball move (-1 left, 1 right)
    private float direction;
    //we create variable that will determine if fireball hit anything
    private bool hit;
    //we create variable that will describe the lifetime of a fireball
    private float lifetime;

    private BoxCollider2D boxCollider;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        if (hit)
        {
            return;
        }
        //we calculate how much the object schould move in 1 fps
        float movementSpeed = speed * Time.deltaTime * direction;
        //and we move object on x axis 
        transform.Translate(movementSpeed, 0, 0);

        //we add 1 every second to the fireball lifetime to describe how long it lived
        lifetime += Time.deltaTime;
        //we check if the object lived for more than 5 seconds
        if( lifetime > 5 ) 
            //if it does then we deactivate it so we fixe the isuue of fireball flying endlessly
            gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //the fireball hit something
        hit = true;
        //then we disable the fireball
        boxCollider.enabled = false;
        anim.SetTrigger("explode");
    }
    //we create function that will determine in what direction the fireball schould move
    public void setDirection(float _direction)
    {
        //everytime the we set the direction of the object the lifetime is reseted
        lifetime = 0;
        direction = _direction;
        //we activate the player object so it is visible
        gameObject.SetActive(true);
        //the fireball is placed so it doesnt hit anything
        hit = false;
        //so we put fireball in
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;

        //we check if the fireball is not facing the right direction
        if(Mathf.Sign(localScaleX) != _direction)
        {
            //we change the direction of the fireball
            localScaleX = -localScaleX;
        }
        transform.localScale = new Vector3(localScaleX, transform.localScale.y,transform.localScale.z);
    }
    //that function is used in the animation so everytime the fireball explodes it will deactivate
    private void Deactivate()
    {
        //we deactivate the player object
        gameObject.SetActive(false);
    }
}
