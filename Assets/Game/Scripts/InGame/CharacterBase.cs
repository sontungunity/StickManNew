using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    public int heart;    
    public int dame;
 
    public virtual void GetDame(int dame) {
        heart -= dame;
    }
}
