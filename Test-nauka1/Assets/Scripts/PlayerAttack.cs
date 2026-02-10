using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    //we add attack cooldown float variable
    [SerializeField] private float attackCooldown;
    //we set the position from which the bullets will be fired
    [SerializeField] private Transform firePoint;
    //we create an array for all fireballs objects
    [SerializeField] private GameObject[] fireballs;
    //we get reference for animator
    private Animator anim;
    //we get reference for playerMovement.cs file
    private PlayerMovement playerMovement;
    //we create a cooldown timer variable as infinity to allow player to shot when the game starts
    private float cooldownTimer = Mathf.Infinity;

    //The section that activates when the code start
    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }


    private void Update()
    {
        //we check if the left mouse button is clicked and we check if the cooldown timer is bigger than attack cooldown, and we check if the canAttackk function is true in playerMovement.cs
        if(Input.GetMouseButtonDown(0) && cooldownTimer > attackCooldown && playerMovement.canAttack())
        {
            //if we press the button player will Attack
            Attack();
        }
        //we add time to cooldown timer
        cooldownTimer += Time.deltaTime;
    }

    //we create private function Attack
    private void Attack()
    {
        //we trigger attack animation everytime the player attacks
        anim.SetTrigger("attack");
        //we reset cooldown timer
        cooldownTimer = 0;
        //we are using object pooling instead of creating because its realy bad for game performance
        fireballs[FindFireball()].transform.position = firePoint.position;
        //we get a direction from Projectile.cs
        fireballs[FindFireball()].GetComponent<Projectile>().setDirection(Mathf.Sign(transform.localScale.x));
    }
    //we create function that will find available fireball
    private int FindFireball()
    {
        //we create loop to check every item in the array
        for (int i = 0; i < fireballs.Length; i++)
        {
            //we check if the fireball is availble for use
            if (!fireballs[i].activeInHierarchy)
                //if it is then we return the fireball that we can use
                return i;
        }

        return 0;
    }

}
