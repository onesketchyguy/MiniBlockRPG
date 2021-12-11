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

    // FIXME: Have this be set automatically when switching weapons
    [SerializeField] private Collider damagerCollider = null;

    [Range(0.0f, 1.0f)]
    [SerializeField] private float attackAnimLength = 0.75f;
    [SerializeField] private string attackString = "Attack";
    private int _animAttackID = -1;
    private float _attackAnimWeight = 0.0f;    

    [SerializeField] private int _animCombatLayer = 1;
    [SerializeField] private float _animLayerSpeed = 1.0f;

    [SerializeField] private StarterAssets.StarterAssetsInputs _inputs = null;

    private bool attackLeft = false;    

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

    // Using these so we can use Invoke()
    private void EnableDamageCollider()
    {
        damagerCollider.enabled = true;
    }

    // Using these so we can use Invoke()
    private void DisableDamageCollider()
    {
        damagerCollider.enabled = false;
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
        CancelInvoke(nameof(ResetTriggerAttack));
        Invoke(nameof(ResetTriggerAttack), length);

        Invoke(nameof(EnableDamageCollider), length * 0.1f);
        Invoke(nameof(DisableDamageCollider), length * 0.9f);
    }

    public void ResetTriggerAttack()
    {
        animator.ResetTrigger(_animAttackID);
        attackLeft = false;
    }
}
