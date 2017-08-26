using UnityEngine;
using System.Collections;


public static class GoKitTweenExtensionsDelay
{
	#region Transform extensions
	
	// to tweens
    public static GoTween rotationTo( this Transform self, float duration, Vector3 endValue, float delay, bool isRelative = false )
    {
        return Go.to( self, duration, new GoTweenConfig().rotation( endValue, isRelative ).setDelay(delay) );
    }


    public static GoTween localRotationTo(this Transform self, float duration, Vector3 endValue, float delay, bool isRelative = false)
    {
        return Go.to(self, duration, new GoTweenConfig().localRotation(endValue, isRelative).setDelay(delay));
    }


    public static GoTween eulerAnglesTo(this Transform self, float duration, Vector3 endValue, float delay, bool isRelative = false)
	{
        return Go.to(self, duration, new GoTweenConfig().eulerAngles(endValue, isRelative).setDelay(delay));
	}


    public static GoTween localEulerAnglesTo(this Transform self, float duration, Vector3 endValue, float delay, bool isRelative = false)
	{
        return Go.to(self, duration, new GoTweenConfig().localEulerAngles(endValue, isRelative).setDelay(delay));
	}


    public static GoTween positionTo(this Transform self, float duration, Vector3 endValue, float delay, bool isRelative = false)
	{
        return Go.to(self, duration, new GoTweenConfig().position(endValue, isRelative).setDelay(delay));
	}


    public static GoTween localPositionTo(this Transform self, float duration, Vector3 endValue, float delay, bool isRelative = false)
	{
        return Go.to(self, duration, new GoTweenConfig().localPosition(endValue, isRelative).setDelay(delay));
	}


    public static GoTween scaleTo(this Transform self, float duration, float endValue, float delay, bool isRelative = false)
	{
        return self.scaleTo(duration, new Vector3(endValue, endValue, endValue), delay, isRelative);
	}


    public static GoTween scaleTo(this Transform self, float duration, Vector3 endValue, float delay, bool isRelative = false)
	{
        return Go.to(self, duration, new GoTweenConfig().scale(endValue, isRelative).setDelay(delay));
	}


    public static GoTween shake(this Transform self, float duration, Vector3 shakeMagnitude, float delay, GoShakeType shakeType = GoShakeType.Position, int frameMod = 1, bool useLocalProperties = false)
	{
        return Go.to(self, duration, new GoTweenConfig().shake(shakeMagnitude, shakeType, frameMod, useLocalProperties).setDelay(delay));
	}
	
	
	// from tweens
    public static GoTween rotationFrom(this Transform self, float duration, Vector3 endValue, float delay, bool isRelative = false)
    {
        return Go.from(self, duration, new GoTweenConfig().rotation(endValue, isRelative).setDelay(delay));
    }


    public static GoTween localRotationFrom(this Transform self, float duration, Vector3 endValue, float delay, bool isRelative = false)
    {
        return Go.from(self, duration, new GoTweenConfig().localRotation(endValue, isRelative).setDelay(delay));
    }


    public static GoTween eulerAnglesFrom(this Transform self, float duration, Vector3 endValue, float delay, bool isRelative = false)
	{
        return Go.from(self, duration, new GoTweenConfig().eulerAngles(endValue, isRelative).setDelay(delay));
	}


    public static GoTween localEulerAnglesFrom(this Transform self, float duration, Vector3 endValue, float delay, bool isRelative = false)
	{
        return Go.from(self, duration, new GoTweenConfig().localEulerAngles(endValue, isRelative).setDelay(delay));
	}


    public static GoTween positionFrom(this Transform self, float duration, Vector3 endValue, float delay, bool isRelative = false)
	{
        return Go.from(self, duration, new GoTweenConfig().position(endValue, isRelative).setDelay(delay));
	}


    public static GoTween localPositionFrom(this Transform self, float duration, Vector3 endValue, float delay, bool isRelative = false)
	{
        return Go.from(self, duration, new GoTweenConfig().localPosition(endValue, isRelative).setDelay(delay));
	}


    public static GoTween scaleFrom(this Transform self, float duration, Vector3 endValue, float delay, bool isRelative = false)
	{
        return Go.from(self, duration, new GoTweenConfig().scale(endValue, isRelative).setDelay(delay));
	}
	
	#endregion
	
	
	#region Material extensions

    public static GoTween colorTo(this Material self, float duration, Color endValue, float delay, string colorName = "_Color")
	{
        return Go.to(self, duration, new GoTweenConfig().materialColor(endValue, colorName).setDelay(delay));
	}


    public static GoTween colorFrom(this Material self, float duration, Color endValue, float delay, string colorName = "_Color")
	{
        return Go.from(self, duration, new GoTweenConfig().materialColor(endValue, colorName).setDelay(delay));
	}
	
	#endregion
}
