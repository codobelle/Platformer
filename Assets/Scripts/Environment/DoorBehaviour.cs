using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour {
    
    public List<PuzzlePiece> puzzlePieces;
    public PuzzleManager puzzleManager;
    public TextMesh codeText;
    private bool isInventory;

    // Use this for initialization
    void Start () {

        puzzleManager.doorCode = new int[puzzlePieces.Count];
        puzzleManager.createdDoorCode = new int[puzzlePieces.Count];
        for (int i = 0; i < puzzlePieces.Count; i++)
        {
            puzzleManager.doorCode[i] = puzzlePieces[i].puzzlePiece.id;
            codeText.text += puzzleManager.doorCode[i].ToString();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(Constants.playerTag))
        {
            isInventory = false;
            Time.timeScale = 0.0f;
            ShowPuzzleItems(isInventory);
        }
    }

    public void ShowPuzzleItems(bool isInventory)
    {
        puzzleManager.AddPuzzlePiecesInUI();
        puzzleManager.ShowPuzzlePanel(isInventory);
    }
}
