using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conditional : MonoBehaviour
{
    [Tooltip("Only Starting Value and Debugging")]
    [SerializeField] int value;
    public int Value { get { return value; } }

    public void Set(int value) 
        => this.value = value;
    public void Add(int value)
        => this.value += value;
}