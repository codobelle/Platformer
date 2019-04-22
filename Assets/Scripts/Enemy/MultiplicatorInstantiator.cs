using UnityEngine;

public class MultiplicatorInstantiator : MonoBehaviour {

    public GameObject multiplicatorEnemyPrefab;
    public GameObject player;
    public static bool isKilled = false;
    public static bool isParent = false;
    private int generationCounter = 1;
    private int maxGenerationParent = 2;
    private GameObject parent;
    private GameObject child1;
    private GameObject child2;
    private int health = 100;
    private int damage = 20;
    private int toHalf = 2;
    private int distanceFromPlayer = 5;

    // Use this for initialization
    void Start () {

        parent = Instantiate(multiplicatorEnemyPrefab, transform.position, Quaternion.identity);
        parent.GetComponentInChildren<Enemy>().InstantiateHealthbarAndDamage(health, damage);
        parent.GetComponentInChildren<MultiplicatorEnemy>().isParent = true;
    }
	
	// Update is called once per frame
	void Update () {

        if (isKilled && isParent)
        {
            isParent = false;
            isKilled = false;
            child1 = Instantiate(multiplicatorEnemyPrefab,
               new Vector3(player.transform.position.x + distanceFromPlayer, transform.position.y - 0.3f, player.transform.position.z),
                Quaternion.identity);
            child2 = Instantiate(multiplicatorEnemyPrefab,
                new Vector3(player.transform.position.x - distanceFromPlayer, transform.position.y - 0.3f, player.transform.position.z),
                Quaternion.identity);
            child1.GetComponentInChildren<Enemy>().InstantiateHealthbarAndDamage(health /= toHalf, damage /= toHalf);
            child2.GetComponentInChildren<Enemy>().InstantiateHealthbarAndDamage(health /= toHalf, damage /= toHalf);

            child1.transform.localScale /= 1.2f + 0.2f * generationCounter;
            child2.transform.localScale /= 1.2f + 0.2f * generationCounter;

            generationCounter++;
            if (generationCounter == maxGenerationParent)
            {
                child1.GetComponentInChildren<MultiplicatorEnemy>().isParent = true;
                child2.GetComponentInChildren<MultiplicatorEnemy>().isParent = true;
            }
        }
    }
}