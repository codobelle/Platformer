using UnityEngine;
 using UnityEngine.EventSystems;
 using UnityEngine.Events;
using UnityEngine.UI;

public class RightClick : MonoBehaviour, IPointerClickHandler {

    public GameObject defaultKeyGameObject;
    private Button defaultKeyButton;
    private string defaultKeyName;

    private void Start()
    {
        defaultKeyButton = defaultKeyGameObject.GetComponent<Button>();
        defaultKeyButton.onClick.AddListener(DefaultKeyValue);
    }

    public void OnPointerClick(PointerEventData eventData)
     {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            defaultKeyName = transform.name;
            defaultKeyGameObject.SetActive(true);
            defaultKeyGameObject.GetComponent<RectTransform>().position = eventData.position;
        }
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            defaultKeyGameObject.SetActive(false);
            defaultKeyName = "";
        }
    }

    public void DefaultKeyValue()
    {
        switch (defaultKeyName)
        {
            case "LeftKey":

                InputManager.KM.LeftKey = KeyCode.LeftArrow;

                GetComponentInChildren<Text>().text = InputManager.KM.LeftKey.ToString();

                PlayerPrefs.SetString("LeftKey", InputManager.KM.LeftKey.ToString());

                break;

            case "RightKey":

                InputManager.KM.RightKey = KeyCode.RightArrow;

                GetComponentInChildren<Text>().text = InputManager.KM.RightKey.ToString();

                PlayerPrefs.SetString("RightKey", InputManager.KM.RightKey.ToString());

                break;

            case "JumpKey":

                InputManager.KM.JumpKey = KeyCode.Space;

                GetComponentInChildren<Text>().text = InputManager.KM.JumpKey.ToString();

                PlayerPrefs.SetString("JumpKey", InputManager.KM.JumpKey.ToString());

                break;

            case "RunKey":

                InputManager.KM.RunKey = KeyCode.LeftShift;

                GetComponentInChildren<Text>().text = InputManager.KM.RunKey.ToString();

                PlayerPrefs.SetString("RunKey", InputManager.KM.RunKey.ToString());

                break;

            case "DashKey":

                InputManager.KM.DashKey = KeyCode.Q;

                GetComponentInChildren<Text>().text = InputManager.KM.DashKey.ToString();

                PlayerPrefs.SetString("DashKey", InputManager.KM.DashKey.ToString());

                break;

            case "AttackKey":

                InputManager.KM.AttackKey = KeyCode.W;

                GetComponentInChildren<Text>().text = InputManager.KM.AttackKey.ToString();

                PlayerPrefs.SetString("AttackKey", InputManager.KM.AttackKey.ToString());

                break;

            case "GroundSmashKey":

                InputManager.KM.GroundSmashKey = KeyCode.S;

                GetComponentInChildren<Text>().text = InputManager.KM.GroundSmashKey.ToString();

                PlayerPrefs.SetString("GroundSmashKey", InputManager.KM.GroundSmashKey.ToString());

                break;
            case "CheckpointKey":

                InputManager.KM.CheckpointKey = KeyCode.A;

                GetComponentInChildren<Text>().text = InputManager.KM.CheckpointKey.ToString();

                PlayerPrefs.SetString("CheckpointKey", InputManager.KM.CheckpointKey.ToString());

                break;

            case "InteractionKey":

                InputManager.KM.InteractionKey = KeyCode.E;

                GetComponentInChildren<Text>().text = InputManager.KM.InteractionKey.ToString();

                PlayerPrefs.SetString("InteractionKey", InputManager.KM.InteractionKey.ToString());

                break;
        }
        defaultKeyGameObject.SetActive(false);
    }
}