using UnityEngine;

public class RecyclerAnimation : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] _particles;
    [SerializeField] private Animator _animator;

    public void StartAimation()
    {
        _animator.SetBool(AnimatorRecycler.Bool.IsWorking, true);
    }

    public void StopAnimation()
    {
        _animator.SetBool(AnimatorRecycler.Bool.IsWorking, false);
    }

    public void OnEventHappend()
    {
        foreach (var particle in _particles)
            particle.Play();
    }

    public static class AnimatorRecycler
    {
        public static class Bool
        {
            public const string IsWorking = nameof(IsWorking);
        }
    }
}
