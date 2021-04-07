using System.Collections;
using Muramasa.Movement;
using Muramasa.Utilities;
using UnityEngine;


namespace Muramasa.Player
{
    public class ActorAnimationController : MonoBehaviour
    {
        private Animator _animatorController;

        private float attackTime;

        private void Awake()
        {
            _animatorController = GetComponent<Animator>();
        }

        private void Start()
        {
            UpdateAnimClipTimes();
        }

        private void UpdateAnimClipTimes()
        {
            var clips = _animatorController.runtimeAnimatorController.animationClips;

            foreach (AnimationClip clip in clips)
            {
                switch (clip.name)
                {
                    case "Attack":
                        attackTime = clip.length;
                        break;
                }
            }
        }

        public void SetForwardVelocity(float value)
        {
            _animatorController.SetFloat(GLOBALS.FORWARD_VELOCITY, value);
        }

        public void PlayAttack(RigidbodyMovement attackEnded)
        {
            // _animatorController.Play(GLOBALS.ATTACK_ANIMATION);
            StartCoroutine(AnimatorSetAttack(attackEnded));
        }
        
        private IEnumerator AnimatorSetAttack(RigidbodyMovement attackEnded)
        {
            _animatorController.Play(GLOBALS.ATTACK_ANIMATION);

            yield return new WaitForSeconds(attackTime);

            attackEnded.CanMove = true;
        }
    }
}