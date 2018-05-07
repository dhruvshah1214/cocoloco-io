/////////////////////////////////////////////////////////////////////////////////
//
//	vp_DMPlayerStats.cs
//	© Opsive. All Rights Reserved.
//	https://twitter.com/Opsive
//	http://www.opsive.com
//
//	description:	an example of how to extend the base (vp_MPPlayerStats) class
//					with getter and setter actions for additional player stats:
//					Deaths, Frags and Score. these are later manipulated in the
//					example class 'vp_DMDamageCallbacks'
//
//					IMPORTANT: by default, a 'vp_MPPlayerStats' component is auto-added
//					to every player by the vp_MPPlayerSpawner on startup. if you want to
//					use a derived component instead (such as this one) then you must
//					update the name of the data component in the Inspector. go to your
//					vp_MPPlayerSpawner component - > Add Components -> Local & Remote
//					and update the string to match the new stats component classname.
//
//					TIP: study the base class to learn more about player stats
//
/////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CLPlayerStats : vp_MPPlayerStats
{

	protected int Killstreak = 0;
	
	public override void InitStats()
	{

		base.InitStats();


		Getters.Add("Killstreak", delegate() { return Killstreak; });

		Setters.Add("Killstreak", delegate(object val) { Killstreak = (int)val; });

	}


	/// <summary>
	/// resets health, shots and inventory to default + resurrects
	/// this player (if dead)
	/// </summary>
	public override void FullReset()
	{

		base.FullReset();		// always remember to call base in subsequent overrides
		
		Killstreak = 0;

	}	

}