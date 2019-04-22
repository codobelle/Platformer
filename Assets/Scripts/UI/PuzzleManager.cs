using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour {

    public GameObject puzzleItem;
    public GameObject puzzleItemBackground;
    public GameObject puzzlePanel;
    public GameObject puzzlePanelInInventory;
    public GameObject nextLevel;
    public GameObject wrongCodeMessagePanel;
    public GameObject loadingImage;
    public Slider loadingSlider;
    [HideInInspector]
    public List<PuzzlePiece> puzzlePieces;
    [HideInInspector]
    public int[] doorCode;
    [HideInInspector]
    public int[] createdDoorCode;

    [HideInInspector]
    public List<int> newCode;
    private int lastNrOfPuzzlePieces = 0;
    private AsyncOperation async;
    private PuzzleManager puzzleManager;


    public void AddPuzzlePiecesInUI()
    {
        if (puzzlePieces.Count > lastNrOfPuzzlePieces)
        {
            for (int i = lastNrOfPuzzlePieces; i < puzzlePieces.Count; i++)
            {
                AddPuzzlePieceInInventory(puzzlePieces[i], i + 1);
                AddPuzzlePieceToOpenDoor(puzzlePieces[i], i + 1);
            }
            lastNrOfPuzzlePieces = puzzlePieces.Count;
        }
    }
    
    public void ShowPuzzlePanel(bool isInventory)
    {
        if (isInventory)
        {
            puzzlePanelInInventory.SetActive(true);
        }
        else
        {
            puzzlePanel.SetActive(true);
        }
    }

    public void HidePuzzlePanel()
    {
        puzzlePanel.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void AddPuzzlePieceToOpenDoor(PuzzlePiece item, int orderNr)
    {
        GameObject puzzlePieceBackground = Instantiate(puzzleItemBackground, new Vector3(orderNr * 100, 400), Quaternion.identity);
        puzzlePieceBackground.GetComponent<RectTransform>().SetParent(puzzlePanel.GetComponent<RectTransform>());

        GameObject puzzlePiece = Instantiate(puzzleItem, new Vector3(orderNr * 100, 400), Quaternion.identity);
        puzzlePiece.GetComponent<RectTransform>().SetParent(puzzlePanel.GetComponent<RectTransform>());

        puzzlePiece.GetComponent<Image>().sprite = item.puzzlePiece.puzzlePieceSprite;
        puzzlePiece.AddComponent<PuzzleDrag>();
        puzzlePiece.GetComponent<PuzzleDrag>().puzzlePieceId = item.puzzlePiece.id;
        puzzlePiece.tag = "PuzzlePiece";

        GameObject puzzlePiecePlace = Instantiate(puzzleItemBackground, new Vector3(orderNr * 100, 200), Quaternion.identity);
        puzzlePiecePlace.GetComponent<RectTransform>().SetParent(puzzlePanel.GetComponent<RectTransform>());
        puzzlePiecePlace.tag = "PuzzlePlace";
    }

    public void AddPuzzlePieceInInventory(PuzzlePiece item, int orderNr)
    {
        GameObject puzzlePiece = Instantiate(puzzleItem, new Vector3(orderNr * 100, 400), Quaternion.identity);
        puzzlePiece.GetComponent<RectTransform>().SetParent(puzzlePanelInInventory.GetComponent<RectTransform>());

        puzzlePiece.GetComponent<Image>().sprite = item.puzzlePiece.puzzlePieceSprite;
    }

    public void CheckCode()
    {
        if (doorCode == null || doorCode.Length == 0)
        {
            ShowFinalLevelPanel();
        }
        else if (createdDoorCode != null)
        {
            if (createdDoorCode.SequenceEqual(doorCode))
            {
                ShowFinalLevelPanel();
            }
            else
            {
                wrongCodeMessagePanel.SetActive(true);
            }
        }
        else
        {
            wrongCodeMessagePanel.SetActive(true);
            wrongCodeMessagePanel.transform.SetAsLastSibling();
        }
    }


    public void ClosePuzzleWrongMessagePanel()
    {
        wrongCodeMessagePanel.SetActive(false);
    }

    public void LoadNextScene()
    {
        Time.timeScale = 1.0f;
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        loadingImage.SetActive(true);
        StartCoroutine(LoadLevelWithBar(nextSceneIndex));
    }

    public void ShowFinalLevelPanel()
    {
        puzzlePanel.SetActive(false);
        nextLevel.SetActive(true);
    }

    IEnumerator LoadLevelWithBar(int level)
    {
        async = SceneManager.LoadSceneAsync(level);
        while (!async.isDone)
        {
            loadingSlider.value = async.progress;
            yield return null;
        }
    }
}
