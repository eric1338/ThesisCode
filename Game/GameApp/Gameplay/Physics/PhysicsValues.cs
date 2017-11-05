using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Gameplay.Physics
{
	class PhysicsValues
	{

		public static readonly float CollectibleHitboxWidth = 0.1f;
		public static readonly float CollectibleHitboxHeight = 0.1f;

		public static readonly float PlayerHitboxWidth = 0.16f;
		public static readonly float PlayerHitboxHeight = 0.32f;

		public static float GetHalfPlayerHitboxWidth()
		{
			return PlayerHitboxWidth / 2.0f;
		}

		public static readonly float RightFootOffset = PlayerHitboxWidth / 2;
		public static readonly float LeftFootOffset = PlayerHitboxWidth / 2;

		public static Vector2 GetRightFootPosition(Vector2 playerPosition)
		{
			return playerPosition + new Vector2(RightFootOffset, 0);
		}

		public static Vector2 GetLeftFootPosition(Vector2 playerPosition)
		{
			return playerPosition + new Vector2(-LeftFootOffset, 0);
		}

		public static readonly float HorizontalPlayerVelocity = 0.024f;
		public static readonly float GravityAcceleration = 0.001f;
		public static readonly float JumpAcceleration = 0.03f;

	}
}
