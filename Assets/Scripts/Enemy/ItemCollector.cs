using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour {

    public PuzzleManager puzzleManager;
    [HideInInspector]
    public bool isInteraction = false;
    private PlayerHealth playerHealthScript;
    private GameObject pickableItem;
	// Use this for initialization
	void Start () {

        playerHealthScript = GetComponent<PlayerHealth>();
    }
	
	// Update is called once per frame
	void Update () {

        if (pickableItem != null)
        {
            if (pickableItem.CompareTag("Heart") && isInteraction)
            {
                playerHealthScript.playerHealth += 25;
                playerHealthScript.playerMaxHealth += 25;
                playerHealthScript.healthFillAmount.fillAmount = (float)playerHealthScript.playerHealth / playerHealthScript.playerMaxHealth;
                
                Destroy(pickableItem.gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cube"))
        {
            puzzleManager.puzzlePieces.Add(other.GetComponent<PuzzlePiece>());
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        pickableItem = collision.gameObject;
    }

    private void OnTriggerExit(Collider collision)
    {
        pickableItem = null;
    }

}
