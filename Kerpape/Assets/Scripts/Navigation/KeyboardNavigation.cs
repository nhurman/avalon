using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class KeyboardNavigation : MonoBehaviour
{
	public bool useKeyboard = false;	//If turned to false the FicedUpdate function doesn't execute
										//Must be turned on only when Navigation is set to none in VRManager

	public float moveSpeed = 3.0F;
	public float rotateSpeed = 3.0F;

	void FixedUpdate ()
	{
		// Cache the inputs.
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		//get the character controller
		CharacterController controller = GetComponent<CharacterController>();

		transform.Rotate (0, h * rotateSpeed, 0);
		Vector3 forward = transform.TransformDirection (Vector3.forward);
		float curSpeed = moveSpeed * v;

		controller.SimpleMove (forward * curSpeed);


		//MovementManagement(h, v);
	}

}