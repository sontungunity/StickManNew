using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    [SerializeField] private int heart;    
    [SerializeField] private int dame;

    public virtual void GetDame(int dame) {
        heart -= dame;
    }
}
