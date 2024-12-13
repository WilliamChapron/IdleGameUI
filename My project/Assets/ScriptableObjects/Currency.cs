using UnityEngine;

//------------------------------------------------------------------------------
[CreateAssetMenu(fileName = "Currency", menuName = "ScriptableObjects/Currency")]
public class Currency : ScriptableObject
{
    //--------------------------------------------------------------------------
    [SerializeField] private int _count = 0;

    public int Count { get; set; }
}