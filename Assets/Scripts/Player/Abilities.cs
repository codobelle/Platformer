using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour {
    [SerializeField]
    private GameObject checkPointParticleEffectPrefab;
    private GameObject checkPointParticleEffect;
    [SerializeField]
    private InputManager InputManager;
    private Gun gunScript;
    private Controller controllerScript;
    private Weapons weaponsScript;
    private ItemCollector itemCollector;
    private float timeCounter = 0, pressTimeDelay = 1.9f;
    private float checkpointCooldown = 60.0f;
    private bool hasCheckpoint = false;

    // Use this for initialization
    void Start () {
        hasCheckpoint = false;
        gunScript = GetComponentInChildren<Gun>();
        controllerScript = GetComponent<Controller>();
        weaponsScript = GetComponent<Weapons>();
        itemCollector = GetComponent<ItemCollector>();
    }
	
	// Update is called once per frame
	void Update () {

        if (InputManager.IsDash())
        {
            Dash();
        }
        if (InputManager.IsAttack())
        {
            Attack();
        }
        if (InputManager.IsGroundSmash())
        {
            GroundSmash();
        }
        if (InputManager.IsCheckpoint())
        {
            if (!hasCheckpoint)
            {
                timeCounter += Time.deltaTime;
                if (timeCounter >= pressTimeDelay)
                {
                    timeCounter = 0;
                    CreateCheckpoint();
                }
            }
        }
        if (InputManager.IsInteraction())
        {
            weaponsScript.isInteraction = true;
            itemCollector.isInteraction = true;
        }
        else
        {
            weaponsScript.isInteraction = false;
            itemCollector.isInteraction = false;
        }
    }
    
    private void Attack()
    {
        gunScript = GetComponentInChildren<Gun>();
        if (gunScript != null)
        {
            gunScript.Fire();
        }
    }

    private void Dash()
    {
        controllerScript.Dash();
    }

    public void GroundSmash()
    {
        controllerScript.GroundSmash();
    }

    public void CreateCheckpoint()
    {
        print("CreateCheckpoint" + timeCounter);
        hasCheckpoint = true;
        GetComponent<PlayerHealth>().checkpoint = transform.position;
        checkPointParticleEffect = Instantiate(checkPointParticleEffectPrefab, transform.position, Quaternion.identity);
        Destroy(checkPointParticleEffect, 5);
        Invoke("EnableCheckpointAbility",checkpointCooldown);
    }

    private void EnableCheckpointAbility()
    {
        hasCheckpoint = false;
    }
}
