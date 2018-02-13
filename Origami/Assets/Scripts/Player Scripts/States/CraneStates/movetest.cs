using UnityEngine;

public class CraneMovingState : State {


	private CharacterController player;

	private float maxSpeed = 10f;
	private float acceleration = 30f;

	public CraneMovingState(Character character) : base(character)
	{
	}

	public override void OnStateEnter()
	{
		MonoBehaviour.print("entering move state");
		player = character.GetComponent<CharacterController>();
	}

	public override void Tick()
	{

		Vector3 direction = ((character.transform.forward * Input.GetAxis("Vertical")) 
			+ (character.transform.right * Input.GetAxis("Horizontal")));
		direction.Normalize();


		if (direction.x > 0) {
			if (character.xSpeed < maxSpeed * direction.x) {
				character.xSpeed += acceleration * Time.deltaTime;
			} else {
				character.xSpeed -= acceleration * Time.deltaTime;
			}
		} else if (direction.x < 0){
			if (character.xSpeed > maxSpeed * direction.x) {
				character.xSpeed -= acceleration * Time.deltaTime;
			} else {
				character.xSpeed += acceleration * Time.deltaTime;
			}
		} else { 
			if (character.xSpeed > 0.1f) {
				character.xSpeed -= acceleration * Time.deltaTime;
			} else if (character.xSpeed < -0.1f) {
				character.xSpeed += acceleration * Time.deltaTime;
			} else {
				character.xSpeed = 0f;
			}
		}


		if (direction.z > 0) {
			if (character.zSpeed < maxSpeed * direction.z) {
				character.zSpeed += acceleration * Time.deltaTime;
			} else {
				character.zSpeed -= acceleration * Time.deltaTime;
			}
		} else if (direction.z < 0){
			if (character.zSpeed > maxSpeed * direction.z) {
				character.zSpeed -= acceleration * Time.deltaTime;
			} else {
				character.zSpeed += acceleration * Time.deltaTime;
			}
		} else { 
			if (character.zSpeed > 0.1f) {
				character.zSpeed -= acceleration * Time.deltaTime;
			} else if (character.zSpeed < -0.1f) {
				character.zSpeed += acceleration * Time.deltaTime;
			} else {
				character.zSpeed = 0f;
			}
		}


		Vector3 moveDirection = new Vector3(character.xSpeed, 0, character.zSpeed);
		player.Move(moveDirection * Time.deltaTime);
		player.Move(Vector3.down * .1f);

		if (!player.isGrounded) {
			character.SetState(new CraneFallingState(character));
		}

		if (Input.GetAxis("Vertical") == 0.0f && Input.GetAxis("Horizontal") == 0.0f)  {
			character.SetState(new CraneIdleState(character));
		}

		if (Input.GetAxis("Jump") != 0.0f) {
			character.SetState(new CraneJumpState(character));
		}
	}

	public override void OnColliderHit(ControllerColliderHit hit)
	{
		Vector3 hitNormal = hit.normal;
		bool isGrounded = (Vector3.Angle(Vector3.up, hitNormal) <= player.slopeLimit);
		if (!isGrounded)
		{

		}
		else
		{
			player.Move(Vector3.up * Time.deltaTime);
			//character.SetState(new CraneIdleState(character));
		}
	}
}
