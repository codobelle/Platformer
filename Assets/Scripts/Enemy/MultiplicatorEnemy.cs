using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MultiplicatorEnemy : Enemy {
    
    public bool isParent = false;
    

    // Update is called once per frame
    private void Update()
    {
        base.EnemyMovement();
        UpdateHealthBarPosition();
    }
}
