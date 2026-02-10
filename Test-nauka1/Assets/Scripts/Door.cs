using UnityEngine;

public class Door : MonoBehaviour
{
    //we need to check the previous room
    [SerializeField]private Transform previousRoom;
    //we need to check the next room
    [SerializeField]private Transform nextRoom;
    [SerializeField] private CameraController cam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //we check if the player touched the door
        if(collision.tag == "Player")
        {
            //we check from which direction the player is heading < (left), > (right)
            if (collision.transform.position.x < transform.position.x)
                //we are going to new room because we are moving right
                cam.MoveToNewRoom(nextRoom);
            else
                //we are going to previous room because we are moving left
                cam.MoveToNewRoom(previousRoom);
        }    
    }
}
