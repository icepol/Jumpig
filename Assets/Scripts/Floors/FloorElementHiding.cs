using System;
using pixelook;
using UnityEngine;
using Random = UnityEngine.Random;

public class FloorElementHiding : MonoBehaviour
{
    [SerializeField] private Material visibleMaterial;
    [SerializeField] private Material hiddenMaterial;
    
    [SerializeField] private bool randomStartIsHidden;
    [SerializeField] private int changeStateJumpsCount = 1;

    private BoxCollider _boxCollider;
    private MeshRenderer _meshRenderer;
    
    private int _jumps;
    private bool _isHidden;

    private bool IsHidden
    {
        get => _isHidden;
        set
        {
            _isHidden = value;

            _boxCollider.enabled = !_isHidden;
            _meshRenderer.material = _isHidden ? hiddenMaterial : visibleMaterial;
        }
    }

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    private void OnEnable()
    {
        EventManager.AddListener(Events.PLAYER_JUMP_STARTED, OnPlayerJumpStarted);
    }

    void Start()
    {
        if (randomStartIsHidden)
            IsHidden = Random.Range(0f, 1f) > 0.5f;
    }

    private void OnDisable()
    {
        EventManager.RemoveListener(Events.PLAYER_JUMP_STARTED, OnPlayerJumpStarted);
    }

    private void OnPlayerJumpStarted()
    {
        if (++_jumps < changeStateJumpsCount) return;
        
        _jumps = 0;
        IsHidden = !IsHidden;
    }
}
