using UnityEngine;

public class Weapons : MonoBehaviour {

    public GameObject pistolObject, shotgunObject, minigunObject;
    public UIManager uiManager;
    [HideInInspector]
    public bool isInteraction = false;
    private string pistolTag, shotgunTag, minigunTag;
    private GameObject pickableGun;
    private GameObject lastActiveGun;

    // Use this for initialization
    void Start () {

        pistolTag = pistolObject.tag;
        shotgunTag = shotgunObject.tag;
        minigunTag = minigunObject.tag;

    }

    private void Update()
    {
        if (pickableGun != null)
        {
            if (pickableGun.CompareTag(pistolTag) && isInteraction)
            {
                SetActiveTakenGun(pickableGun, pistolObject);
            }

            if (pickableGun.CompareTag(shotgunTag) && isInteraction)
            {
                SetActiveTakenGun(pickableGun, shotgunObject);
            }

            if (pickableGun.gameObject.CompareTag(minigunTag) && isInteraction)
            {
                SetActiveTakenGun(pickableGun, minigunObject);
            }
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        pickableGun = collision.gameObject;
    }

    private void OnTriggerExit(Collider collision)
    {
        pickableGun = null;
    }

    private void SetActiveTakenGun(GameObject pickableGun, GameObject gunObject)
    {
        pistolObject.SetActive(false);
        shotgunObject.SetActive(false);
        minigunObject.SetActive(false);

        gunObject.SetActive(true);

        if (lastActiveGun != null)
        {
            lastActiveGun.SetActive(true);
            lastActiveGun.transform.position = pickableGun.transform.position;
        }
        uiManager.pickableItems.Add(pickableGun.GetComponent<PickableItem>());
        uiManager.pickableItemIsAdded = true;
        pickableGun.SetActive(false);
        lastActiveGun = pickableGun;
    }
}
