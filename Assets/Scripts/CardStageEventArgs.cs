using UnityEngine;

public class CardStageEventArgs : System.EventArgs
{
  public CardModel cardModel { get; private set; }
  public Vector3 position { get; private set; }
  public bool flip { get; private set; }

  public CardStageEventArgs(CardModel cardModel, Vector3 position, bool flip)
  {
    this.cardModel = cardModel;
    this.position = position;
    this.flip = flip;
  }

}