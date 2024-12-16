using System;
using UnityEngine;

//------------------------------------------------------------------------------
[CreateAssetMenu(fileName = "Resource", menuName = "ScriptableObjects/Resource")]
public class Resource : ScriptableObject
{
    //--------------------------------------------------------------------------
    [SerializeField] private int _count = 0;
    [SerializeField] private int _price = 0;
    [SerializeField] private Currency _currency = null;

    private Action<int> _onCountChange;

    public int Count { 
        get => _count; 
        set { _count = value; _onCountChange?.Invoke(_count); } 
    }

    //--------------------------------------------------------------------------
    /*
     * @param int: count
     */
    //--------------------------------------------------------------------------
    public event Action<int> OnCountChange
    {
        add { _onCountChange += value; }
        remove { _onCountChange -= value; }
    }

    //--------------------------------------------------------------------------
    public void Buy(int amount)
    {
        if(_currency.Count >= _price * amount)
        {
            _currency.Count -= _price * amount;
            Count += amount;
        }
    }
}