using UnityEngine;

public class CardMoveToStackEventArgs : System.EventArgs
{
  public CardModel cardModel { get; private set; }
  public CardStack cardStack { get; private set; }
  public Vector3 position { get; private set; }
  public bool flip { get; private set; }
  public int indexFrom { get; private set; }
  public int indexTo { get; private set; }

  public CardMoveToStackEventArgs(
    CardModel cardModel, 
    CardStack cardStack, 
    Vector3 position, 
    bool flip, 
    int indexFrom,
    int indexTo)
  {
    this.cardModel = cardModel;
    this.cardStack = cardStack;
    this.position = position;
    this.flip = flip;
    this.indexFrom = indexFrom;
    this.indexTo = indexTo;
  }

}