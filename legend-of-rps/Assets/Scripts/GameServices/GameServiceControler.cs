using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameServiceControler : UnityActiveSingleton<GameServiceControler>
{
    private bool isInitialize;
    public bool IsInitialize { get => isInitialize; private set => isInitialize = value; }
	
	public void Initialize()
	{
		if (IsInitialize)
			return;
		IsInitialize = true;
	}
}
