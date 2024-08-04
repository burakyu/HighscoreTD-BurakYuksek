using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable 
{
    bool GetsDamage { get; set; }
    public IEnumerator GetDamage(float damage, float delay);
    //float Health { get; }
}
