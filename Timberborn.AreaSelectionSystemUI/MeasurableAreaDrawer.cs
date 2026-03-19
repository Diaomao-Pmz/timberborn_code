using System;
using System.Collections.Generic;
using Timberborn.CoreUI;
using Timberborn.SingletonSystem;
using Timberborn.ToolPanelSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.AreaSelectionSystemUI
{
	// Token: 0x02000008 RID: 8
	public class MeasurableAreaDrawer : ILateUpdatableSingleton, IToolFragment
	{
		// Token: 0x0600000E RID: 14 RVA: 0x00002273 File Offset: 0x00000473
		public MeasurableAreaDrawer(VisualElementLoader visualElementLoader)
		{
			this._visualElementLoader = visualElementLoader;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000228D File Offset: 0x0000048D
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Common/MeasurableAreaPanel");
			this._dimensions = UQueryExtensions.Q<Label>(this._root, "Dimensions", null);
			return this._root;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000022C2 File Offset: 0x000004C2
		public void LateUpdateSingleton()
		{
			this.UpdateDrawingState();
			this.UpdatePanel();
			this._areaCoordinates.Clear();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000022DB File Offset: 0x000004DB
		public void AddMeasurableCoordinates(Vector3Int coordinates)
		{
			this._areaCoordinates.Add(coordinates);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022E9 File Offset: 0x000004E9
		public void AddMeasurableCoordinates(IEnumerable<Vector3Int> coordinates)
		{
			this._areaCoordinates.AddRange(coordinates);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000022F8 File Offset: 0x000004F8
		public void UpdateDrawingState()
		{
			if (!this._isDrawing && this._areaCoordinates.Count > 1)
			{
				if (this._frameDelayed)
				{
					this._isDrawing = true;
				}
				this._frameDelayed = true;
				return;
			}
			if (this._isDrawing && this._areaCoordinates.Count <= 1)
			{
				this._frameDelayed = false;
				this._isDrawing = false;
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002356 File Offset: 0x00000556
		public void UpdatePanel()
		{
			if (this._isDrawing)
			{
				this._root.ToggleDisplayStyle(true);
				this.UpdateDimensions();
				return;
			}
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002380 File Offset: 0x00000580
		public void UpdateDimensions()
		{
			int num = int.MaxValue;
			int num2 = int.MinValue;
			int num3 = int.MaxValue;
			int num4 = int.MinValue;
			foreach (Vector3Int vector3Int in this._areaCoordinates)
			{
				num = Mathf.Min(num, vector3Int.x);
				num2 = Mathf.Max(num2, vector3Int.x);
				num3 = Mathf.Min(num3, vector3Int.y);
				num4 = Mathf.Max(num4, vector3Int.y);
			}
			this._dimensions.text = string.Format("{0} × {1}", num2 - num + 1, num4 - num3 + 1);
		}

		// Token: 0x04000011 RID: 17
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000012 RID: 18
		public readonly List<Vector3Int> _areaCoordinates = new List<Vector3Int>();

		// Token: 0x04000013 RID: 19
		public VisualElement _root;

		// Token: 0x04000014 RID: 20
		public Label _dimensions;

		// Token: 0x04000015 RID: 21
		public bool _isDrawing;

		// Token: 0x04000016 RID: 22
		public bool _frameDelayed;
	}
}
