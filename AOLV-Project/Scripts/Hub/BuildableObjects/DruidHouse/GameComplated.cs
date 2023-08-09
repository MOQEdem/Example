using Agava.YandexMetrica;
using UnityEngine;

public class GameComplated : MonoBehaviour
{
   private BuildableObject _buildableObject;

   private void OnEnable()
   {
      _buildableObject = GetComponent<BuildableObject>();
      _buildableObject.Builded += GameCompleted;
   }

   private void OnDisable()
   {
      _buildableObject.Builded -= GameCompleted;
   }

   private void GameCompleted()
   {
      YandexMetrica.Send("gameCompleted");
   }
}
