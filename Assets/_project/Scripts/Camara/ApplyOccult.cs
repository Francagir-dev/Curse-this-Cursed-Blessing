using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamApplyOccult : MonoBehaviour
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

        newOccult.SetOccult(true);

        if (currentOccult != newOccult)
        {
            if (currentOccult != null)
                currentOccult.SetOccult(false);
            currentOccult = newOccult;
        }
    }
}
