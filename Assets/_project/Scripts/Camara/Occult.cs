using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Occult : MonoBehaviour
{
    Material mat;

    readonly float lowAlpha = .4f;
    readonly float totalTime = .4f;

    [SerializeField] bool test;

    // Start is called before the first frame update

    private void Awake() 
        => mat = GetComponent<Renderer>().material;
    
    public void SetOccult(bool occult)
    {
        StopAllCoroutines();
        StartCoroutine(SetToAlpha());
        IEnumerator SetToAlpha()
        {
            float to = occult ? lowAlpha : 1;
            float prev = mat.GetFloat("_Occult");
            float time = totalTime;
            while(mat.GetFloat("_Occult") != to)
            {
                time -= Time.deltaTime;
                mat.SetFloat("_Occult", Mathf.Lerp(to, prev, time / totalTime));
                yield return null;
            }
        }
    }

    private void OnValidate()
    {
        //SetOccult(test);
    }
}
