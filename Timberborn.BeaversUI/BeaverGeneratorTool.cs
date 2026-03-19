using System;
using Timberborn.Beavers;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.CursorToolSystem;
using Timberborn.InputSystem;
using Timberborn.ToolSystem;
using UnityEngine;

namespace Timberborn.BeaversUI
{
	// Token: 0x02000011 RID: 17
	public class BeaverGeneratorTool : IDevModeTool, ITool, IInputProcessor
	{
		// Token: 0x06000047 RID: 71 RVA: 0x00002E33 File Offset: 0x00001033
		public BeaverGeneratorTool(BeaverFactory beaverFactory, InputService inputService, IRandomNumberGenerator randomNumberGenerator, CursorService cursorService, CursorCoordinatesPicker cursorCoordinatesPicker)
		{
			this._beaverFactory = beaverFactory;
			this._inputService = inputService;
			this._randomNumberGenerator = randomNumberGenerator;
			this._cursorService = cursorService;
			this._cursorCoordinatesPicker = cursorCoordinatesPicker;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002E60 File Offset: 0x00001060
		public bool IsDevMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002E64 File Offset: 0x00001064
		public bool ProcessInput()
		{
			if (this._inputService.MainMouseButtonDown && !this._inputService.MouseOverUI)
			{
				bool isChild = this._inputService.IsKeyHeld(BeaverGeneratorTool.SpawnChildKey);
				int count = this._inputService.IsKeyHeld(BeaverGeneratorTool.SpawnManyCharactersKey) ? BeaverGeneratorTool.ManyBeaversToAdd : 1;
				this.PlaceBeavers(isChild, count);
				return true;
			}
			return false;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002EC2 File Offset: 0x000010C2
		public void Enter()
		{
			this._cursorService.SetCursor(BeaverGeneratorTool.CursorKey);
			this._inputService.AddInputProcessor(this);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002EE0 File Offset: 0x000010E0
		public void Exit()
		{
			this._cursorService.ResetCursor();
			this._inputService.RemoveInputProcessor(this);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002EFC File Offset: 0x000010FC
		public void PlaceBeavers(bool isChild, int count)
		{
			CursorCoordinates? cursorCoordinates = this._cursorCoordinatesPicker.Pick();
			if (cursorCoordinates != null)
			{
				Vector3 position = CoordinateSystem.GridToWorldCentered(cursorCoordinates.GetValueOrDefault().TileCoordinates);
				for (int i = 0; i < count; i++)
				{
					float num = this._randomNumberGenerator.Range(0f, 1f);
					if (isChild)
					{
						this._beaverFactory.CreateChild(position, num);
					}
					else
					{
						this._beaverFactory.CreateAdult(position, num);
					}
				}
			}
		}

		// Token: 0x0400004D RID: 77
		public static readonly string CursorKey = "BeaverAvatarCursor";

		// Token: 0x0400004E RID: 78
		public static readonly string SpawnManyCharactersKey = "SpawnManyCharacters";

		// Token: 0x0400004F RID: 79
		public static readonly string SpawnChildKey = "SpawnChild";

		// Token: 0x04000050 RID: 80
		public static readonly int ManyBeaversToAdd = 10;

		// Token: 0x04000051 RID: 81
		public readonly BeaverFactory _beaverFactory;

		// Token: 0x04000052 RID: 82
		public readonly InputService _inputService;

		// Token: 0x04000053 RID: 83
		public readonly IRandomNumberGenerator _randomNumberGenerator;

		// Token: 0x04000054 RID: 84
		public readonly CursorService _cursorService;

		// Token: 0x04000055 RID: 85
		public readonly CursorCoordinatesPicker _cursorCoordinatesPicker;
	}
}
