using System.Collections;
using UnityEngine;

//------------------------------------------------------------------------------
public class IdleLogic : MonoBehaviour
{
    //--------------------------------------------------------------------------
    [SerializeField] private Product _product;

    //--------------------------------------------------------------------------
    void Start()
    {
        // Workaround to compute the initial salesPerSecond and demand.
        _product.AdjustPrice(_product.SellPrice);

        StartCoroutine(Produce(1));
    }

    //--------------------------------------------------------------------------
    IEnumerator Produce(int intervalInSeconds)
    {
        while (true)
        {
            yield return new WaitForSeconds(intervalInSeconds);
            _product.IdleProduce(intervalInSeconds);
        }
    }
}