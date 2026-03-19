using System;
using Timberborn.Bots;
using Timberborn.Coordinates;
using Timberborn.CursorToolSystem;
using Timberborn.InputSystem;
using Timberborn.ToolSystem;
using UnityEngine;

namespace Timberborn.BotsUI
{
	// Token: 0x0200000B RID: 11
	public class BotGeneratorTool : IDevModeTool, ITool, IInputProcessor
	{
		// Token: 0x0600001C RID: 28 RVA: 0x00002353 File Offset: 0x00000553
		public BotGeneratorTool(BotFactory botFactory, InputService inputService, CursorService cursorService, CursorCoordinatesPicker cursorCoordinatesPicker)
		{
			this._botFactory = botFactory;
			this._inputService = inputService;
			this._cursorService = cursorService;
			this._cursorCoordinatesPicker = cursorCoordinatesPicker;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002378 File Offset: 0x00000578
		public bool IsDevMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000237C File Offset: 0x0000057C
		public bool ProcessInput()
		{
			if (this._inputService.MainMouseButtonDown && !this._inputService.MouseOverUI)
			{
				int count = this._inputService.IsKeyHeld(BotGeneratorTool.SpawnManyCharactersKey) ? BotGeneratorTool.ManyBotsToAdd : 1;
				this.PlaceBots(count);
				return true;
			}
			return false;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000023C8 File Offset: 0x000005C8
		public void Enter()
		{
			this._cursorService.SetCursor(BotGeneratorTool.CursorKey);
			this._inputService.AddInputProcessor(this);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000023E6 File Offset: 0x000005E6
		public void Exit()
		{
			this._cursorService.ResetCursor();
			this._inputService.RemoveInputProcessor(this);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002400 File Offset: 0x00000600
		public void PlaceBots(int count)
		{
			CursorCoordinates? cursorCoordinates = this._cursorCoordinatesPicker.Pick();
			if (cursorCoordinates != null)
			{
				Vector3 position = CoordinateSystem.GridToWorldCentered(cursorCoordinates.GetValueOrDefault().TileCoordinates);
				for (int i = 0; i < count; i++)
				{
					this._botFactory.Create(position);
				}
			}
		}

		// Token: 0x04000018 RID: 24
		public static readonly string CursorKey = "BeaverAvatarCursor";

		// Token: 0x04000019 RID: 25
		public static readonly string SpawnManyCharactersKey = "SpawnManyCharacters";

		// Token: 0x0400001A RID: 26
		public static readonly int ManyBotsToAdd = 10;

		// Token: 0x0400001B RID: 27
		public readonly BotFactory _botFactory;

		// Token: 0x0400001C RID: 28
		public readonly InputService _inputService;

		// Token: 0x0400001D RID: 29
		public readonly CursorService _cursorService;

		// Token: 0x0400001E RID: 30
		public readonly CursorCoordinatesPicker _cursorCoordinatesPicker;
	}
}
