using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject inventoryPanel, pickablePanel;
    [SerializeField]
    private GameObject mainMenuPanel;
    [SerializeField]
    private GameObject controlsPanel;
    [HideInInspector]
    public List<PickableItem> pickableItems;
    [HideInInspector]
    public bool pickableItemIsAdded = false;

    private void Start()
    {
        Time.timeScale = 0f;
        mainMenuPanel.SetActive(true);
    }

    public void Play()
    {
        mainMenuPanel.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void Settings()
    {
        Time.timeScale = 0f;
        settingsPanel.transform.SetAsLastSibling();
        settingsPanel.SetActive(true);
    }

    public void ShowInventory()
    {
        Time.timeScale = 0f;
        if (pickableItemIsAdded)
        {
            pickableItemIsAdded = false;
            foreach (var item in pickableItems)
            {
                AddPickableItemsInInventory(item);
            }
        }
        inventoryPanel.SetActive(true);
    }
    
    public void HideInventory()
    {
        Time.timeScale = 1.0f;
        inventoryPanel.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        settingsPanel.SetActive(false);
        if (!mainMenuPanel.activeSelf)
        {
            mainMenuPanel.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        settingsPanel.SetActive(false);

        if (mainMenuPanel.activeSelf)
        {
            mainMenuPanel.SetActive(false);
        }
        Time.timeScale = 1.0f;
    }

    public void Controls()
    {
        controlsPanel.SetActive(true);
    }
    
    public void AddPickableItemsInInventory(PickableItem item)
    {
        GameObject pickableItem = new GameObject();
        pickableItem.name = "Item" + item.pickableItem.id;
        pickableItem.transform.parent = pickablePanel.transform;
        pickableItem.AddComponent<RectTransform>();
        pickableItem.AddComponent<Image>();
        pickableItem.GetComponent<Image>().sprite = item.pickableItem.pickableItemSprite;
    }
}
