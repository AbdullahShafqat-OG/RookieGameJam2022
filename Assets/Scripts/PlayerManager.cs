using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject camHolder;

    [HideInInspector]
    public PlayerInput playerInput;
    [HideInInspector]
    public PlayerMovement playerMovement;
    [HideInInspector]
    public PlayerCollision playerCollision;

    [SerializeField]
    internal Animator animator;
    public int damageCapability;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
        playerCollision = GetComponent<PlayerCollision>();
    }
}
