/////////////////////////////////////////////////////////////////////////////////
//
//	vp_DMDamageCallbacks.cs
//	© Opsive. All Rights Reserved.
//	https://twitter.com/Opsive
//	http://www.opsive.com
//
//	description:	an example of how to extend the base (vp_MPDamageCallbacks)
//					class with additional callback logic for 'Damage' events. here,
//					we refresh the 'Deaths', 'Frags' and 'Score' stats declared in
//					vp_DMPlayerStats every time a player dies, and broadcast a new
//					gamestate (with these stats only) to reflect it on all machines.
//
//					TIP: study the base class to see how the 'TransmitKill' callback works
//					
/////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CLDamageCallbacks : vp_MPDamageCallbacks
{

	/// <summary>
	/// 
	/// </summary>
	protected override void TransmitDamage(Transform targetTransform, Transform sourceTransform, float damage)
	{

		if (!PhotonNetwork.isMasterClient)
			return;

		vp_MPNetworkPlayer target = vp_MPNetworkPlayer.Get(targetTransform);
		if ((target == null)									// if target is an object (not a player) ...
			|| (target.DamageHandler.CurrentHealth > 0.0f))		// ... or it's a player that was only damaged (not killed) ...
		{
			// ... transmit a simple health update (update remote clients with
			// its health from the master scene) and bail out
			base.TransmitDamage(targetTransform, sourceTransform, damage);
			return;
		}

		// if this was healing and not damage, nothing more to do here
		if (damage <= 0.0f)
			return;

		// if we get here then target was a player that got killed, so
		// see if we know about the damage source

		vp_MPNetworkPlayer source = vp_MPNetworkPlayer.Get(sourceTransform);
		if (source == null)
			return;
				
		// we know who did it! if this injury killed the target, update the
		// local (master scene) stats for both players TIP: hit statistics
		// can be implemented here
		if ((target.DamageHandler.CurrentHealth + damage) > 0.0f)
		{
			
			if (target != source)
			{
				source.Stats.Set("Killstreak", (int)source.Stats.Get("Killstreak") + 1);
				target.Stats.Set("Killstreak", (int) 0);
			}
			else
			{
				target.Stats.Set("Killstreak", (int) 0);
			}

		}

		// send RPC with updated stats to the photonView of the gamelogic
		// object on all clients. NOTES:
		//	1) we only broadcast the stats that have actually changed
		//	2) we can't send a target and sender with the same ID, since
		//     adding the same key twice to a hashtable is impossible
		if (target != source)	// kill
		{
			vp_MPMaster.Instance.TransmitPlayerState(new int[] { target.ID, source.ID },
				new string[] { "Killstreak" },
				new string[] { "Killstreak" });
		}
		else	// suicide
		{
			vp_MPMaster.Instance.TransmitPlayerState(new int[] { target.ID },
				new string[] { "Killstreak" });
		}
		
	}

}
