using UnityEngine;

public class PuzzlePiece : MonoBehaviour {

    public Piece puzzlePiece;
    private int minDistance = 2;
    private GameObject player;

    [System.Serializable]
    public struct Piece
    {
        public int id;
        public Sprite puzzlePieceSprite;
    }

    private void Start()
    {
        player = MenuManager.instance.player;
    }

    private void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < minDistance)
        {
            transform.position = Vector3.Lerp(player.transform.position, transform.position, 0.9f);
        }
    }
}
