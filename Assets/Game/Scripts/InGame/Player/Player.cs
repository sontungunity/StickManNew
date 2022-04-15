using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerHorizontal))]
[RequireComponent(typeof(PlayerVertical))]
[RequireComponent(typeof(PlayerAnim))]
[RequireComponent(typeof(PlayerAttack))]
public class Player : MonoBehaviour {
    public int Damage => 1;
    public int Hearth => 20;
}
