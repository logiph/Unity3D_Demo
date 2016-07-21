using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class ThirdPersonUserControl : MonoBehaviour
    {
        private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        private Transform m_Cam;                  // A reference to the main camera in the scenes transform
        private Vector3 m_CamForward;             // The current forward direction of the camera
        private Vector3 m_Move;
        private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.

		private Vector3 targetPosition;

		public Camera mainCamera;
		private Time timestamp;
		private bool stop;

        private void Start()
        {
            // get the transform of the main camera
            if (Camera.main != null)
            {
                m_Cam = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.");
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
            }

            // get the third person character ( this should never be null due to require component )
            m_Character = GetComponent<ThirdPersonCharacter>();

			stop = false;
        }


        private void Update()
        {
            if (!m_Jump)
            {
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }


			if (stop) {
				return;
			}

			if (Input.GetMouseButton (1)) {

				stop = true;

				Vector3 screenPosition = Input.mousePosition;
				Debug.Log ("mouse pos" + screenPosition);

				Ray ray =  mainCamera.ScreenPointToRay (screenPosition);
				RaycastHit hit;
				Debug.Log ("eeeaoe");
				if (Physics.Raycast (ray, out hit)) {

					if (hit.collider.gameObject.tag == "Terrain") {

						targetPosition = hit.point;

						transform.LookAt (targetPosition);

						Debug.Log ("target " + targetPosition); 

						transform.gameObject.GetComponent<Animator> ().Play ("HumanoidRun", 0);
						transform.Translate (Vector3.forward * 0.5f);
					}

				}

			}
		}


        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            // read inputs
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
            bool crouch = Input.GetKey(KeyCode.C);

            // calculate move direction to pass to character
            if (m_Cam != null)
            {
                // calculate camera relative direction to move:
                m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
                m_Move = v*m_CamForward + h*m_Cam.right;
            }
            else
            {
                // we use world-relative directions in the case of no main camera
                m_Move = v*Vector3.forward + h*Vector3.right;
            }
#if !MOBILE_INPUT
			// walk speed multiplier
	        if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;
#endif
			if (m_Move.x == 0 && m_Move.y == 0 && m_Move.z == 0 ) {
//				Debug.Log("idle");
			}


//			Debug.Log ( "move " + m_Move + " crouch " + crouch + " jump " + m_Jump);
            // pass all parameters to the character control script
            m_Character.Move(m_Move, crouch, m_Jump);
            m_Jump = false;
        }
    }
}