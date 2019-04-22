using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CustomInputManager : MonoBehaviour
{
    public Transform controlsPanel;
    private KeyboardManager KM;
    private Event keyEvent;
    private Text buttonText;
    private KeyCode newKey;

    private bool waitingForKey;

    private void Awake()
    {
        KM = new KeyboardManager
        {
            LeftKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("LeftKey", "LeftArrow")),
            RightKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("RightKey", "RightArrow")),
            JumpKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("JumpKey", "Space")),
            RunKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("RunKey", "LeftShift")),
            DashKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("DashKey", "Q")),
            AttackKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("AttackKey", "W")),
            GroundSmashKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("GroundSmashKey", "S")),
            CheckpointKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("CheckpointKey", "A")),
            InteractionKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("InteractionKey", "E")),

        };
        InputManager.KM = KM;
    }

    private void Start(){
        
        
        controlsPanel.gameObject.SetActive(false);

        waitingForKey = false;
        for (int i = 0; i < controlsPanel.childCount; i++)
        {
            if (controlsPanel.GetChild(i).name == "LeftKey")
                controlsPanel.GetChild(i).GetComponentInChildren<Text>().text = KM.LeftKey.ToString();
            else if (controlsPanel.GetChild(i).name == "RightKey")
                controlsPanel.GetChild(i).GetComponentInChildren<Text>().text = KM.RightKey.ToString();
            else if (controlsPanel.GetChild(i).name == "JumpKey")
                controlsPanel.GetChild(i).GetComponentInChildren<Text>().text = KM.JumpKey.ToString();
            else if (controlsPanel.GetChild(i).name == "RunKey")
                controlsPanel.GetChild(i).GetComponentInChildren<Text>().text = KM.RunKey.ToString();
            else if (controlsPanel.GetChild(i).name == "DashKey")
                controlsPanel.GetChild(i).GetComponentInChildren<Text>().text = KM.DashKey.ToString();
            else if (controlsPanel.GetChild(i).name == "AttackKey")
                controlsPanel.GetChild(i).GetComponentInChildren<Text>().text = KM.AttackKey.ToString();
            else if (controlsPanel.GetChild(i).name == "GroundSmashKey")
                controlsPanel.GetChild(i).GetComponentInChildren<Text>().text = KM.GroundSmashKey.ToString();
            else if (controlsPanel.GetChild(i).name == "CheckpointKey")
                controlsPanel.GetChild(i).GetComponentInChildren<Text>().text = KM.CheckpointKey.ToString();
            else if (controlsPanel.GetChild(i).name == "InteractionKey")
                controlsPanel.GetChild(i).GetComponentInChildren<Text>().text = KM.InteractionKey.ToString();
        }

    }

    void OnGUI()
    {
        keyEvent = Event.current;
        
        if (keyEvent.isKey && waitingForKey)
        {
            newKey = keyEvent.keyCode;
            waitingForKey = false;
        }
    }
    
    public void StartAssignment(string keyName)
    {
        if (!waitingForKey)
            StartCoroutine(AssignKey(keyName));
    }

    public void SendText(Text text)
    {
        buttonText = text;
    }

    public IEnumerator AssignKey(string keyName)
    {
        waitingForKey = true;
        
        yield return WaitForKey(); //Executes endlessly until user presses a key
        
        switch (keyName)

        {
            case "left":

                KM.LeftKey = newKey; //set left to new keycode

                buttonText.text = KM.LeftKey.ToString(); //set button text to new key

                PlayerPrefs.SetString("LeftKey", KM.LeftKey.ToString()); //save new key to playerprefs

                break;

            case "right":

                KM.RightKey = newKey; //set right to new keycode

                buttonText.text = KM.RightKey.ToString(); //set button text to new key

                PlayerPrefs.SetString("RightKey", KM.RightKey.ToString()); //save new key to playerprefs

                break;

            case "jump":

                KM.JumpKey = newKey; //set jump to new keycode

                buttonText.text = KM.JumpKey.ToString(); //set button text to new key

                PlayerPrefs.SetString("JumpKey", KM.JumpKey.ToString()); //save new key to playerprefs

                break;
            case "run":

                KM.RunKey = newKey; //set jump to new keycode

                buttonText.text = KM.RunKey.ToString(); //set button text to new key

                PlayerPrefs.SetString("RunKey", KM.RunKey.ToString()); //save new key to playerprefs

                break;
            case "dash":

                KM.DashKey = newKey; //set jump to new keycode

                buttonText.text = KM.DashKey.ToString(); //set button text to new key

                PlayerPrefs.SetString("DashKey", KM.DashKey.ToString()); //save new key to playerprefs

                break;
            case "attack":

                KM.AttackKey = newKey; //set jump to new keycode

                buttonText.text = KM.AttackKey.ToString(); //set button text to new key

                PlayerPrefs.SetString("AttackKey", KM.AttackKey.ToString()); //save new key to playerprefs

                break;
            case "groundsmash":

                KM.GroundSmashKey = newKey; //set jump to new keycode

                buttonText.text = KM.GroundSmashKey.ToString(); //set button text to new key

                PlayerPrefs.SetString("GroundSmashKey", KM.GroundSmashKey.ToString()); //save new key to playerprefs

                break;
            case "checkpoint":

                KM.CheckpointKey = newKey; //set jump to new keycode

                buttonText.text = KM.CheckpointKey.ToString(); //set button text to new key

                PlayerPrefs.SetString("CheckpointKey", KM.CheckpointKey.ToString()); //save new key to playerprefs

                break;
            case "interaction":

                KM.InteractionKey = newKey; //set jump to new keycode

                buttonText.text = KM.InteractionKey.ToString(); //set button text to new key

                PlayerPrefs.SetString("InteractionKey", KM.InteractionKey.ToString()); //save new key to playerprefs

                break;
        }
        yield return null;
    }

    public void ClosePanel() {
        controlsPanel.gameObject.SetActive(false);
    }

    private IEnumerator WaitForKey()
    {
        while (!keyEvent.isKey)
            yield return null;
    }
}