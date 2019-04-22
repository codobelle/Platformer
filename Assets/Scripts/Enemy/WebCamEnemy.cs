using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebCamEnemy : Enemy {
    public GameObject mainLaser;
    public Transform startPoint;
    public Transform endPoint;
    public LineRenderer[] lasers;
    private RaycastHit hit;
    private int collisionTimes = 0;
    private int attackCooldown = 2;

    // Use this for initialization
    void Start () {

        Damage = 25;

        for (int i = 0; i < lasers.Length; i++)
        {
            lasers[i].SetPosition(0, lasers[i].transform.position);
            lasers[i].SetPosition(1, endPoint.position);
        }
        mainLaser.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (collisionTimes == 0)
        {
            if (other.CompareTag(Constants.playerTag))
            {
                collisionTimes++;
                StartCoroutine(EnableDangerousArea());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        collisionTimes = 0;
        StopAllCoroutines();
        if (mainLaser.activeSelf)
        {
            mainLaser.SetActive(false);
        }
    }

    private void EnemyAttack()
    {
        mainLaser.SetActive(true);
        playerHealthScript.UpdatePlayerHealth(Damage);
        StartCoroutine(DisableDangerousArea());
    }

    private IEnumerator EnableDangerousArea()
    {
        yield return new WaitForSeconds(attackCooldown);
        EnemyAttack();
    }

    private IEnumerator DisableDangerousArea()
    {
        yield return new WaitForSeconds(1);
        mainLaser.SetActive(false);
        StartCoroutine(EnableDangerousArea());
    }

}
