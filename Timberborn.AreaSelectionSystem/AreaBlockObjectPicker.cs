using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Timberborn.BlockObjectPickingSystem;
using Timberborn.BlockSystem;
using UnityEngine;

namespace Timberborn.AreaSelectionSystem
{
	// Token: 0x0200000C RID: 12
	public class AreaBlockObjectPicker
	{
		// Token: 0x0600001D RID: 29 RVA: 0x00002460 File Offset: 0x00000660
		public AreaBlockObjectPicker(AreaSelectionController areaSelectionController, AreaSelector areaSelector, BlockObjectPicker blockObjectPicker, BlockObjectPickingMode pickingMode)
		{
			this._areaSelectionController = areaSelectionController;
			this._areaSelector = areaSelector;
			this._blockObjectPicker = blockObjectPicker;
			this._pickingMode = pickingMode;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002488 File Offset: 0x00000688
		public bool PickBlockObjects<T>(AreaBlockObjectPicker.Callback previewCallback, AreaBlockObjectPicker.Callback actionCallback, Action showNoneCallback, Func<BlockObject, bool> blockObjectFilter = null)
		{
			return this._areaSelectionController.ProcessInput(delegate(Ray startRay, Ray endRay, bool selectionStarted)
			{
				ValueTuple<IEnumerable<BlockObject>, Vector3Int, Vector3Int, bool> valueTuple = this.PickBlockObjects<T>(startRay, endRay, blockObjectFilter);
				IEnumerable<BlockObject> item = valueTuple.Item1;
				Vector3Int item2 = valueTuple.Item2;
				Vector3Int item3 = valueTuple.Item3;
				bool item4 = valueTuple.Item4;
				previewCallback(item, item2, item3, selectionStarted, item4);
			}, delegate(Ray startRay, Ray endRay, bool selectionStarted)
			{
				ValueTuple<IEnumerable<BlockObject>, Vector3Int, Vector3Int, bool> valueTuple = this.PickBlockObjects<T>(startRay, endRay, blockObjectFilter);
				IEnumerable<BlockObject> item = valueTuple.Item1;
				Vector3Int item2 = valueTuple.Item2;
				Vector3Int item3 = valueTuple.Item3;
				bool item4 = valueTuple.Item4;
				actionCallback(item, item2, item3, selectionStarted, item4);
			}, showNoneCallback);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000024DC File Offset: 0x000006DC
		public void Reset()
		{
			this._areaSelectionController.Reset();
			this._selectionStart = null;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000024F8 File Offset: 0x000006F8
		[return: TupleElementNames(new string[]
		{
			"blockObjects",
			"start",
			"end",
			"selectingArea"
		})]
		public ValueTuple<IEnumerable<BlockObject>, Vector3Int, Vector3Int, bool> PickBlockObjects<T>(Ray startRay, Ray endRay, Func<BlockObject, bool> blockObjectFilter)
		{
			bool flag = !startRay.Equals(endRay);
			SelectionStart? selectionStart = this.GetSelectionStart<T>(startRay, flag);
			if (selectionStart != null)
			{
				SelectionStart valueOrDefault = selectionStart.GetValueOrDefault();
				Vector3Int coordinates = valueOrDefault.Coordinates;
				Vector3Int vector3Int = flag ? this._areaSelector.GetSelectionEnd(valueOrDefault, endRay) : coordinates;
				return new ValueTuple<IEnumerable<BlockObject>, Vector3Int, Vector3Int, bool>(from blockObject in this._blockObjectPicker.PickBlockObjects(valueOrDefault, vector3Int, this._pickingMode, blockObjectFilter, flag)
				where blockObject.GetComponent<T>() != null
				select blockObject, coordinates, vector3Int, flag);
			}
			return new ValueTuple<IEnumerable<BlockObject>, Vector3Int, Vector3Int, bool>(Enumerable.Empty<BlockObject>(), default(Vector3Int), default(Vector3Int), false);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000025B6 File Offset: 0x000007B6
		public SelectionStart? GetSelectionStart<T>(Ray startRay, bool selectingArea)
		{
			if (!selectingArea)
			{
				this._selectionStart = this._areaSelector.GetSelectionStart<T>(startRay);
			}
			return this._selectionStart;
		}

		// Token: 0x0400001A RID: 26
		public readonly AreaSelectionController _areaSelectionController;

		// Token: 0x0400001B RID: 27
		public readonly AreaSelector _areaSelector;

		// Token: 0x0400001C RID: 28
		public readonly BlockObjectPicker _blockObjectPicker;

		// Token: 0x0400001D RID: 29
		public readonly BlockObjectPickingMode _pickingMode;

		// Token: 0x0400001E RID: 30
		public SelectionStart? _selectionStart;

		// Token: 0x0200000D RID: 13
		// (Invoke) Token: 0x06000023 RID: 35
		public delegate void Callback(IEnumerable<BlockObject> blockObjects, Vector3Int start, Vector3Int end, bool selectionStarted, bool selectingArea);
	}
}
