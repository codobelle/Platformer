using UnityEngine;

public class BasicPatrolEnemy : Enemy {
    
    private void Start()
    {
        Damage = 10;
        Health = 100;
        InstantiateHealthbar();
    }

    // Update is called once per frame
    void Update()
    {
        base.EnemyMovement();
        UpdateHealthBarPosition();
    }
}
