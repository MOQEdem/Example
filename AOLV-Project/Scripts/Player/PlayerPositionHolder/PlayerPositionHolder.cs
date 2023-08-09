using UnityEngine;

public abstract class PlayerPositionHolder : MonoBehaviour
{
   private string _sceneSaver;
   private PlayerPosition _playerPosition;
   
   protected abstract PlayerPosition initpPlayerPosition();
   
   private void OnEnable()
   {
      _sceneSaver = PlayerPrefs.GetString("SceneName");
      _playerPosition = initpPlayerPosition();
      print(_sceneSaver);

      if (_sceneSaver != SceneName.NewHub && _sceneSaver != null)
      {
        _playerPosition.Load();
        transform.position = _playerPosition.Postion;
      }
   }

   private void OnDisable()
   {
      _playerPosition.SavePosition(transform.position);
      _playerPosition.Save();
   }
}