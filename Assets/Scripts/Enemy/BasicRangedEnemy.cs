using System.Collections;
using UnityEngine;

public class BasicRangedEnemy : Enemy
{
    public float cooldown = 1;

    [SerializeField]
    private int minDistance = 6;
    private int enemySpeed;
    private GameObject bullet;
    private int bulletSpeed = 6;
    private bool isReadyToFire = true;
    private Vector3 forward;
    private Vector3 toOther;


    // Use this for initialization
    private void Start()
    {
        Damage = 10;
        Health = 100;
        InstantiateHealthbar();
    }

    // Update is called once per frame
    private void Update()
    {
        base.EnemyMovement();
        EnemyAttack();
        UpdateHealthBarPosition();
    }
    

    private void EnemyAttack()
    {
        forward = transform.TransformDirection(Vector3.right);
        toOther = player.transform.position - transform.position;
        
        if (Vector3.Dot(forward, toOther) > 0 && Vector3.Distance(player.transform.position, transform.position) < minDistance)
        {
            speed = 0;
            if (isReadyToFire)
            {
                bullet = MenuManager.instance.GetPooledObject(Constants.enemyBulletTag);
                if (bullet != null)
                {
                    bullet.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
                    bullet.transform.eulerAngles = Vector3.zero;
                    bullet.GetComponent<BulletBehaviour>().Damage = Damage;
                    bullet.SetActive(true);

                    bullet.GetComponent<Rigidbody>().velocity = transform.right * bulletSpeed;
                }
                StartCoroutine(BulletCooldown());
            }
        }
        else
        {
            speed = 1;
        }
    }

    private IEnumerator BulletCooldown()
    {
        isReadyToFire = false;
        yield return new WaitForSeconds(cooldown);
        isReadyToFire = true;
    }
}
