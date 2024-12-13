using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    private int _totalCounter = 0;
    private int _counter = 0;
    private int _coins = 0;
    private Action<int> _onCounterChanged;

    public int Counter { 
        get { return _counter; }
        set {
            if (value <= 0) return;
            _counter = value; 
            _onCounterChanged.Invoke(_counter);
        }
    }

    public int Coins
    {
        get { return _coins; }
        set { _coins = value; }
    }

    public event Action<int> OnCounterChanged
    {
        add { _onCounterChanged += value; }
        remove { _onCounterChanged -= value; }
    }
}
