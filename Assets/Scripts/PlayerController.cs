using UnityEngine;

public sealed class PlayerController : MonoBehaviour
{
	public Rigidbody2D playerRigidbody;

	public float moveAcceleration = 1.0f;
	public float maxHorizontalVelocity = 10.0f;
	public float jumpImpulse = 10.0f;

	public float frictionVelocity = 1.0f;

	public float groundCheckHeight = 1.0f;

	[System.NonSerialized]
	public bool isGrounded;
	[System.NonSerialized]
	public bool isDucking;

	private void Update()
	{
		if( isGrounded && Input.GetButtonDown( "Jump" ) )
			playerRigidbody.AddForce( new Vector2( 0.0f, jumpImpulse ), ForceMode2D.Impulse );

		isDucking = isGrounded && Input.GetAxisRaw( "Vertical" ) < -0.5f;
	}

	private void FixedUpdate()
	{
		int layerMask = LayerMask.GetMask( "Environment" );
		isGrounded = Physics2D.Raycast( transform.position + Vector3.up, Vector2.down, groundCheckHeight, layerMask ).collider != null;

		float horizontalInput = Input.GetAxis( "Horizontal" );
		if( !isDucking && Mathf.Abs( horizontalInput ) > Mathf.Epsilon )
		{
			if( Mathf.Sign( horizontalInput ) * playerRigidbody.velocity.x < maxHorizontalVelocity )
			{
				float move = horizontalInput * moveAcceleration;
				playerRigidbody.AddForce( new Vector2( move, 0.0f ), ForceMode2D.Force );
			}
		}
	}

	private void OnCollisionStay2D( Collision2D collision )
	{
		float horizontalInput = Input.GetAxis( "Horizontal" );
		if( isDucking || Mathf.Abs( horizontalInput ) < Mathf.Epsilon )
		{
			Vector2 vel = playerRigidbody.velocity;
			vel.x *= frictionVelocity * Time.deltaTime;
			playerRigidbody.velocity = vel;
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawRay( transform.position + Vector3.up, Vector2.down * groundCheckHeight );
	}
}
