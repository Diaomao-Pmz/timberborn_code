using System;
using System.Text;
using Timberborn.CoreUI;
using Timberborn.Localization;
using Timberborn.PlatformUtilities;
using Timberborn.PlayerDataSystem;
using UnityEngine.UIElements;

namespace Timberborn.MainMenuScene
{
	// Token: 0x0200000C RID: 12
	public class PlayerDataLoader
	{
		// Token: 0x0600002A RID: 42 RVA: 0x000025FD File Offset: 0x000007FD
		public PlayerDataLoader(IPlayerDataService playerDataService, DialogBoxShower dialogBoxShower, VisualElementLoader visualElementLoader, IExplorerOpener explorerOpener, ILoc loc, HyperlinkInitializer hyperlinkInitializer)
		{
			this._playerDataService = playerDataService;
			this._dialogBoxShower = dialogBoxShower;
			this._visualElementLoader = visualElementLoader;
			this._explorerOpener = explorerOpener;
			this._loc = loc;
			this._hyperlinkInitializer = hyperlinkInitializer;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002634 File Offset: 0x00000834
		public void Load(Action nextAction)
		{
			if (this._playerDataService.DataLoadSuccessful)
			{
				nextAction();
				return;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(this._loc.T(PlayerDataLoader.CorruptedDataLocKey));
			string elementName = "MainMenu/CorruptedPlayerDataLabel";
			Label label = UQueryExtensions.Q<Label>(this._visualElementLoader.LoadVisualElement(elementName), null, null);
			this._hyperlinkInitializer.Initialize(label, delegate
			{
				this._explorerOpener.OpenDirectory(PlayerDataFileService.PlayerDataDirectory);
			});
			this._dialogBoxShower.Create().SetMessage(stringBuilder.ToString().TrimEnd()).AddContent(label).SetConfirmButton(nextAction).Show();
		}

		// Token: 0x04000022 RID: 34
		public static readonly string CorruptedDataLocKey = "PlayerData.CorruptedDataInfo.Header";

		// Token: 0x04000023 RID: 35
		public readonly IPlayerDataService _playerDataService;

		// Token: 0x04000024 RID: 36
		public readonly DialogBoxShower _dialogBoxShower;

		// Token: 0x04000025 RID: 37
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000026 RID: 38
		public readonly IExplorerOpener _explorerOpener;

		// Token: 0x04000027 RID: 39
		public readonly ILoc _loc;

		// Token: 0x04000028 RID: 40
		public readonly HyperlinkInitializer _hyperlinkInitializer;
	}
}
