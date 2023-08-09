using UnityEngine;

public class CapturedNPC : MonoBehaviour
{
    [SerializeField] private CharacterAnimator _animator;
    [SerializeField] private Canvas _message;

    public void StartCelebrate()
    {
        _message.gameObject.SetActive(false);
        //_animator.Victory();
    }
}
