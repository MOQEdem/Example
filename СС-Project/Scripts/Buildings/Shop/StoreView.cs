using UnityEngine;
using TMPro;

public class StoreView : MonoBehaviour
{
    [SerializeField] private TMP_Text _balance;

    public void SetBalance(int balance)
    {
        _balance.text = balance.ToString();
    }
}
