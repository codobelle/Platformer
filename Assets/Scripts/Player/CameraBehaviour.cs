using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public GameObject player;
    private float speed = 2f;
    private Vector3 target;

    // LateUpdate is called after Update each frame
    void FixedUpdate()
    {
        target = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
        // Set the position of the camera's transform. 
        transform.position = Vector3.Lerp(transform.position, target, speed * Time.deltaTime);
    }
}
