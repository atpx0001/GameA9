using UnityEngine;
using System;
using System.Collections;


public class FloatTweenProperty : AbstractTweenProperty, IGenericProperty
{
	public string propertyName { get; private set; }
	private Action<float> _setter;
    private Func<float> _getter;
	
	protected float _originalEndValue;
	protected float _startValue;
	protected float _endValue;
	protected float _diffValue;
	
	
	public FloatTweenProperty( string propertyName, float endValue, bool isRelative = false ) : base( isRelative )
	{
		this.propertyName = propertyName;
		_originalEndValue = endValue;
	}

    public FloatTweenProperty(Func<float> getter, Action<float> setter, float endValue, bool isRelative = false)
        : base(isRelative) {
        this._setter = setter;
        this._getter = getter;
        _originalEndValue = endValue;
    }
	

	/// <summary>
	/// validation checks to make sure the target has a valid property with an accessible setter
	/// </summary>
	public override bool validateTarget( object target )
	{
		// cache the setter
        if(_setter == null) {
            _setter = GoTweenUtils.setterForProperty<Action<float>>(target, propertyName);
        }
		return _setter != null;
	}

	
	public override void prepareForUse()
	{
		// retrieve the getter
        var getter = _getter != null ? _getter : GoTweenUtils.getterForProperty<Func<float>>(_ownerTween.target, propertyName);
		
		_endValue = _originalEndValue;
		
		// if this is a from tween we need to swap the start and end values
		if( _ownerTween.isFrom )
		{
			_startValue = _endValue;
			_endValue = getter();
		}
		else
		{
			_startValue = getter();
		}
		
		// setup the diff value
		if( _isRelative && !_ownerTween.isFrom )
			_diffValue = _endValue;
		else
			_diffValue = _endValue - _startValue;
	}
	

	public override void tick( float totalElapsedTime )
	{
		var easedValue = _easeFunction( totalElapsedTime, _startValue, _diffValue, _ownerTween.duration );
		_setter( easedValue );
	}
}
