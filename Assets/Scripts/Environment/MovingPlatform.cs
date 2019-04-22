using UnityEngine;

public class MovingPlatform : MonoBehaviour {
    
    [SerializeField]
    private int speed = 3;
    [SerializeField]
    private int maxDistance = 1;

    private Vector2 startPosition;
    private Vector2 newPosition;
    private Transform playerParent;
    
    void Start()
    {
        startPosition = transform.position;
        newPosition = transform.position;
    }

    void Update()
    {
        newPosition.x = startPosition.x + (maxDistance * Mathf.Sin(Time.time * speed));
        transform.position = newPosition;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag(Constants.playerTag))
        {
            playerParent = other.transform.parent;
            other.transform.parent = transform;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.transform.CompareTag(Constants.playerTag))
        {
            other.transform.parent = playerParent;
        }
    }
}
