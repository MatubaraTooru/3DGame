using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class IKController : MonoBehaviour
{
    [SerializeField] Transform _rightTarget;
    [SerializeField] Transform _leftTarget;
    [SerializeField, Range(0f, 1f)] float _rightPositionWeight;
    [SerializeField, Range(0f, 1f)] float _leftPositionWeight;
    [SerializeField, Range(0f, 1f)] float _rightRotationWeight;
    [SerializeField, Range(0f, 1f)] float _leftRotationWeight;
    [SerializeField] Animator _animator;
    private void OnAnimatorIK(int layerIndex)
    {
        // âEéËÇÃIKÇê›íË
        _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, _rightPositionWeight);
        _animator.SetIKRotationWeight(AvatarIKGoal.RightHand, _rightRotationWeight);
        _animator.SetIKPosition(AvatarIKGoal.RightHand, _rightTarget.position);
        _animator.SetIKRotation(AvatarIKGoal.RightHand, _rightTarget.rotation);
        // ç∂éËÇÃIKÇê›íË
        _animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, _leftPositionWeight);
        _animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, _leftRotationWeight);
        _animator.SetIKPosition(AvatarIKGoal.LeftHand, _leftTarget.position);
        _animator.SetIKRotation(AvatarIKGoal.LeftHand, _leftTarget.rotation);
    }
}
