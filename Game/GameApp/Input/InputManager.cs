using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Input
{
	class InputManager
	{

		private Dictionary<Key, UserAction> prolongedUserActionKeyMap = new Dictionary<Key, UserAction>();
		private Dictionary<Key, UserAction> singleUserActionKeyMap = new Dictionary<Key, UserAction>();

		private Dictionary<UserAction, bool> prolongedUserActions = new Dictionary<UserAction, bool>();
		private Queue<UserAction> singleUserActions = new Queue<UserAction>();

		public InputManager()
		{

		}

		public void AddSingleUserActionMapping(Key key, UserAction userAction)
		{
			singleUserActionKeyMap.Add(key, userAction);
		}

		public void AddProlongedUserActionMapping(Key key, UserAction userAction)
		{
			prolongedUserActionKeyMap.Add(key, userAction);
		}

		public void ProcessKeyUp(Key key)
		{
			if (prolongedUserActionKeyMap.ContainsKey(key))
			{
				prolongedUserActions[prolongedUserActionKeyMap[key]] = false;
			}
		}

		public void ProcessKeyDown(Key key)
		{
			if (prolongedUserActionKeyMap.ContainsKey(key))
			{
				prolongedUserActions[prolongedUserActionKeyMap[key]] = true;
			}

			if (singleUserActionKeyMap.ContainsKey(key))
			{
				singleUserActions.Enqueue(singleUserActionKeyMap[key]);
			}
		}

		public List<UserAction> GetSingleUserActionsAsList()
		{
			List<UserAction> singleUserActionsList = singleUserActions.ToList();
			singleUserActions.Clear();

			return singleUserActionsList;
		}

		public bool IsUserActionActive(UserAction userAction)
		{
			return prolongedUserActions.ContainsKey(userAction) && prolongedUserActions[userAction];
		}


	}
}
