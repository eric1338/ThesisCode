using GameApp.Screens.Input;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Screens
{
	abstract class Screen
	{

		protected MyGameWindow gameWindow;

		private InputManager inputManager = new InputManager();

		// TODO: evtl in Mapping umbenennen
		private Dictionary<UserAction, Action> singleUserActionFunctionMapping = new Dictionary<UserAction, Action>();
		private Dictionary<UserAction, Action<bool>> prolongedUserActionFunctionMapping = new Dictionary<UserAction, Action<bool>>();


		public Screen(MyGameWindow gameWindow)
		{
			this.gameWindow = gameWindow;
		}

		public abstract void DoLogic();

		public abstract void Draw();

		protected void SwitchToScreen(Screen screen)
		{
			gameWindow.SetScreen(screen);
		}

		public void ProcessKeyUp(Key key)
		{
			inputManager.ProcessKeyUp(key);
		}

		public void ProcessKeyDown(Key key)
		{
			inputManager.ProcessKeyDown(key);
		}

		protected void AddKeyToSingleUserActionMapping(Key key, UserAction userAction)
		{
			inputManager.AddSingleUserActionMapping(key, userAction);
		}

		protected void AddKeyToProlongedUserActionMapping(Key key, UserAction userAction)
		{
			inputManager.AddProlongedUserActionMapping(key, userAction);
		}

		protected void AddSingleUserActionToFunctionMapping(UserAction userAction, Action function)
		{
			singleUserActionFunctionMapping.Add(userAction, function);
		}

		protected void AddProlongedUserActionToFunctionMapping(UserAction userAction, Action<bool> function)
		{
			prolongedUserActionFunctionMapping.Add(userAction, function);
		}

		protected void ProcessUserActions()
		{
			ProcessSingleUserActions();
			ProcessProlongedUserActions();
		}

		private void ProcessSingleUserActions()
		{
			List<UserAction> userActions = inputManager.GetSingleUserActionsAsList();

			foreach (UserAction userAction in userActions)
			{
				if (singleUserActionFunctionMapping.ContainsKey(userAction))
				{
					singleUserActionFunctionMapping[userAction]();
				}
			}
		}

		private void ProcessProlongedUserActions()
		{
			foreach (var userActionFunction in prolongedUserActionFunctionMapping)
			{
				bool value = inputManager.IsUserActionActive(userActionFunction.Key);

				userActionFunction.Value(value);
			}
		}

	}
}
