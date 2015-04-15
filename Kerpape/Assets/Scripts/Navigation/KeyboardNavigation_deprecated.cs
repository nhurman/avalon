using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class KeyboardNavigation_deprecated : MonoBehaviour
{
	//If turned to false the FicedUpdate function doesn't execute
	//Must be turned on only when Navigation is set to none in VRManager
	public bool useKeyboard = false;	
	public float moveSpeed = 5.0F;
	public float rotateSpeed = 3.0F;

	void FixedUpdate ()
	{
		if (useKeyboard) {
			// Cache the inputs.
			float h = Input.GetAxisRaw ("Horizontal");
			float v = Input.GetAxisRaw ("Vertical");

			//get the character controller
			CharacterController controller = GetComponent<CharacterController> ();

			Vector3 move = new Vector3(h, 0, v);
			move = transform.TransformDirection(move);
			move*=moveSpeed;
			controller.Move(move*Time.deltaTime);
		}
	}
}



/*[RequireComponent(typeof(CharacterController))]
public class KeyboardNavigation : MonoBehaviour
{
	//If turned to false the FicedUpdate function doesn't execute
	//Must be turned on only when Navigation is set to none in VRManager
	public bool useKeyboard = false;	
	
	//If set to true, Left and Right make us strafe
	//If false Left and Right make rotate the head
	public bool strafe = true;
	
	public float moveSpeed = 3.0F;
	public float rotateSpeed = 3.0F;

	void FixedUpdate ()
	{
		if (useKeyboard) {
			// Cache the inputs.
			float h = Input.GetAxisRaw ("Horizontal");
			float v = Input.GetAxisRaw ("Vertical");

			//get the character controller
			CharacterController controller = GetComponent<CharacterController> ();
			
			//no rotation, we make a translation
			if(strafe)
			{
				curSpeed = moveSpeed * h;
				//movment to the right
				if(h>0)
				{
					Vector3 right = transform.TransformDirection(Vector3.right);
					controller.SimpleMove (right * curSpeed);
				}
				//movment to the left
				else
				{
					Vector3 left = transform.TransformDirection(Vector3.left);
					controller.SimpleMove (left * curSpeed);
				}

			}
			//rotation
			else
			{
				transform.Rotate (0, h * rotateSpeed, 0);
			}

			//Vector3.forward is the (0,0,1) vector
			//TransformDirection transforms forward from local space to global space (???)
			Vector3 forward = transform.TransformDirection (Vector3.forward);
			//calculate the speed with regard to the vertical imput
			curSpeed = moveSpeed * v;
			//apply corresponding amount of movment
			controller.SimpleMove (forward * curSpeed);

		}
	}

}*/