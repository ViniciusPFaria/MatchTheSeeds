using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrophyRoomSoundCall : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	public void PlayTrophySounds()
    {
        AudioControl.INSTANCE.stopBackGroundSound();
        AudioControl.INSTANCE.PlaySound(8);
    }

    public void StopTrophySounds()
    {
        AudioControl.INSTANCE.stopBackGroundSound();
        AudioControl.INSTANCE.PlaySound(2);
    }
}
