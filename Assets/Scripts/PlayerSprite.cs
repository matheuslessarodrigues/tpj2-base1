using UnityEngine;

public sealed class PlayerSprite : MonoBehaviour
{
	public PlayerMovement playerMovement;
	public SpriteRenderer spriteRenderer;
	public Animator playerAnimator;

	public string onAirAnimationParam = "OnAir";
	public string movingAnimationParam = "Moving";

	public float groundCheckHeight = 1.0f;
	public bool debugIsMoving;
	public float debugX;

	private void Update()
	{
		float horizontalVelocity = playerMovement.playerRigidbody.velocity.x;
		bool isMoving = Mathf.Abs( horizontalVelocity ) > 0.001f;

		if( isMoving )
			spriteRenderer.flipX = horizontalVelocity < 0.0f;

		debugIsMoving = isMoving;
		debugX = horizontalVelocity;

		playerAnimator.SetBool( movingAnimationParam, isMoving );

		int layerMask = LayerMask.GetMask( "Environment" );
		bool isGrounded = Physics2D.Raycast( transform.position + Vector3.up, Vector2.down, groundCheckHeight, layerMask ).collider != null;

		playerAnimator.SetBool( onAirAnimationParam, !isGrounded );
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawRay( transform.position + Vector3.up, Vector2.down * groundCheckHeight );
	}
}
