using UnityEngine;

public class CardClicker : MonoBehaviour {

  public event CardClickEventHandler CardClickedEvent; 

  private void Update()
  {
    if (Input.GetMouseButtonDown(0)) {
      RaycastHit hit;
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

      if (Physics.Raycast(ray, out hit, 100f)) {
        if (hit.transform != null) {
          Card card = hit.transform.gameObject.GetComponent<Card>();
          if (card != null && CardClickedEvent != null) {
              CardClickedEvent(this, new CardEventArgs(card.cardModel));
          }
        }
      }
    }
  }
  
}