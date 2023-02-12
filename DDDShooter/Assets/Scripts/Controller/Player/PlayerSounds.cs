using System;
using UnityEngine;
using Geekbrains;


namespace DddShooter
{
    public sealed class PlayerSounds : BaseController, IExecute
    {

        #region Fields

        private CharacterController _characterController;
        private AudioSource _audioSource;
        private PlayerSoundContainer _soundContainer;

        //private Vector3 _horizontalVelocity;

        //private float _horizontalMovementSqrMagnitude;
        private float _timeCounter;
        private float _stepSoundInterval = 0.5f;

        private readonly float _notMoveLimitSqrMagnitude = 0.01f;
        private readonly float _verticalVelocityLimit = 5.0f;

        private bool _isGrounded;


        #endregion


        #region Methods

        private void PlayJumpSound()
        {
            _audioSource.clip = _soundContainer.JumpSound;
            _audioSource.Play();
        }

        private void PlayLandingSound()
        {
            _audioSource.clip = _soundContainer.LandingSound;
            _audioSource.Play();
        }

        private void PlayFootStepSound()
        {
            AudioClip[] clips = _soundContainer.FootstepSounds;
            int n = UnityEngine.Random.Range(0, clips.Length);
            _audioSource.PlayOneShot(clips[n]);
        }

        private void ProgressStepCycle()
        {
            //_timeCounter += Time.deltaTime;
            if (_timeCounter >= _stepSoundInterval)
            {
                PlayFootStepSound();
                _timeCounter = 0;
            }
        }

        public override void On(params BaseObjectScene[] obj)
        {
            if (IsActive) return;
            base.On(obj);

            bool isDataCorrect = false;

            if (obj != null)
            {
                if (obj.Length > 0)
                {
                    PlayerBody body = obj[0] as PlayerBody;
                    if (body != null)
                    {
                        _characterController = body.CharacterController;
                        _audioSource = body.AudioSource;
                        _soundContainer = body.SoundContainer;
                        isDataCorrect = CheckDataIsCorrect();
                    }
                }
            }

            if (!isDataCorrect)
            {
                CustumDebug.Log("PlayerSounds->On: PlayerSounds отключён.");
                Off();
            }
        }

        private bool CheckDataIsCorrect()
        {
            bool isDataCorrect = true;
            if (!_characterController)
            {
                isDataCorrect = false;
            }
            else if (_audioSource == null)
            {
                isDataCorrect = false;
            }
            else if (_soundContainer == null)
            {
                isDataCorrect = false;
            }
            else if (_soundContainer.FootstepSounds == null)
            {
                isDataCorrect = false;
            }

            return isDataCorrect;
        }

        private bool CheckFallForce()
        {
            Vector3 velocity = _characterController.velocity;
            return  velocity.y < -_verticalVelocityLimit;
        }

        private bool CheckJumpForce()
        {
            Vector3 velocity = _characterController.velocity;
            return velocity.y > _verticalVelocityLimit;
        }

        #endregion


        #region IExecute

        public void Execute()
        {
            if (IsActive)
            {
                _timeCounter += Time.deltaTime;
                Vector3 currentHorizontalVelocity = _characterController.velocity;
                currentHorizontalVelocity.y = 0;
                float horizontalMovementSqrMagnitude = currentHorizontalVelocity.sqrMagnitude;
                bool isGrounded = _characterController.isGrounded;

                if (isGrounded)
                {
                    if (!_isGrounded)
                    {
                        if (CheckFallForce())
                        {
                            PlayLandingSound();
                        }
                    }
                    if (horizontalMovementSqrMagnitude > _notMoveLimitSqrMagnitude)
                    {
                        ProgressStepCycle();
                    }
                }
                else
                {
                    if (_isGrounded)
                    {
                        if (CheckJumpForce())
                        {
                            PlayJumpSound();
                        }
                    }
                }

                _isGrounded = isGrounded;
                //_horizontalMovementSqrMagnitude = horizontalMovementSqrMagnitude;
                //_horizontalVelocity = currentHorizontalVelocity;
            }
        }

        #endregion
    }
}
