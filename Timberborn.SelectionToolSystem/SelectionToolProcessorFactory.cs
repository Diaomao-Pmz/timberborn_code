using System;
using System.Collections.Generic;
using Timberborn.AreaSelectionSystem;
using Timberborn.InputSystem;
using UnityEngine;

namespace Timberborn.SelectionToolSystem
{
	// Token: 0x02000005 RID: 5
	public class SelectionToolProcessorFactory
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00002192 File Offset: 0x00000392
		public SelectionToolProcessorFactory(AreaPicker areaPicker, InputService inputService, CursorService cursorService)
		{
			this._areaPicker = areaPicker;
			this._inputService = inputService;
			this._cursorService = cursorService;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021AF File Offset: 0x000003AF
		public SelectionToolProcessor Create(Action<IEnumerable<Vector3Int>, Ray> previewCallback, Action<IEnumerable<Vector3Int>, Ray> actionCallback, Action showNoneCallback, string customCursor)
		{
			return new SelectionToolProcessor(this._areaPicker, this._inputService, this._cursorService, previewCallback, actionCallback, showNoneCallback, customCursor);
		}

		// Token: 0x0400000D RID: 13
		public readonly AreaPicker _areaPicker;

		// Token: 0x0400000E RID: 14
		public readonly InputService _inputService;

		// Token: 0x0400000F RID: 15
		public readonly CursorService _cursorService;
	}
}
