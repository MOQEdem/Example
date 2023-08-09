using UnityEngine;

public class SplashDamage : MonoBehaviour
{
    [SerializeField] [Min(0)] private int _damage;
    
    private TargetDetector _detector;
    
    private void Awake() => 
        _detector = GetComponentInChildren<TargetDetector>();

    // private void Start() => 
    //     _detector.gameObject.SetActive(false);

    public void MakeDamage()
    {
        // _detector.gameObject.SetActive(true);
        Character[] targets = _detector.Targets.ToArray();

        foreach (Character target in targets) 
            target.ApplyDamage(_damage);
        
        // _detector.gameObject.SetActive(false);
    }
}
