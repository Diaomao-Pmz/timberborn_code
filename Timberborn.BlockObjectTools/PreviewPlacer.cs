using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BlockObjectModelSystem;
using Timberborn.BlockSystem;
using Timberborn.Coordinates;

namespace Timberborn.BlockObjectTools
{
	// Token: 0x02000025 RID: 37
	public class PreviewPlacer
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000AC RID: 172 RVA: 0x0000388D File Offset: 0x00001A8D
		// (set) Token: 0x060000AD RID: 173 RVA: 0x00003895 File Offset: 0x00001A95
		public string WarningText { get; private set; } = "";

		// Token: 0x060000AE RID: 174 RVA: 0x000038A0 File Offset: 0x00001AA0
		public PreviewPlacer(PreviewShower previewShower, BlockObjectValidationService blockObjectValidationService, bool treatPreviewsAsSingle, Lazy<Preview[]> previews)
		{
			this._previewShower = previewShower;
			this._blockObjectValidationService = blockObjectValidationService;
			this._previews = previews;
			this._treatPreviewsAsSingle = treatPreviewsAsSingle;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000038F4 File Offset: 0x00001AF4
		public void ShowPreviews(IEnumerable<Placement> placements)
		{
			this.WarningText = string.Empty;
			this._previewShower.UnhighlightAllPreviews();
			this._coordinatesCache.AddRange(placements);
			if (this._coordinatesCache.Count > 0)
			{
				int num;
				this.PopulateBuildablePreviewsAndAddToBlockServices(this._coordinatesCache, out num);
				if (this._treatPreviewsAsSingle)
				{
					this.ShowAllPreviews(this._buildablePreviews);
				}
				else if (num == 1)
				{
					this.HideRemainingPreviews(this._buildablePreviews, true);
					this.ShowSinglePreview(this._buildablePreviews);
				}
				else
				{
					this.HideRemainingPreviews(this._buildablePreviews, false);
					this.ShowBuildablePreviews(this._buildablePreviews);
				}
			}
			else
			{
				this.HideAllPreviews();
			}
			this._buildablePreviews.Clear();
			this._coordinatesCache.Clear();
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000039AB File Offset: 0x00001BAB
		public IEnumerable<Placement> GetBuildableCoordinates(IEnumerable<Placement> placements)
		{
			int num;
			this.PopulateBuildablePreviewsAndAddToBlockServices(placements, out num);
			bool flag = this.PreviewsAreValid(this._buildablePreviews);
			this.RemovePreviewsFromServices();
			if (flag)
			{
				foreach (Preview preview in this._buildablePreviews)
				{
					yield return preview.BlockObject.Placement;
				}
				List<Preview>.Enumerator enumerator = default(List<Preview>.Enumerator);
			}
			this._buildablePreviews.Clear();
			yield break;
			yield break;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000039C2 File Offset: 0x00001BC2
		public void HideAllPreviews()
		{
			this.RemovePreviewsFromServices();
			this._previewShower.HidePreviews(this.Previews);
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x000039DB File Offset: 0x00001BDB
		public Preview[] Previews
		{
			get
			{
				return this._previews.Value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x000039E8 File Offset: 0x00001BE8
		public Preview SinglePreview
		{
			get
			{
				return this.Previews[0];
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000039F2 File Offset: 0x00001BF2
		public bool PreviewsAreValid(IReadOnlyList<Preview> buildablePreviews)
		{
			return this._blockObjectValidationService.AreValid(buildablePreviews) && (!this._treatPreviewsAsSingle || buildablePreviews.Count == this.Previews.Length);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003A20 File Offset: 0x00001C20
		public void PopulateBuildablePreviewsAndAddToBlockServices(IEnumerable<Placement> placements, out int blocksCount)
		{
			this.RemovePreviewsFromServices();
			foreach (Preview preview in this.GetPositionedBuildablePreviews(placements, out blocksCount))
			{
				preview.AddToPreviewServices();
				this._buildablePreviews.Add(preview);
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003A80 File Offset: 0x00001C80
		public IEnumerable<Preview> GetPositionedBuildablePreviews(IEnumerable<Placement> placements, out int blocksCount)
		{
			return from preview in this.GetPositionedPreviews(placements, out blocksCount)
			where preview.BlockObject.IsValid()
			select preview;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003AB0 File Offset: 0x00001CB0
		public IEnumerable<Preview> GetPositionedPreviews(IEnumerable<Placement> placements, out int blocksCount)
		{
			blocksCount = 0;
			foreach (Placement placement in placements)
			{
				Preview[] previews = this.Previews;
				int num = blocksCount;
				blocksCount = num + 1;
				previews[num].Reposition(placement);
			}
			return this.Previews.Take(blocksCount);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003B18 File Offset: 0x00001D18
		public void RemovePreviewsFromServices()
		{
			Preview[] previews = this.Previews;
			for (int i = 0; i < previews.Length; i++)
			{
				previews[i].RemoveFromPreviewServices();
			}
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00003B44 File Offset: 0x00001D44
		public void ShowAllPreviews(IReadOnlyCollection<Preview> buildablePreviews)
		{
			if (buildablePreviews.Count == this.Previews.Length)
			{
				this.ShowBuildablePreviews(this.Previews);
				return;
			}
			if (this.AnyPreviewIsAlmostValid())
			{
				this.ShowUnbuildablePreviews(this.Previews);
				this.WarningText = this.GetWarningText(this.Previews);
				PreviewPlacer.UpdateModels(this.Previews);
				return;
			}
			this.HideAllPreviews();
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00003BA8 File Offset: 0x00001DA8
		public bool AnyPreviewIsAlmostValid()
		{
			Preview[] previews = this.Previews;
			for (int i = 0; i < previews.Length; i++)
			{
				if (PreviewPlacer.PreviewAlmostValid(previews[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00003BD8 File Offset: 0x00001DD8
		public void ShowSinglePreview(IReadOnlyCollection<Preview> buildablePreviews)
		{
			if (buildablePreviews.Count > 0)
			{
				this.ShowBuildablePreviews(new Preview[]
				{
					this.SinglePreview
				});
				return;
			}
			if (PreviewPlacer.PreviewAlmostValid(this.SinglePreview))
			{
				this.WarningText = this.GetWarningText(new Preview[]
				{
					this.SinglePreview
				});
				this._previewShower.ShowUnbuildablePreview(this.SinglePreview);
				this.SinglePreview.GetComponent<BlockObjectModelController>().UpdateModel();
				return;
			}
			PreviewPlacer.HideAndRemoveFromServices(this.SinglePreview);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00003C59 File Offset: 0x00001E59
		public static bool PreviewAlmostValid(Preview preview)
		{
			return preview.BlockObject.IsAlmostValid();
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003C68 File Offset: 0x00001E68
		public void HideRemainingPreviews(IReadOnlyCollection<Preview> buildablePreviews, bool exceptSinglePreview)
		{
			foreach (Preview preview in this.Previews.Except(buildablePreviews))
			{
				if (preview != this.SinglePreview || !exceptSinglePreview)
				{
					PreviewPlacer.HideAndRemoveFromServices(preview);
				}
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00003CC8 File Offset: 0x00001EC8
		public void ShowBuildablePreviews(IReadOnlyList<Preview> previews)
		{
			string warningText;
			if (this._blockObjectValidationService.AreValid(previews, out warningText))
			{
				if (this._treatPreviewsAsSingle)
				{
					this._previewShower.ShowBuildablePreviewsAsSingle(previews, out warningText);
				}
				else
				{
					this._previewShower.ShowBuildablePreviews(previews, out warningText);
				}
			}
			else
			{
				this.ShowUnbuildablePreviews(previews);
			}
			PreviewPlacer.UpdateModels(previews);
			this.WarningText = warningText;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00003D24 File Offset: 0x00001F24
		public string GetWarningText(IReadOnlyList<Preview> previews)
		{
			string result;
			this._blockObjectValidationService.AreValid(previews, out result);
			return result;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00003D41 File Offset: 0x00001F41
		public void ShowUnbuildablePreviews(IReadOnlyList<Preview> previews)
		{
			if (this._treatPreviewsAsSingle)
			{
				this._previewShower.ShowUnbuildablePreviewsAsSingle(previews);
				return;
			}
			this._previewShower.ShowUnbuildablePreviews(previews);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00003D64 File Offset: 0x00001F64
		public static void UpdateModels(IReadOnlyList<Preview> previews)
		{
			foreach (Preview preview in previews)
			{
				preview.GetComponent<BlockObjectModelController>().UpdateModel();
			}
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00003DB0 File Offset: 0x00001FB0
		public static void HideAndRemoveFromServices(Preview preview)
		{
			preview.RemoveFromPreviewServices();
			preview.Hide();
		}

		// Token: 0x04000070 RID: 112
		public readonly PreviewShower _previewShower;

		// Token: 0x04000071 RID: 113
		public readonly BlockObjectValidationService _blockObjectValidationService;

		// Token: 0x04000072 RID: 114
		public readonly bool _treatPreviewsAsSingle;

		// Token: 0x04000073 RID: 115
		public readonly Lazy<Preview[]> _previews;

		// Token: 0x04000074 RID: 116
		public readonly List<Placement> _coordinatesCache = new List<Placement>();

		// Token: 0x04000075 RID: 117
		public readonly List<Preview> _buildablePreviews = new List<Preview>();
	}
}
