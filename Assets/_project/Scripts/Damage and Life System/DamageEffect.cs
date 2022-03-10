using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffect : MonoBehaviour
{
    public List<MeshRenderer> rend;
    [SerializeField] float cooldownTrans = 0.1f;
    float timer;
    bool activate;
    public bool Activate { set { activate = value; timer = cooldownTrans; rend.ForEach(x => x.enabled = true); } }

    private void Update()
    {
        if (!activate) return;
        timer -= Time.deltaTime;

        if (timer > 0) return;
        rend.ForEach(x => x.enabled = !x.enabled);
        timer = cooldownTrans;
    }
}
