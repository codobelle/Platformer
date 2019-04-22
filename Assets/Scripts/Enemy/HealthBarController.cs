using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour {

    public GameObject healthBarPrefab;
    public GameObject healEffect, scoreEffect;
    
    public void UpdateHealthBarValue(Image healthBarFillAmount, float value)
    {
        healthBarFillAmount.fillAmount = value;
    }

    public void UpdateHealthBarPosition(GameObject healthBar, Vector3 healthbarPosition)
    {
        healthBar.transform.position = healthbarPosition;
    }

}
