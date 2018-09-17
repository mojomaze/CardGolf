using UnityEngine;

public class CardEventArgs : System.EventArgs
{
  public CardModel cardModel { get; private set; }

  public CardEventArgs(CardModel cardModel)
  {
    this.cardModel = cardModel;
  }

}