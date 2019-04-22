using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour {

    public Transform leftTarget, rightTarget;
    protected GameObject player;

    [SerializeField]
    protected int speed = 2;
    protected PlayerHealth playerHealthScript;
    [SerializeField]
    private GameObject dieEffect;
    private GameObject healthBarControllerGO;
    private HealthBarController healthBarController;
    private GameObject healthBar;
    private Image healthFillAmount;
    private int health = 1;
    private int damage = 10;
    private int currentHealth;
    private Vector3 healthBarPosition;

    protected float direction = 1;

    public int Health
    {
        get { return health; }
        set { health = value; }
    }
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    private void Awake()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.playerBulletTag))
        {
            currentHealth -= other.gameObject.GetComponent<BulletBehaviour>().Damage;
            if (currentHealth <= 0)
            {
                player.GetComponent<PlayerScore>().UpdateScore(Damage);
                if (GetComponent<MultiplicatorEnemy>() != null)
                {
                    MultiplicatorInstantiator.isKilled = true;
                    MultiplicatorInstantiator.isParent = GetComponent<MultiplicatorEnemy>().isParent;
                }
                dieEffect = Instantiate(dieEffect, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
                Destroy(dieEffect, 3);
                Destroy(transform.parent.gameObject);
                Destroy(healthBar);
            }
            healthBarController.UpdateHealthBarValue(healthFillAmount, (float)currentHealth / Health);
        }
    }

    public void InstantiateHealthbarAndDamage(int health, int damage)
    {
        Health = health;
        Damage = damage;
        healthBar = Instantiate(healthBarController.healthBarPrefab, healthBarController.transform);
        healthFillAmount = healthBar.transform.GetChild(0).GetComponentInChildren<Image>();
        currentHealth = Health;
    }

    public void InstantiateHealthbar()
    {
        player = MenuManager.instance.player;
        healthBarControllerGO = MenuManager.instance.healthBarController;

        playerHealthScript = player.GetComponent<PlayerHealth>();
        healthBarController = healthBarControllerGO.GetComponent<HealthBarController>();

        healthBar = Instantiate(healthBarController.healthBarPrefab, healthBarController.transform);
        healthFillAmount = healthBar.transform.GetChild(0).GetComponentInChildren<Image>();
        currentHealth = Health;
    }

    public void UpdateHealthBarPosition()
    {
        healthBarPosition = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + 2, transform.position.z));
        healthBarController.UpdateHealthBarPosition(healthBar, healthBarPosition);
    }

    public virtual void EnemyMovement()
    {
        if (transform.position.x <= leftTarget.position.x)
        {
            direction = 1;
            transform.eulerAngles = Vector3.zero;
        }

        if (transform.position.x >= rightTarget.position.x)
        {
            direction = -1;
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        transform.Translate(transform.right * direction * speed * Time.deltaTime);
    }
}