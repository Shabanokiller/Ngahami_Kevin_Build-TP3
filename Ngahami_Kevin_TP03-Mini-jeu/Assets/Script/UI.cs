using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public RectTransform BarreEnergie;
    RbCharacterMovements rb;

    public void SetController(RbCharacterMovements _rb)
    {
        rb = _rb;
    }

    void Update()
    {
        RemplissageBar(rb.GetspeedBarreEnergie());
    }

    void RemplissageBar(float rempli)
    {
        BarreEnergie.localScale = new Vector3(rempli, 1f, 1f);
    }
}
