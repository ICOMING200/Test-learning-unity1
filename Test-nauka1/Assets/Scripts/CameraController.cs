using Unity.Mathematics;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Room Camera movement
    //camera speed
    [SerializeField] private float speed;
    //camera position
    private float currentPosX;
    //we set camera velocity to 0 because we want smooth transition
    private Vector3 velocity = Vector3.zero;

    //Following player movement
    //we getthe player position (x,y,z)
    [SerializeField] private Transform player;
    //we get information how far ahead should the camera be
    [SerializeField] private float aheadDistance;
    //camera speed
    [SerializeField] private float cameraSpeed;
    //we create variable that will determine in which way the camera need to go and how far
    private float lookAhead;

    private void Update()
    {
        //Room Camera movement
        //we move smoothly camera to its destination (target, destination, velocity, speed)
        //transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX, transform.position.y, transform.position.z), ref velocity, speed);


        //Following player movement
        transform.position = new Vector3(player.position.x + lookAhead, transform.position.y, transform.position.z);
        //we set the lookAhead value so when the starting value for lookAhead is 2 and player is facing right(1) the lookAhead is 2 but when player is facing left(-1) the value is -2
        lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed);
    }

    public void MoveToNewRoom(Transform _newRoom)
    {
        //we change the current x position of the camera to next room x positionso camera follows the new room
        currentPosX = _newRoom.position.x;
    }
}
