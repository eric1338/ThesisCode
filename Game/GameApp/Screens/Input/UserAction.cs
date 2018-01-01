using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Screens.Input
{
	enum UserAction
	{
		Jump,
		Hit,
		Duck,
		Defend,

		ResetLevel,
		TogglePauseGame,
		ReturnToMainMenu,

		SelectCurrentMenuItem,
		GoUpInMenu,
		GoDownInMenu
	}
}
