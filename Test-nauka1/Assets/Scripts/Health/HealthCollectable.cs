using UnityEngine;

public class HealthCollectable : MonoBehaviour
{
    [SerializeField] private float healthValue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //we check if the collision is betwen player and object
        if (collision.tag == "Player")
        {
            //we get elements and functions from Health.cs script
            collision.GetComponent<Health>().AddHealth(healthValue);
            //after the player picks up the health object it is no longer visible
            gameObject.SetActive(false);
        }
    }
}
