using UnityEngine;

public class PickableItem : MonoBehaviour {

    public Pickable pickableItem;

    [System.Serializable]
    public struct Pickable
    {
        public int id;
        public Sprite pickableItemSprite;
    }
}
