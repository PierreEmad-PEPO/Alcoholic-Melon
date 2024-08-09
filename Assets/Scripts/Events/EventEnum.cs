using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventEnum
{
    // Penalty Shoot
    OnPlayerStartShoot,
    OnBallMove,
    OnBallHitTarget,
    OnBallHitGoal,
    OnResetShoot,
    OnPenaltyTryAgain,

    // Wheelbarrow Packages
    onPlayerHitPackage,
    OnPackagePickUp,
    OnPackageDrop,
    onTryAgainWheelbarrow,
    onWionWheelbarrow,
    onPackageDlivered,
    onPackageReached
}