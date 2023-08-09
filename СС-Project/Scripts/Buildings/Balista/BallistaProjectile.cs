using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BallistaProjectile : MonoBehaviour
{
    [Header("Game Settings")]
    [SerializeField] private int _damage;

    [Space]
    [Header("Animation Settings")]
    [SerializeField] private AnimationCurve _flyCurve;
    [SerializeField][Min(0)] private float _flyHigh = 1;
    [SerializeField][Min(0)] private float _speed = 10;
    [SerializeField] private int _animationTime;

    private List<Character> _hitCharacters = new List<Character>();

    private void Start() =>
        ShowAppearAnimation();

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Character>(out var character))
        {
            if (character.CharacterType == CharacterType.Enemy)
            {
                foreach (var npc in _hitCharacters)
                    if (character == npc)
                        return;

                _hitCharacters.Add(character);
                character.ApplyDamage(_damage);

                if (character is NPCWalker walker)
                    walker.DoHitJump();
            }
        }
    }

    public void MoveTo(Transform target) =>
        StartCoroutine(MoveRoutine(target));

    private IEnumerator MoveRoutine(Transform target)
    {
        Vector3 startPosition = transform.position;
        float moveDuration = Vector3.Distance(target.position, transform.position) / _speed;

        Vector3 endPosition = target.position;

        for (float t = 0; t < 1; t += Time.deltaTime / moveDuration)
        {
            float offsetY = _flyCurve.Evaluate(t) * _flyHigh;
            Vector3 nextPos = Vector3.Lerp(startPosition, endPosition, t) + Vector3.up * offsetY;
            transform.LookAt(nextPos);
            transform.position = nextPos;

            yield return null;
        }

        Die();
    }

    private void ShowAppearAnimation()
    {
        Vector3 defaultPosition = transform.position;
        transform.position = defaultPosition - new Vector3(0f, -0.35f, 0f);
        transform.DOMoveY(defaultPosition.y, 0.16f);
    }

    private void Die() =>
        Destroy(gameObject);
}
