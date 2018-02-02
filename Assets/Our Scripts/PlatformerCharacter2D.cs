using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class PlatformerCharacter2D : MonoBehaviour
    {
        [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
        [SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
        [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
        [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character
		[SerializeField] private int k_PitchScale;
		[SerializeField] private float k_PanScale;
		[SerializeField] private int k_Width;

        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;            // Whether or not the player is grounded.
        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        private Rigidbody2D m_Rigidbody2D;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.
		private AudioSource m_PinkNoise;
		private AudioSource m_BounceJump;
		private AudioSource m_HeadBump;
		private bool m_isColliding = false;
		private int m_bounceStrength = 1200;

        private void Awake()
        {
            // Setting up references.
            m_GroundCheck = transform.Find("GroundCheck");
            m_Rigidbody2D = GetComponent<Rigidbody2D>();

			AudioSource[] sources = GetComponents<AudioSource>();
			m_PinkNoise = sources [0];
			m_BounceJump = sources [1];
			m_HeadBump = sources [2];

            if (PreferenceManager.Instance.blind) {
                Renderer m_Renderer = GetComponent<Renderer>();
                m_Renderer.enabled = false;
            }
        }

		private void OnCollisionEnter2D (Collision2D collision) {
			if (collision.collider.tag == "Block") {
				Vector3 contactPoint = collision.contacts[0].point;
				Vector3 centerPoint = collision.collider.bounds.center;
				float halfBlockWidth = collision.collider.bounds.extents.x;

				if (contactPoint.y < centerPoint.y && contactPoint.x > (centerPoint.x - halfBlockWidth) && contactPoint.x < (centerPoint.x + halfBlockWidth)) {
					m_HeadBump.Play ();
				}
			}
		}

		private void OnTriggerEnter2D (Collider2D collider) {
			if (m_isColliding) return;
			m_isColliding = true;

			if (collider.tag == "Bounce") {
				m_BounceJump.Play ();
				m_Rigidbody2D.velocity = Vector2.zero;
				m_Rigidbody2D.AddForce (new Vector2 (0, m_bounceStrength));
			}
		}

        private void FixedUpdate()
        {
			m_isColliding = false;
            m_Grounded = false;

            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                    m_Grounded = true;
            }
        }

		private float scalePlayerPitch () {
			return transform.position.y / k_PitchScale + 1;
		}

		private float scalePlayerStereo () {
			return (float) (transform.position.x / k_Width / k_PanScale);
		}

        public void Move(float move, bool crouch, bool jump)
        {

            // Reduce the speed if crouching by the crouchSpeed multiplier
            move = (crouch ? move*m_CrouchSpeed : move);

            // Move the character
            m_Rigidbody2D.velocity = new Vector2(move*m_MaxSpeed, m_Rigidbody2D.velocity.y);

            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
                // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }

            // If the player should jump...
            if (m_Grounded && jump)
            {
                // Add a vertical force to the player.
                m_Grounded = false;
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            }

			m_PinkNoise.panStereo = scalePlayerStereo();
			m_PinkNoise.pitch = scalePlayerPitch ();
        }


        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

		public bool IsFacingRight(){
			return m_FacingRight;
		}
    }
}
