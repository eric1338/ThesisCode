﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Screens.Input
{
	enum UserAction
	{
		Jump,
		Duck,
		Defend,
		ToggleAlwaysDefending,

		JumpToNextLevel,
		RestartLevel,
		ResetLevel,
		TogglePauseGame,
		ReturnToMainMenu,

		ImportSong,
		SelectCurrentMenuItem,
		GoUpInMenu,
		GoDownInMenu,

		DecreaseDifficulty,
		IncreaseDifficulty
	}
}
