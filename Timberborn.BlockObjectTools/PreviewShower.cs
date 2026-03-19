using System;
using System.Collections.Generic;
using System.Text;
using Timberborn.AreaSelectionSystemUI;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.BlueprintSystem;
using Timberborn.Common;
using Timberborn.SelectionSystem;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.BlockObjectTools
{
	// Token: 0x0200002A RID: 42
	public class PreviewShower : ILoadableSingleton
	{
		// Token: 0x060000D6 RID: 214 RVA: 0x000040A4 File Offset: 0x000022A4
		public PreviewShower(Highlighter highlighter, ISpecService specService, MeasurableAreaDrawer measurableAreaDrawer)
		{
			this._highlighter = highlighter;
			this._specService = specService;
			this._measurableAreaDrawer = measurableAreaDrawer;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000040F8 File Offset: 0x000022F8
		public void Load()
		{
			this._previewShowerSpec = this._specService.GetSingleSpec<PreviewShowerSpec>();
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x0000410B File Offset: 0x0000230B
		public void ShowBuildablePreviewsAsSingle(IReadOnlyList<Preview> previews, out string warningMessage)
		{
			this.ShowBuildablePreviews(previews, true, out warningMessage);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00004116 File Offset: 0x00002316
		public void ShowBuildablePreviews(IReadOnlyList<Preview> previews, out string warningMessage)
		{
			this.ShowBuildablePreviews(previews, previews.Count == 1, out warningMessage);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00004129 File Offset: 0x00002329
		public void ShowUnbuildablePreview(Preview preview)
		{
			this.ShowPreview(preview, PreviewState.UnbuildableSingle, this._previewShowerSpec.UnbuildablePreview);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00004142 File Offset: 0x00002342
		public void ShowUnbuildablePreviewsAsSingle(IReadOnlyList<Preview> previews)
		{
			this.ShowUnbuildablePreviews(previews, true);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x0000414C File Offset: 0x0000234C
		public void ShowUnbuildablePreviews(IReadOnlyList<Preview> previews)
		{
			this.ShowUnbuildablePreviews(previews, previews.Count == 1);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x0000415E File Offset: 0x0000235E
		public void UnhighlightAllPreviews()
		{
			this._highlighter.UnhighlightAllPrimary();
		}

		// Token: 0x060000DE RID: 222 RVA: 0x0000416C File Offset: 0x0000236C
		public void HidePreviews(IEnumerable<Preview> previews)
		{
			this.UnhighlightAllPreviews();
			foreach (Preview preview in previews)
			{
				preview.Hide();
			}
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000041B8 File Offset: 0x000023B8
		public void ShowBuildablePreviews(IReadOnlyList<Preview> previews, bool showAsSingle, out string warningMessage)
		{
			HashSet<string> warningMessages = new HashSet<string>();
			for (int i = 0; i < previews.Count; i++)
			{
				Preview preview = previews[i];
				PreviewState previewState = showAsSingle ? PreviewState.BuildableSingle : ((i == previews.Count - 1) ? PreviewState.BuildableLast : PreviewState.BuildableNotLast);
				this.ShowBuildablePreview(preview, previewState, warningMessages);
				if (!showAsSingle)
				{
					this._areaCoordinates.AddRange(preview.BlockObject.PositionedBlocks.GetOccupiedCoordinates());
				}
			}
			foreach (BaseComponent target in this._invalidatedObjects)
			{
				this._highlighter.HighlightPrimary(target, this._previewShowerSpec.WarningPreview);
			}
			this._measurableAreaDrawer.AddMeasurableCoordinates(this._areaCoordinates);
			this._invalidatedObjects.Clear();
			this._areaCoordinates.Clear();
			warningMessage = PreviewShower.BuildWarningMessage(warningMessages);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000042B8 File Offset: 0x000024B8
		public void ShowBuildablePreview(Preview preview, PreviewState previewState, HashSet<string> warningMessages)
		{
			Color color = this._previewShowerSpec.BuildablePreview;
			preview.GetComponents<IPreviewValidator>(this._previewValidatorsCache);
			foreach (IPreviewValidator previewValidator in this._previewValidatorsCache)
			{
				string item;
				if (this.PreviewIsInvalid(previewValidator, out item))
				{
					warningMessages.Add(item);
					color = this._previewShowerSpec.WarningPreview;
				}
			}
			this._previewValidatorsCache.Clear();
			this.ShowPreview(preview, previewState, color);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00004350 File Offset: 0x00002550
		public bool PreviewIsInvalid(IPreviewValidator previewValidator, out string warningMessage)
		{
			string text;
			bool flag = this.CheckAndAddInvalidatedObjects(previewValidator, out text);
			string text2;
			if (!previewValidator.IsValid(out text2))
			{
				warningMessage = text2;
				return true;
			}
			if (flag)
			{
				warningMessage = text;
				return true;
			}
			warningMessage = null;
			return false;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00004384 File Offset: 0x00002584
		public bool CheckAndAddInvalidatedObjects(IPreviewValidator previewValidator, out string warningMessage)
		{
			bool result = false;
			foreach (BaseComponent item in previewValidator.InvalidatedObjects(out warningMessage))
			{
				this._invalidatedObjects.Add(item);
				result = true;
			}
			return result;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000043E8 File Offset: 0x000025E8
		public static string BuildWarningMessage(IEnumerable<string> warningMessages)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string value in warningMessages)
			{
				stringBuilder.AppendLine(value);
			}
			return stringBuilder.ToStringWithoutNewLineEnd();
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004440 File Offset: 0x00002640
		public void ShowUnbuildablePreviews(IReadOnlyList<Preview> previews, bool showAsSingle)
		{
			for (int i = 0; i < previews.Count; i++)
			{
				Preview preview = previews[i];
				PreviewState previewState = showAsSingle ? PreviewState.UnbuildableSingle : ((i == previews.Count - 1) ? PreviewState.UnbuildableLast : PreviewState.UnbuildableNotLast);
				this.ShowPreview(preview, previewState, this._previewShowerSpec.UnbuildablePreview);
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x0000449B File Offset: 0x0000269B
		public void ShowPreview(Preview preview, PreviewState previewState, Color color)
		{
			this.NotifyPrePreviewShownListeners(preview);
			preview.Show(previewState);
			this._highlighter.HighlightPrimary(preview, color);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000044B8 File Offset: 0x000026B8
		public void NotifyPrePreviewShownListeners(Preview preview)
		{
			preview.GetComponents<IPrePreviewShownListener>(this._prePreviewShownListeners);
			foreach (IPrePreviewShownListener prePreviewShownListener in this._prePreviewShownListeners)
			{
				prePreviewShownListener.OnPrePreviewShown();
			}
			this._prePreviewShownListeners.Clear();
		}

		// Token: 0x04000086 RID: 134
		public readonly Highlighter _highlighter;

		// Token: 0x04000087 RID: 135
		public readonly ISpecService _specService;

		// Token: 0x04000088 RID: 136
		public readonly MeasurableAreaDrawer _measurableAreaDrawer;

		// Token: 0x04000089 RID: 137
		public readonly HashSet<BaseComponent> _invalidatedObjects = new HashSet<BaseComponent>();

		// Token: 0x0400008A RID: 138
		public readonly List<IPreviewValidator> _previewValidatorsCache = new List<IPreviewValidator>();

		// Token: 0x0400008B RID: 139
		public readonly List<IPrePreviewShownListener> _prePreviewShownListeners = new List<IPrePreviewShownListener>();

		// Token: 0x0400008C RID: 140
		public readonly List<Vector3Int> _areaCoordinates = new List<Vector3Int>();

		// Token: 0x0400008D RID: 141
		public PreviewShowerSpec _previewShowerSpec;
	}
}
