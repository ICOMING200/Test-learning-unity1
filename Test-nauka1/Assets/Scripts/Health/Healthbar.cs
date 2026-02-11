using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    //we create reference for health as playerHealth
    [SerializeField]private Health playerHealth;
    //we create reference for Image as totalhealthBar
    [SerializeField]private Image totalhealthBar;
    //we create reference for Image as currenthealthBar
    [SerializeField] private Image currenthealthBar;

    //everytime the code starts
    private void Start()
    {
        //at the start of the game we set our health as the maximum value because at the start the player current health is always max
        totalhealthBar.fillAmount = playerHealth.currentHealth / 10;
    }

    private void Update()
    {
        //we fill amount that is shown from the healthbar image( /10 because the image fill amount is in lower or equal to 1 where 1 - 100% and so one)
        currenthealthBar.fillAmount = playerHealth.currentHealth / 10;
    }
}
