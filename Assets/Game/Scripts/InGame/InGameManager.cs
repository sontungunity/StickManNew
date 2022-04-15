using STU;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : Singleton<InGameManager>
{
    [SerializeField] private Player player;
    public Player Player => player;
}
