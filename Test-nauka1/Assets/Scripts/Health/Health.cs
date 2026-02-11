using UnityEngine;

public class Health : MonoBehaviour
{
    //we create variable for starting health
    [SerializeField]private float startingHealth;
    //variable for current health (we can get that value but only set it in that specific script
    public float currentHealth {  get; private set; }
    //we get the animator
    private Animator anim;
    private bool death;

    private void Awake()
    {
        //everytime the script(game) starts the player will have the max health ( starting level)
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
    }

    //function for taking damage
    public void TakeDamage(float _damage)
    {
        //we check if the player health doesnt go below zero after taking damage and decrease the player health
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        
        if (currentHealth > 0)
        {
            //we play the player hurt animation
            anim.SetTrigger("hurt");

        }
        else
        {
            //we check if player isnt death already to prevent infinite animation playing
            if (!death)
            {
                //we play the player death animation
                anim.SetTrigger("die");
                //we make the player disapear after the death
                GetComponent<PlayerMovement>().enabled = false;
                //we make sure the player is dead
                death = true;
            }
            
        }
    }
    public void AddHealth(float _value)
    {
        //we add players health
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }
}
