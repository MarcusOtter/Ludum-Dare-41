using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
public class PlayerSpriteController : MonoBehaviour
{
    [SerializeField] private float _idleDelay;

    private Animator _animator;
    private MouseInput _mouseInput;
    private SpriteRenderer _spriteRenderer;
    private PlayerHorizontalMovement _playerHorizontalMovement;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _mouseInput = Camera.main.GetComponent<MouseInput>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerHorizontalMovement = GetComponentInParent<PlayerHorizontalMovement>();
    }

    private void Update()
    {
        _spriteRenderer.flipX = FacingLeft();

        if (_playerHorizontalMovement.PlayerMoving)
        {
            StopAllCoroutines();
            _animator.SetBool("Walking", true);
        }
        else
        {
            StartCoroutine(SetIdle());
        }

        //_animator.SetBool("Walking", _playerHorizontalMovement.PlayerMoving);
    }

    private IEnumerator SetIdle()
    {
        yield return new WaitForSeconds(_idleDelay);
        _animator.SetBool("Walking", false);
    }

    private bool FacingLeft()
    {
        return _mouseInput.MouseWorldPosition.x < transform.position.x;
    }
}
