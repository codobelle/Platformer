using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [HideInInspector]
    public bool wasExplosion = false;
    [SerializeField]
    private GameObject particleEffectPlayer;
    [SerializeField]
    private GameObject particleEffectEnemy;
    [SerializeField]
    private AudioClip explosion;
    private GameObject gun;
    private Gun gunScript;
    private float range = 1100;
    private Vector3 bulletStartPosition;

    public int Damage { get; set; }

    private void Start()
    {
        particleEffectPlayer = Instantiate(particleEffectPlayer, transform.position, transform.rotation);
        particleEffectEnemy = Instantiate(particleEffectEnemy, transform.position, transform.rotation);
        DisableParticleEffects();
    }

    private void Update()
    {
        if (Vector3.Distance(Camera.main.WorldToScreenPoint(transform.position),
        Camera.main.WorldToScreenPoint(bulletStartPosition)) > range)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        bulletStartPosition = transform.position;
        wasExplosion = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.playerTag) && 
            (CompareTag(Constants.enemyBulletTag) || CompareTag(Constants.wallExplosionBulletTag)))
        {
            other.GetComponent<PlayerHealth>().UpdatePlayerHealth(Damage);
            AudioSource.PlayClipAtPoint(explosion, transform.position, 1);
            particleEffectPlayer.transform.position = transform.position;
            particleEffectPlayer.SetActive(true);
            gameObject.SetActive(false);
            Invoke("DisableParticleEffects", 1f);
        }

        if (other.transform.CompareTag(Constants.groundTag))
        {
            gameObject.SetActive(false);
        }

        if (other.CompareTag(Constants.groundTag) && CompareTag(Constants.wallExplosionBulletTag))
        {
            AudioSource.PlayClipAtPoint(explosion, transform.position, 1);
            particleEffectEnemy.transform.position = transform.position;
            particleEffectEnemy.SetActive(true);
            gameObject.SetActive(false);
            wasExplosion = true;
            Invoke("DisableParticleEffects", .4f);
        }

        if (other.CompareTag(Constants.enemyTag) && CompareTag(Constants.playerBulletTag))
        {
            AudioSource.PlayClipAtPoint(explosion, transform.position, 1);
            particleEffectEnemy.transform.position = transform.position;
            particleEffectEnemy.SetActive(true);
            gameObject.SetActive(false);
            Invoke("DisableParticleEffects", .4f);
        }
    }

    private void DisableParticleEffects()
    {
        particleEffectPlayer.SetActive(false);
        particleEffectEnemy.SetActive(false);
        wasExplosion = false;
    }
    
}
