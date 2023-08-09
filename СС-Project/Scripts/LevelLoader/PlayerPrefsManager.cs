using UnityEngine;

public class PlayerPrefManager
{
    public void ClearPrefsBetweenLevels()
    {
        PlayerPrefs.DeleteKey(nameof(RangedForge));
        PlayerPrefs.DeleteKey(nameof(MeleeForge));
        PlayerPrefs.DeleteKey(nameof(FirstMeleeBarracks));
        PlayerPrefs.DeleteKey(nameof(SecondMeleeBarracks));
        PlayerPrefs.DeleteKey(nameof(ThirdMeleeBarracks));
        PlayerPrefs.DeleteKey(nameof(FirstRangedBarracks));
        PlayerPrefs.DeleteKey(nameof(SecondRangedBarracks));
        PlayerPrefs.DeleteKey(nameof(ThirdRangedBarracks));
        PlayerPrefs.DeleteKey(nameof(HeavyBarracks));
        PlayerPrefs.DeleteKey(nameof(ResourceExchange));
    }

    public void ClearAllPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
