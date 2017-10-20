using UnityEngine;

public sealed class PlayerJump : MonoBehaviour
{
    public PlayerController controller;

    public float jumpImpulse = 10.0f;

    private void Update()
    {
        if (controller.isGrounded && !controller.isStunned && Input.GetKeyDown(KeyCode.Space))
            controller.playerRigidbody.AddForce(new Vector2(0.0f, jumpImpulse), ForceMode2D.Impulse);
    }
}
