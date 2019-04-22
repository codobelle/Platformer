using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingSaucerEnemyInstantiator : MonoBehaviour {
    [SerializeField]
    private GameObject flyingSaucerEnemy;
    [SerializeField]
    private GameObject player;
    private float timeToInstantiate = 30; // 30 seconds
    private float timeToChangeRateAfter3Min = 180f; //3 minutes
    private float timeToChangeRateAfter5Min = 300f; //3 minutes

    // Use this for initialization
    void Start () {

        Instantiate(flyingSaucerEnemy, new Vector3(player.transform.position.x + 5, player.transform.position.y + 5, player.transform.position.z),
            Quaternion.identity, transform.parent);

        StartCoroutine(InstantiateThisEnemy());
    }
	
	// Update is called once per frame
	void Update () {

        if (Time.timeSinceLevelLoad >= timeToChangeRateAfter3Min)
        {
            timeToInstantiate = 15f;
        }
        else if (Time.timeSinceLevelLoad >= timeToChangeRateAfter5Min)
        {
            timeToInstantiate = 7f;
        }
    }

    private IEnumerator InstantiateThisEnemy()
    {
        yield return new WaitForSeconds(timeToInstantiate);
        Instantiate(flyingSaucerEnemy, new Vector3(player.transform.position.x, player.transform.position.y + 5, player.transform.position.z),
            Quaternion.identity, transform.parent);
        StartCoroutine(InstantiateThisEnemy());
    }
}
