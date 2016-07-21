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

		// 添加自动寻路，这里保存目标的位置
		private Vector3 targetPosition;
		public Camera mainCamera;
		private float timestamp;
		private bool stop;

		// 自动导航
		public Transform TargetObject;



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

			if (TargetObject != null) {
//				GetComponent<NavMeshAgent> ().destination = TargetObject.position;
			}
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

			float currentTime = Time.time;

			if (Input.GetMouseButton (1) && currentTime - timestamp > 0.5f) {
				
//				stop = true;

				timestamp = currentTime;

				Vector3 screenPosition = Input.mousePosition;
				Debug.Log ("mouse pos" + screenPosition + "deltal time:" + Time.deltaTime + "time:" + Time.time);

				Ray ray =  mainCamera.ScreenPointToRay (screenPosition);
				RaycastHit hit;
				Debug.Log ("eeeaoe");
				if (Physics.Raycast (ray, out hit)) {

					if (hit.collider.gameObject.tag == "Terrain") {

						targetPosition = hit.point;

						transform.LookAt (targetPosition);

						Debug.Log ("target " + targetPosition); 


						GetComponent<NavMeshAgent> ().destination = targetPosition;

//						transform.gameObject.GetComponent<Animator> ().Play ("HumanoidRun", 0);
//						transform.Translate (Vector3.forward * 0.5f);
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

//			Debug.Log ("v" + v + " h" + h);

			// 禁用后退的按钮
			if (v < 0) {
				return;
			}

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


//			if (!stop) {
//				stop = true;
//
//				m_Move = new Vector3 (-1.0f, 0f, 1.0f);
//
//			}

			//			Debug.Log ( "move " + m_Move + " crouch " + crouch + " jump " + m_Jump);
			// pass all parameters to the character control script
			m_Character.Move(m_Move, crouch, m_Jump);
			m_Jump = false;

		}
    }
}
