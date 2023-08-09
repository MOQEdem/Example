using UnityEngine;

public class KeyboardInput : MonoBehaviour
{
   public Vector2 SetInputDirection()
   {
      Vector2 direction = new Vector2();
      direction.x = Input.GetAxis("Horizontal");
      direction.y = Input.GetAxis("Vertical");
      return direction;
   }
}