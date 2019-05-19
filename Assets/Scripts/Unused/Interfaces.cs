using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGrounded {
    bool MyGrounded { get;set; }
}

public interface IAirable {
    void isAir();
}

public interface IAttackable {
    void isAttacking();
}

public interface IDamageable {
    void Damage(int damageAmount);
}

public interface IHealable {

}
