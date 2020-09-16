using System;
using UnityEngine;


namespace DddShooter
{
    [Serializable]
    public sealed class PlayerSoundContainer
    {
        #region Fields

        [SerializeField] private AudioClip[] _footstepSounds;
        [SerializeField] private AudioClip _jumpSound;
        [SerializeField] private AudioClip _landingSound;

        #endregion


        #region Properties

        public AudioClip[] FootstepSounds { get => _footstepSounds; }

        public AudioClip JumpSound { get => _jumpSound; }

        public AudioClip LandingSound { get => _landingSound; }

        #endregion

    }
}
