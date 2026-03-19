using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BlueprintSystem;
using Timberborn.BuildingRange;
using Timberborn.Common;
using Timberborn.SelectionSystem;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.RangedEffectBuildingUI
{
	// Token: 0x0200000B RID: 11
	public class RangeObjectHighlighterService : ILoadableSingleton
	{
		// Token: 0x06000033 RID: 51 RVA: 0x00002690 File Offset: 0x00000890
		public RangeObjectHighlighterService(ISpecService specService, Highlighter highlighter)
		{
			this._specService = specService;
			this._highlighter = highlighter;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000026BC File Offset: 0x000008BC
		public void Load()
		{
			this._buildingRangeObjectColor = this._specService.GetSingleSpec<RangedEffectBuildingColorsSpec>().BuildingRangeObject;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000026D4 File Offset: 0x000008D4
		public void AddBuildingWithObjectRange(IBuildingWithRange buildingWithRange)
		{
			this._buildingsWithRanges.GetOrAdd(buildingWithRange.RangeName).Add(buildingWithRange);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000026F0 File Offset: 0x000008F0
		public void RemoveBuildingWithObjectRange(IBuildingWithRange buildingWithRange)
		{
			string rangeName = buildingWithRange.RangeName;
			this._buildingsWithRanges[rangeName].Remove(buildingWithRange);
			this.RecalculateHighlightArea(rangeName);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000271E File Offset: 0x0000091E
		public void AddPreviewBuildingWithObjectRange(IBuildingWithRange buildingWithRange)
		{
			this._objectsPreview = buildingWithRange;
			this.HighlightObjects();
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000272D File Offset: 0x0000092D
		public void RemovePreviewBuildingWithObjectRange()
		{
			this._objectsPreview = null;
			this.ClearHighlights();
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000273C File Offset: 0x0000093C
		public void RecalculateAreaAndHighlightObjects(string rangeName)
		{
			this.RecalculateHighlightArea(rangeName);
			this.HighlightObjects();
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000274C File Offset: 0x0000094C
		public void HighlightObjects()
		{
			this.ClearHighlights();
			foreach (BaseComponent target in this._currentObjects)
			{
				this._highlighter.HighlightSecondary(target, this._buildingRangeObjectColor);
			}
			if (this._objectsPreview != null)
			{
				foreach (BaseComponent target2 in this._objectsPreview.GetObjectsInRange())
				{
					this._highlighter.HighlightSecondary(target2, this._buildingRangeObjectColor);
				}
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002804 File Offset: 0x00000A04
		public void ClearHighlights()
		{
			this._highlighter.UnhighlightAllSecondary();
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002814 File Offset: 0x00000A14
		public void RecalculateHighlightArea(string rangeName)
		{
			this._currentObjects.Clear();
			foreach (IBuildingWithRange buildingWithRange in this._buildingsWithRanges.GetOrAdd(rangeName))
			{
				this._currentObjects.UnionWith(buildingWithRange.GetObjectsInRange());
			}
		}

		// Token: 0x04000016 RID: 22
		public readonly ISpecService _specService;

		// Token: 0x04000017 RID: 23
		public readonly Highlighter _highlighter;

		// Token: 0x04000018 RID: 24
		public IBuildingWithRange _objectsPreview;

		// Token: 0x04000019 RID: 25
		public readonly HashSet<BaseComponent> _currentObjects = new HashSet<BaseComponent>();

		// Token: 0x0400001A RID: 26
		public readonly Dictionary<string, HashSet<IBuildingWithRange>> _buildingsWithRanges = new Dictionary<string, HashSet<IBuildingWithRange>>();

		// Token: 0x0400001B RID: 27
		public Color _buildingRangeObjectColor;
	}
}
