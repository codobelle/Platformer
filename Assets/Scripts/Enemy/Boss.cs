using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy {

    private GameObject bulletHealthLevel1, bulletHealthLevel2;
    private int bulletSpeed = 5;
    private int cooldownHealthLevel1 = 1, cooldownHealthLevel2 = 2;
    private bool isReadyToFireInFirstPhase = true, isReadyToFireInSecondPhase = true;
    private int startAngle = 0;
    private float healthFirstPhasePercent = 66f, healthSecondPhasePercent = 33f;
    private Vector3 shootTarget;
    private Vector3 shoot;

    // Use this for initialization
    void Start () {

        Damage = 10;
        Health = 3000;
        InstantiateHealthbar();
    }
	
	// Update is called once per frame
	void Update () {

        UpdateHealthBarPosition();
        if (Health > healthFirstPhasePercent * Health  / 100)
        {
            HealthFirstPhaseAtack();
        }
        if (Health < healthFirstPhasePercent * Health / 100 && Health > healthSecondPhasePercent * Health / 100)
        {
            HealthSecondPhaseAtack();
        }
        if (Health < healthSecondPhasePercent * Health / 100)
        {
            EnemyAttack();
        }
    }
    
    private void EnemyAttack()
    {
        HealthFirstPhaseAtack();
        HealthSecondPhaseAtack();
    }


    private void HealthFirstPhaseAtack()
    {
        if (isReadyToFireInFirstPhase)
        {
            bulletHealthLevel1 = MenuManager.instance.GetPooledObject(Constants.enemyBulletTag);
            if (bulletHealthLevel1 != null)
            {
                bulletHealthLevel1.transform.position = transform.position;
                bulletHealthLevel1.transform.eulerAngles = Vector3.zero;
                bulletHealthLevel1.GetComponent<BulletBehaviour>().Damage = Damage;
                bulletHealthLevel1.SetActive(true);
                shootTarget = new Vector3(player.transform.position.x, player.transform.position.y + Constants.playerColliderPosition, player.transform.position.z);
                shoot = (shootTarget - bulletHealthLevel1.transform.position).normalized;
                bulletHealthLevel1.GetComponent<Rigidbody>().velocity = shoot * bulletSpeed;
            }
            StartCoroutine(BulletCooldownHealthLevel1(cooldownHealthLevel1));
        }
    }

    private void HealthSecondPhaseAtack()
    {
        if (isReadyToFireInSecondPhase)
        {
            for (var i = 0; i < 12; i++, startAngle += 30)
            {
                if (startAngle > 360)
                {
                    startAngle = 0;
                }

                bulletHealthLevel2 = MenuManager.instance.GetPooledObject(Constants.enemyBulletTag);
                if (bulletHealthLevel2 != null)
                {
                    bulletHealthLevel2.transform.position = transform.position;
                    bulletHealthLevel2.transform.eulerAngles = Vector3.zero;
                    bulletHealthLevel2.GetComponent<BulletBehaviour>().Damage = Damage;
                    bulletHealthLevel2.transform.Rotate(Vector3.forward * startAngle);
                    bulletHealthLevel2.SetActive(true);
                    bulletHealthLevel2.GetComponent<Rigidbody>().velocity = bulletHealthLevel2.transform.right * bulletSpeed;
                }
            }
            StartCoroutine(BulletCooldownHealthLevel2(cooldownHealthLevel2));
        }
    }

    IEnumerator BulletCooldownHealthLevel1(int cooldown)
    {
        isReadyToFireInFirstPhase = false;
        yield return new WaitForSeconds(cooldown);
        isReadyToFireInFirstPhase = true;
    }

    IEnumerator BulletCooldownHealthLevel2(int cooldown)
    {
        isReadyToFireInSecondPhase = false;
        yield return new WaitForSeconds(cooldown);
        isReadyToFireInSecondPhase = true;
    }
}
