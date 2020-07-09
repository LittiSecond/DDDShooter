using Geekbrains;
using UnityEngine;

public sealed class UnitMotor : IMotor
{
	private Transform _instance;

	private float _speedMove =10;
	private float _jumpPower = 10;
	private float _gravityForce;
	private Vector2 _input;
	private Vector3 _moveVector;
	private CharacterController _characterController;
	private Transform _head;

	public float XSensitivity = 2f;
	public float YSensitivity = 2f;
	public bool ClampVerticalRotation = true;
	public float MinimumX = -90F;
	public float MaximumX = 90F;
	public bool Smooth;
	public float SmoothTime = 5f;
	private Quaternion _characterTargetRot;
	private Quaternion _cameraTargetRot;

	public UnitMotor(CharacterController instance)
	{
		_instance = instance.transform;
		_characterController = instance;
		_head = Camera.main.transform;

		_characterTargetRot = _instance.localRotation;
		_cameraTargetRot = _head.localRotation;
	}

	public void Move()
	{
		CharecterMove();
		GamingGravity();

		LookRotation(_instance, _head);
	}

	private void CharecterMove()
	{
		if (_characterController.isGrounded)
		{
			_input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
			Vector3 desiredMove = _instance.forward * _input.y + _instance.right * _input.x;
			_moveVector.x = desiredMove.x * _speedMove;
			_moveVector.z = desiredMove.z * _speedMove;
		}

		_moveVector.y = _gravityForce;
		_characterController.Move(_moveVector * Time.deltaTime);
	}

	private void GamingGravity()
	{
		if (!_characterController.isGrounded) _gravityForce -= 30 * Time.deltaTime;
		else _gravityForce = -1;
		if (Input.GetKeyDown(KeyCode.Space) && _characterController.isGrounded) _gravityForce = _jumpPower;
	}

	private void LookRotation(Transform character, Transform camera)
	{
		float yRot = Input.GetAxis("Mouse X") * XSensitivity;
		float xRot = Input.GetAxis("Mouse Y") * YSensitivity;

		_characterTargetRot *= Quaternion.Euler(0f, yRot, 0f);
		_cameraTargetRot *= Quaternion.Euler(-xRot, 0f, 0f);

		if (ClampVerticalRotation)
			_cameraTargetRot = ClampRotationAroundXAxis(_cameraTargetRot);

		if (Smooth)
		{
			character.localRotation = Quaternion.Slerp(character.localRotation, _characterTargetRot,
				SmoothTime * Time.deltaTime);
			camera.localRotation = Quaternion.Slerp(camera.localRotation, _cameraTargetRot,
				SmoothTime * Time.deltaTime);
		}
		else
		{
			character.localRotation = _characterTargetRot;
			camera.localRotation = _cameraTargetRot;
		}
	}

	private Quaternion ClampRotationAroundXAxis(Quaternion q)
	{
		q.x /= q.w;
		q.y /= q.w;
		q.z /= q.w;
		q.w = 1.0f;

		float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

		angleX = Mathf.Clamp(angleX, MinimumX, MaximumX);

		q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

		return q;
	}
}
