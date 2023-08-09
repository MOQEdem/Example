using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "StableTransportSetter", menuName = "GameAssets/StableTransportSetter")]
public class StableTransportSetter : ScriptableObject
{
    [SerializeField] private TransportDataPack[] _packs;

    public RidingTransport GetTransport()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;

        for (int i = _packs.Length - 1; i >= 0; i--)
        {
            if (_packs[i].OpeningLevel <= currentLevel)
            {
                return _packs[i].Transport;
            }
        }

        return null;
    }
}

[Serializable]
public class TransportDataPack
{
    [SerializeField] private RidingTransport _transport;
    [SerializeField] private int _openingLevel;

    public RidingTransport Transport => _transport;
    public int OpeningLevel => _openingLevel;
}
