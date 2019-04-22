using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour {

    public int damage = 25;
    public float cooldown = 1;
    public float range = 1;

    private bool isReadyToFire = true;
    private GameObject bullet;
    private int bulletSpeed = 6;
    private Controller playerController;

    private void Start()
    {
        playerController = GetComponentInParent<Controller>();
    }

    private void OnEnable()
    {
        StartCoroutine(BulletCooldown());
    }

    public void Fire()
    {
        if (isReadyToFire)
        {
            if (transform.parent.tag == Constants.shotgunTag)
            {
                GetBullet(30);
                GetBullet(0);
                GetBullet(-30);
            }
            else
            {
                GetBullet(0);
            }
            StartCoroutine(BulletCooldown());
        }
    }

    private void GetBullet(int rotation)
    {
        bullet = MenuManager.instance.GetPooledObject(Constants.playerBulletTag);
        if (bullet != null)
        {
            bullet.transform.position = transform.position;
            bullet.transform.eulerAngles = Vector3.zero;
            bullet.transform.eulerAngles = new Vector3(bullet.transform.rotation.x, bullet.transform.rotation.y, rotation);
            bullet.GetComponent<BulletBehaviour>().Damage = damage;
            bullet.SetActive(true);
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.right * playerController.direction * bulletSpeed;
        }
    }

    IEnumerator BulletCooldown()
    {
        isReadyToFire = false;
        yield return new WaitForSeconds(cooldown);
        isReadyToFire = true;
    }
}
