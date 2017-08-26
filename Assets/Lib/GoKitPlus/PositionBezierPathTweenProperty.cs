using UnityEngine;
using System.Collections;

public class PositionBezierPathTweenProperty: AbstractTweenProperty {
    protected Transform _target;
    //protected BezierCreater _path;
    //public PositionBezierPathTweenProperty(BezierCreater path, bool isRelative = false) {
    //    _path = path;
    //}


    #region Object overrides

    public override int GetHashCode() {
        return base.GetHashCode();
    }


    public override bool Equals(object obj) {
        // start with a base check and then compare if we are both using local values
        if(base.Equals(obj))
            return true;
        return false;
    }

    #endregion


    public override void prepareForUse() {
        _target = _ownerTween.target as Transform;
    }


    public override void tick(float totalElapsedTime) {
        var easedTime = _easeFunction(totalElapsedTime, 0, 1, _ownerTween.duration);
        //var vec = _path.GetPointAtRate(easedTime);
        //_target.position = vec;
    }


}
