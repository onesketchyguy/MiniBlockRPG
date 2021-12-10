using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// FIXME: UNOPTIMIZED

public class AttackAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator = null;
    private AnimatorOverrideController controller = null;
    [SerializeField] private AnimationClip[] animations = null;
    [SerializeField] private string overrideClipName = "Attack";
    private int overrideIndex = -1;
    private int currentAnim = 0;
    private float lastAttack = 0.0f;

    [Range(0.0f, 1.0f)]
    [SerializeField] private float attackAnimLength = 0.75f;
    [SerializeField] private string attackString = "Attack";
    private int _animAttackID = -1;
    private float _attackAnimWeight = 0.0f;

    [SerializeField] private int _animCombatLayer = 1;
    [SerializeField] private float _animLayerSpeed = 1.0f;

    [SerializeField] private StarterAssets.StarterAssetsInputs _inputs = null;

    private bool attackLeft = false;

    private void SwapClip()
    {
        if (Time.timeSinceLevelLoad - lastAttack > 5.0f ||
            currentAnim + 1 >= animations.Length)
        {
            currentAnim = 0;
        }
        else
        {
            currentAnim++;
        }

        if (overrideIndex == -1)
        {
            controller = new AnimatorOverrideController(animator.runtimeAnimatorController);            

            for (int i = 0; i < controller.animationClips.Length; i++)
            {
                AnimationClip item = animator.runtimeAnimatorController.animationClips[i];
                if (item.name == overrideClipName)
                {
                    overrideIndex = i;
                    break;
                }
            }

            animator.runtimeAnimatorController = controller;
        }

        controller[overrideClipName] = animations[currentAnim];
    }

    private void Update()
    {
        if (_inputs.fireLeft && !attackLeft)
        {
            TriggerAttack();            
        }
        else
        {
            if (attackLeft == true) _inputs.fireLeft = false;
        }

        _attackAnimWeight = Mathf.Lerp(_attackAnimWeight, attackLeft ? 1.0f : 0.0f, _animLayerSpeed * Time.deltaTime);
        animator.SetLayerWeight(_animCombatLayer, _attackAnimWeight);
    }

    public void TriggerAttack()
    {
        SwapClip();        

        if (_animAttackID == -1) _animAttackID = Animator.StringToHash(attackString);
        animator.SetTrigger(_animAttackID);        

        _attackAnimWeight = 1.0f;

        _inputs.fireLeft = false;
        attackLeft = true;

        lastAttack = Time.timeSinceLevelLoad;

        var length = animator.GetCurrentAnimatorClipInfo(_animCombatLayer)[0].clip.length * attackAnimLength;        
        Invoke(nameof(ResetTriggerAttack), length);
    }

    public void ResetTriggerAttack()
    {
        animator.ResetTrigger(_animAttackID);
        attackLeft = false;
    }
}
