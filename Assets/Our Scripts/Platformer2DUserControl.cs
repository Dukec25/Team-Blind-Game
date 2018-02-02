using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;
		private bool m_Fire;
		private float ECHO_SPEED = 8;

        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
        }

        private void Update()
        {
            if (!m_Jump)
            {
                // Read the jump input in Update so button presses aren't missed.
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }
			if (!m_Fire)
			{
				// Read the "fire" input in Update so button presses aren't missed.
				m_Fire = CrossPlatformInputManager.GetButtonDown("Fire1");
			}
        }

        private void FixedUpdate()
        {
            // Read the inputs.
            bool crouch = Input.GetKey(KeyCode.LeftControl);
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            // Pass all parameters to the character control script.
            m_Character.Move(h, crouch, m_Jump);

			if (m_Fire) {
				int posOffset;
				float echoVelocity;

				if (m_Character.IsFacingRight ()) {
					posOffset = 1;
					echoVelocity = ECHO_SPEED;
				} else {
					posOffset = -1;
					echoVelocity = -ECHO_SPEED;
				}

				GameObject echo = (GameObject)GameObject.Instantiate (Resources.Load ("Echo"), new Vector2 (m_Character.transform.position.x + posOffset, m_Character.transform.position.y), Quaternion.identity);
				Rigidbody2D m_Rigidbody2D = echo.GetComponent<Rigidbody2D>();
				m_Rigidbody2D.velocity = new Vector2(echoVelocity, m_Rigidbody2D.velocity.y);
			}
            m_Jump = false;
			m_Fire = false;
        }
    }
}
