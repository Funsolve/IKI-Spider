using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderBrainState : BrainState
{
    public override void Update(Transform _body, Transform _target)
    {
        Vector3 dirVec = _target.position - _body.position;
        _body.Translate(new Vector3(dirVec.x, 0f, dirVec.z));
    }
}
