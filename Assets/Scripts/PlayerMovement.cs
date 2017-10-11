using UnityEngine;

public sealed class PlayerMovement : MonoBehaviour
{
	public Rigidbody2D playerRigidbody;
	public PhysicsMaterial2D standingMaterial;
	public PhysicsMaterial2D movingMaterial;

	public float moveSpeed = 1.0f;
	public float maxHorizontalVelocity = 10.0f;
	public float jumpImpulse = 10.0f;

	private void Update()
	{
		if( Input.GetButtonDown( "Jump" ) )
			playerRigidbody.AddForce( new Vector2( 0.0f, jumpImpulse ), ForceMode2D.Impulse );
	}

	private void FixedUpdate()
	{
		float moveInput = Input.GetAxis( "Horizontal" );

		if( Mathf.Abs( moveInput ) > Mathf.Epsilon )
		{
			if( Mathf.Abs( playerRigidbody.velocity.x ) < maxHorizontalVelocity )
			{
				float move = moveInput * moveSpeed;
				playerRigidbody.AddForce( new Vector2( move, 0.0f ), ForceMode2D.Force );
			}

			playerRigidbody.sharedMaterial = movingMaterial;
		}
		else
		{
			playerRigidbody.sharedMaterial = standingMaterial;
		}
	}
}
