using System;
using UnityEngine;

//------------------------------------------------------------------------------
[CreateAssetMenu(fileName = "Currency", menuName = "ScriptableObjects/Currency")]
public class Currency : ScriptableObject
{
    //--------------------------------------------------------------------------
    [SerializeField] private int _count = 0;

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
}