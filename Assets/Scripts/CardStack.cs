using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardStack : MonoBehaviour {
  private List<CardModel> cardModels;

  public event CardAddedEventHandler CardAddedEvent;
  public event CardRemovedEventHandler CardRemovedEvent;
  public event CardFlipEventHandler CardFlipEvent;
  public event CardStageEventHandler CardStageEvent;
  public event CardMoveToStackEventHandler CardMoveToStackEvent;

  public void setCardModels(List<CardModel> cardModels) {
    this.cardModels = cardModels;
  }

  public bool HasCard(CardModel cardModel) {
    return cardModels.Contains(cardModel);
  }

  public int IndexOf(CardModel cardModel) {
    return cardModels.IndexOf(cardModel);
  }

  public void Push(CardModel cardModel, int index = -1) 
  {
    if (cardModels == null) {
      cardModels = new List<CardModel>();
    }
    
    if (index != -1 && index < cardModels.Count) {
      cardModels.Insert(index, cardModel);
    } else {
      cardModels.Add(cardModel);
    }
    if (CardAddedEvent != null) {
      CardAddedEvent(this, new CardEventArgs(cardModel));
    }
  }

  public void Insert(CardModel cardModel, int index) {
    Push(cardModel, index);
  }

  public CardModel Pop(int index = -1)
  {
    index = index > -1 && index < cardModels.Count ? index : cardModels.Count - 1;
    CardModel cardModel = cardModels[index];
    cardModels.RemoveAt(index);

    if (CardRemovedEvent != null) {
      CardRemovedEvent(this, new CardEventArgs(cardModel));
    }
    return cardModel;
  }

  public void ShuffleCards() 
  {
    int n = cardModels.Count;
		while (n > 1) {
			n--;
			int k = Random.Range (0, n + 1);
			CardModel temp = cardModels[k];
			cardModels [k] = cardModels [n];
			cardModels [n] = temp;
		}

    cardModels.Reverse();
  }

  public int CardCount 
  {
		get {
			if (cardModels == null) {
				return 0;
			} else {
				return cardModels.Count;
			}
		}
	}

  public void flipCards(RangeInt range, bool faceUp)
  {
    if (range.start > cardModels.Count || range.end > cardModels.Count) {
      return;
    }

    for (int i = range.start; i < range.end; i++) {
      if (CardFlipEvent != null) {
        CardFlipEvent(this, new CardEventArgs(cardModels[i]));
      } else {
        cardModels[i].isFaceUp = faceUp;
      }
    }
  }

  public void StageCard(CardModel cardModel, Vector3 position, bool flip)
  {
    if (cardModels.Contains(cardModel) && CardStageEvent != null) {
      CardStageEvent(this, new CardStageEventArgs(cardModel, position, flip));
    }
  }

  public void MoveCardToStack(CardMoveToStackEventArgs e)
  {
    if (cardModels.Contains(e.cardModel) && CardMoveToStackEvent != null) {
      CardMoveToStackEvent(this, e);
    }
  }

  public void finishedMovingToStack(CardMoveToStackEventArgs e)
  {
    if (cardModels.Contains(e.cardModel)) {
      e.cardStack.Insert(Pop(e.indexFrom), e.indexTo);
    }
  }

	public IEnumerable<CardModel> GetCards()
  {
		foreach (CardModel cardModel in cardModels) {
			yield return cardModel;
		}
	}
}