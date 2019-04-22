using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    [HideInInspector]
    public Vector3 checkpoint;
    public GameObject healthBar;
    [HideInInspector]
    public int playerHealth = 100;
    [HideInInspector]
    public int playerMaxHealth = 100;
    [HideInInspector]
    public Image healthFillAmount;

    // Use this for initialization
    void Start ()
    {
        healthFillAmount = healthBar.transform.GetChild(0).GetChild(0).GetComponentInChildren<Image>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        // position health bar
        Vector3 wantedPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + 2, transform.position.z));
        healthBar.transform.position = wantedPos;
    }

    public void UpdatePlayerHealth(int damage)
    {
        playerHealth -= damage;
        VerifyPlayerHealth();
        healthFillAmount.fillAmount = (float)playerHealth / playerMaxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BasicPatrolEnemy>() != null)
        {
            UpdatePlayerHealth(other.GetComponent<BasicPatrolEnemy>().Damage);
        }

        if (other.GetComponent<MultiplicatorEnemy>() != null)
        {
            UpdatePlayerHealth(other.GetComponent<MultiplicatorEnemy>().Damage);
        }

        if (other.CompareTag("Destroyer"))
        {
            playerHealth = 0;
            VerifyPlayerHealth();
        }
    }
    
    private void VerifyPlayerHealth()
    {
        //verify health value
        if (playerHealth <= 0 && checkpoint != Vector3.zero)
        {
            transform.position = checkpoint;
            playerHealth = playerMaxHealth;
        }
        else if (playerHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            GetComponent<CapsuleCollider>().enabled = false;
            Invoke("EnableImunity", 3f);
        }
    }
    private void EnableImunity()
    {
        GetComponent<CapsuleCollider>().enabled = true;
    }
}
