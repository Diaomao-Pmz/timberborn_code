using System;
using System.Collections.Generic;
using Timberborn.AreaSelectionSystem;
using Timberborn.InputSystem;
using UnityEngine;

namespace Timberborn.SelectionToolSystem
{
	// Token: 0x02000004 RID: 4
	public class SelectionToolProcessor : IInputProcessor
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public SelectionToolProcessor(AreaPicker areaPicker, InputService inputService, CursorService cursorService, Action<IEnumerable<Vector3Int>, Ray> previewCallback, Action<IEnumerable<Vector3Int>, Ray> actionCallback, Action showNoneCallback, string customCursor)
		{
			this._areaPicker = areaPicker;
			this._inputService = inputService;
			this._cursorService = cursorService;
			this._previewCallback = previewCallback;
			this._actionCallback = actionCallback;
			this._showNoneCallback = showNoneCallback;
			this._customCursor = customCursor;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020FB File Offset: 0x000002FB
		public bool ProcessInput()
		{
			return this._areaPicker.PickTerrainIntArea(delegate(IEnumerable<Vector3Int> blocks, Ray ray)
			{
				this._previewCallback(blocks, ray);
			}, delegate(IEnumerable<Vector3Int> blocks, Ray ray)
			{
				this._actionCallback(blocks, ray);
			}, this._showNoneCallback);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002126 File Offset: 0x00000326
		public void Enter()
		{
			this._inputService.AddInputProcessor(this);
			this._cursorService.SetCursor(this._customCursor);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002145 File Offset: 0x00000345
		public void Exit()
		{
			this._inputService.RemoveInputProcessor(this);
			this._areaPicker.Reset();
			this._cursorService.ResetCursor();
			this._showNoneCallback();
		}

		// Token: 0x04000006 RID: 6
		public readonly AreaPicker _areaPicker;

		// Token: 0x04000007 RID: 7
		public readonly InputService _inputService;

		// Token: 0x04000008 RID: 8
		public readonly CursorService _cursorService;

		// Token: 0x04000009 RID: 9
		public readonly Action<IEnumerable<Vector3Int>, Ray> _previewCallback;

		// Token: 0x0400000A RID: 10
		public readonly Action<IEnumerable<Vector3Int>, Ray> _actionCallback;

		// Token: 0x0400000B RID: 11
		public readonly Action _showNoneCallback;

		// Token: 0x0400000C RID: 12
		public readonly string _customCursor;
	}
}
