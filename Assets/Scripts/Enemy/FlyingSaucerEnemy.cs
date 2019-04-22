using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingSaucerEnemy : Enemy {

    public GameObject target;
    private float timeCounterForChangingDirection = 0;
    private float timeToChangeDirection = 10f;

    private GameObject bullet;
    private int bulletSpeed = 5;
    private bool isReadyToFire = true;
    private int cooldown = 3;
    private int bulletCounter = 0;
    private int maxBulletsPerFly = 3;

    private Vector3 shootTarget;
    private Vector3 shoot;
    private bool groundIsFound = false;
    private RaycastHit hit;
    private float colliderRadius;

    private void Start()
    {
        Damage = 10;
        target = Instantiate(target, new Vector3(transform.position.x, 
            transform.position.y + 1, transform.position.z), 
            Quaternion.identity, transform.parent.parent);
        colliderRadius = target.GetComponent<CapsuleCollider>().radius;
    }
    // Update is called once per frame
    void Update () {

        EnemyFlyingMovement();
        EnemyAttack();

        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            if (hit.transform.CompareTag(Constants.groundTag))
            {
                groundIsFound = true;
            }
            else
            {
                groundIsFound = false;
            }
        }
    }

    private void EnemyFlyingMovement()
    {
        transform.Translate(transform.right * direction * speed * Time.deltaTime);

        timeCounterForChangingDirection += Time.deltaTime;
        if (timeCounterForChangingDirection >= timeToChangeDirection)
        {
            direction *= -1;
            timeCounterForChangingDirection = 0;
            bulletCounter = 0;
        }
    }

    private void EnemyAttack()
    {
        if (isReadyToFire && groundIsFound && bulletCounter <= maxBulletsPerFly)
        {
            bullet = MenuManager.instance.GetPooledObject(Constants.wallExplosionBulletTag);
            if (bullet != null)
            {
                target.SetActive(true);
                target.transform.position = hit.point;
                bullet.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                bullet.transform.eulerAngles = Vector3.zero;
                bullet.GetComponent<BulletBehaviour>().Damage = Damage;
                bullet.SetActive(true);

                shootTarget = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z);
                shoot = (shootTarget - bullet.transform.position).normalized;
                bullet.GetComponent<Rigidbody>().velocity = shoot * bulletSpeed;
                bulletCounter++;
            }
            StartCoroutine(BulletCooldown());
        }

        if (bullet != null && !bullet.gameObject.activeSelf)
        {
            if (target.activeSelf && Vector3.Distance(target.transform.position, player.transform.position) < colliderRadius)
            {
                playerHealthScript.UpdatePlayerHealth(Damage);
            }
            target.SetActive(false);
        }
    }

    IEnumerator BulletCooldown()
    {
        isReadyToFire = false;
        yield return new WaitForSeconds(cooldown);
        isReadyToFire = true;
    }
}
