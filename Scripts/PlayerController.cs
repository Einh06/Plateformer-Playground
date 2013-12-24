using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
		// button config
	public string moveName = "Horizontal";
	public string jumpName = "Jump";
	public string runName = "Fire1";

	// movement config
	public float gravity = -15f;
	public float runSpeed = 8f;
	public float walkSpeed = 3f;
	public float jumpSpeed = 2f;
	public float groundDamping = 20f; // how fast do we change direction? higher means faster
	public float inAirDamping = 5f;

	private bool isJumping = false;
	[HideInInspector]
	public float
			rawMovementDirection = 1;
	[HideInInspector]
	public float
			normalizedHorizontalSpeed = 0;
	private CharacterController2D _controller;
	private Animator _animator;
	public RaycastHit2D lastControllerColliderHit;
	[HideInInspector]
	public Vector3
			velocity;

	void Awake ()
	{
			_animator = GetComponent<Animator> ();
			_controller = GetComponent<CharacterController2D> ();
			_controller.onControllerCollidedEvent += onControllerCollider;
	}

	void onControllerCollider (RaycastHit2D hit)
	{
			// bail out on plain old ground hits
			if (hit.normal.y == 1f)
					return;
	
			// logs any collider hits
			//Debug.Log( "flags: " + _controller.collisionState + ", hit.normal: " + hit.normal );
	}

	void Update ()
	{
		// grab our current velocity to use as a base for all calculations
		velocity = _controller.velocity;

		if (isJumping && _controller.collisionState.becameGroundedThisFrame) {

			velocity.y = 0;
			isJumping = false;
			_animator.Play ("Idle");

		}

		float axisValue = Input.GetAxis (moveName);
		_animator.SetFloat ("Speed", Mathf.Abs (axisValue));

		if (Mathf.Abs (axisValue) > 0.3f) {
			normalizedHorizontalSpeed = Mathf.Sign (axisValue);

			if (Mathf.Sign (axisValue) != Mathf.Sign (transform.localScale.x))
				transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);

		} else {

			normalizedHorizontalSpeed = 0;
		}


		float refSpeed = walkSpeed;
		if (Input.GetButton (runName)) {
			refSpeed = runSpeed;
		}

		// apply horizontal speed smoothing it
		var smoothedMovementFactor = _controller.isGrounded ? groundDamping : inAirDamping; // how fast do we change direction?
		velocity.x = Mathf.Lerp (velocity.x, normalizedHorizontalSpeed * rawMovementDirection * refSpeed, Time.deltaTime * smoothedMovementFactor);


		if (!isJumping && Input.GetButtonDown (jumpName)) {
			velocity.y = Mathf.Sqrt (2f * jumpSpeed * ((refSpeed) /walkSpeed) * -gravity);
			isJumping = true;
			_animator.Play ("PlayerJumping");
		}

		// apply gravity before moving
		velocity.y += gravity * Time.deltaTime;

		_controller.move (velocity * Time.deltaTime);
	}

}
