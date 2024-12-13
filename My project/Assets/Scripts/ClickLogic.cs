using UnityEngine;

//------------------------------------------------------------------------------
public class ClickLogic : MonoBehaviour
{
    //--------------------------------------------------------------------------
    [SerializeField] private int _perClick = 1;
    [SerializeField] private Product _product;

    //--------------------------------------------------------------------------
    public int PerClick
    {
        get { return _perClick; }
        set { _perClick = value; }
    }

    //--------------------------------------------------------------------------
    public void OnClick()
    {
        _product.Produce(_perClick);
    }
}
