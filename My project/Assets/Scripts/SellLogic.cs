using System.Collections;
using UnityEngine;

//------------------------------------------------------------------------------
public class SellLogic : MonoBehaviour
{
    //--------------------------------------------------------------------------
    [SerializeField] private Product _product;

    //--------------------------------------------------------------------------
    void Start()
    {
        // Workaround to compute the initial salesPerSecond and demand.
        _product.AdjustPrice(_product.SellPrice);

        StartCoroutine(Sell(1));
    }

    //--------------------------------------------------------------------------
    IEnumerator Sell(int intervalInSeconds)
    {
        while (true)
        {
            yield return new WaitForSeconds(intervalInSeconds);
            _product.Sell(intervalInSeconds);
        }
    }
}