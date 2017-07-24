using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Physics
{
	interface IPhysics
	{

		// Param Level & current Forces (PlayerV, PlayerA) evtl?
		Vector2 CalculateNewPlayerPosition();

		// return new PlayerPos

	}
}
