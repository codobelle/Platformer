using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzleDrag : MonoBehaviour, IPointerDownHandler, IDragHandler, IDropHandler
{
    [HideInInspector]
    public int puzzlePieceId;
    private Vector2 initialPuzzleItemPosition;
    private RectTransform puzzleItemRectTransform;
    private RectTransform canvasRectTransform;

    private GameObject[] puzzlePieces;
    private GameObject[] puzzlePlaces;
    private PuzzleManager puzzleManager;
    private GameObject touchedPuzzlePlace;
    private int minDistanceToTarget = 30;

    private void Awake()
    {
        Canvas canvas = GetComponentInParent<Canvas>();
        puzzleItemRectTransform = transform as RectTransform;
        canvasRectTransform = canvas.transform as RectTransform;
    }

    // Use this for initialization
    void Start () {
        initialPuzzleItemPosition = puzzleItemRectTransform.localPosition;
        puzzleManager = GetComponentInParent<PuzzleManager>();
        puzzlePieces = GameObject.FindGameObjectsWithTag("PuzzlePiece");
        puzzlePlaces = GameObject.FindGameObjectsWithTag("PuzzlePlace");
    }

    public void OnPointerDown(PointerEventData data)
    {
        touchedPuzzlePlace = null;
        puzzleItemRectTransform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData data)
    {
        Vector2 localPointerPosition;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRectTransform, data.position, data.pressEventCamera, out localPointerPosition
        ))
        {
            puzzleItemRectTransform.localPosition = localPointerPosition;
        }
    }

    public void OnDrop(PointerEventData data)
    {
        for (int i = 0; i < puzzlePlaces.Length; i++)
        {
            float distance = Vector2.Distance(puzzleItemRectTransform.localPosition,
                puzzlePlaces[i].GetComponent<RectTransform>().localPosition);
            float dis = Vector2.Distance(puzzleItemRectTransform.localPosition,
                puzzlePieces[i].GetComponent<RectTransform>().localPosition);
            if (distance < minDistanceToTarget && dis != distance)
            {
                touchedPuzzlePlace = puzzlePlaces[i];
                puzzleManager.createdDoorCode[i] = puzzlePieceId;
            }
        }

        if (touchedPuzzlePlace != null)
        {
            puzzleItemRectTransform.localPosition = touchedPuzzlePlace.GetComponent<RectTransform>().localPosition;
        }
        else
        {
            puzzleItemRectTransform.localPosition = initialPuzzleItemPosition;
        }
    }

}
