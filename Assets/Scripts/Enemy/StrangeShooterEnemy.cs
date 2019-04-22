using System.Collections;
using UnityEngine;

public class StrangeShooterEnemy : Enemy
{
    public float cooldown = 0;
    private GameObject bullet;
    private int bulletSpeed = 6;
    private bool isReadyToFire = true;
    private Vector3 shootDirection;
    private Vector3 shootTarget;
    private int bulletCounter = 0;
    private int maxNrOfBullets = 3;
    private float waitForGunCharge = 2f;
    private float waitForBullet = .5f;
    private int explosionDistanceImpact = 2;
    private int maxTargetDistance = 7;
    private float distanceEnemyTarget;
    private bool startedAttack = false;

    // Use this for initialization
    void Start()
    {
        Damage = 15;
        Health = 100;
        InstantiateHealthbar();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthBarPosition();
        distanceEnemyTarget = Vector3.Distance(transform.position, player.transform.position);
        if (bullet != null && bullet.GetComponent<BulletBehaviour>().wasExplosion &&
             Vector3.Distance(bullet.transform.position, player.transform.position) < explosionDistanceImpact)
        {
            bullet.GetComponent<BulletBehaviour>().wasExplosion = false;
            playerHealthScript.UpdatePlayerHealth(Damage);
        }

        if (distanceEnemyTarget < maxTargetDistance)
        {
            EnemyAttack();
            startedAttack = true;
        }
        else if (distanceEnemyTarget > maxTargetDistance && bulletCounter < maxNrOfBullets && startedAttack)
        {
            EnemyAttack();
        }
    }

    private void EnemyAttack()
    {
        if (isReadyToFire && bulletCounter < maxNrOfBullets)
        {
            bullet = MenuManager.instance.GetPooledObject(Constants.wallExplosionBulletTag);
            if (bullet != null)
            {
                bullet.transform.position = transform.position;
                bullet.transform.eulerAngles = Vector3.zero;
                bullet.GetComponent<BulletBehaviour>().Damage = Damage;
                bullet.SetActive(true);

                shootTarget = new Vector3(player.transform.position.x, 
                    player.transform.position.y + Constants.playerColliderPosition,
                    player.transform.position.z);
                shootDirection = (shootTarget - bullet.transform.position).normalized;

                bullet.GetComponent<Rigidbody>().velocity = shootDirection * bulletSpeed;
                bulletCounter++;
            }
            StartCoroutine(BulletCooldown());
        }
    }

    private IEnumerator BulletCooldown()
    {
        isReadyToFire = false;

        if (bulletCounter == maxNrOfBullets)
        {
            bulletCounter = 0;
            cooldown = waitForGunCharge;
            startedAttack = false;
        }
        else
        {
            cooldown = waitForBullet;
        }
        yield return new WaitForSeconds(cooldown);

        isReadyToFire = true;
    }
}
