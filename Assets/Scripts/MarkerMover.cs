using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerMover : MonoBehaviour {

  public void moveToPlayer(int player)
  {
    Vector3 position;
    switch(player) {
      case 0:
        position = new Vector3(0f, 0f, -2f);
        break;

      case 1:
        position = new Vector3(-3f, 0f);
        break;

      case 2:
        position = new Vector3(0f, 0f, 2f);
        break;

      default:
        position = new Vector3(3f, 0f);
        break;
    }
    transform.position = position;
  }

}