using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyOccult : MonoBehaviour
{
    Occult currentOccult;

    private void Update()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        Ray ray = Camera.main.ScreenPointToRay(pos);
        Physics.Raycast(ray, out RaycastHit hit);

        if (!hit.transform.TryGetComponent(out Occult newOccult))
        {
            if (currentOccult != null)
            {
                currentOccult.SetOccult(false);
                currentOccult = null;
            }
            return;
        }

        if (newOccult == currentOccult) return;

        newOccult.SetOccult(true);
        currentOccult = newOccult;
    }
}