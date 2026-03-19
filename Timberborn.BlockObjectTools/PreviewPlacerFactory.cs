using System;
using Timberborn.BlockSystem;

namespace Timberborn.BlockObjectTools
{
	// Token: 0x02000028 RID: 40
	public class PreviewPlacerFactory
	{
		// Token: 0x060000CF RID: 207 RVA: 0x00003FA3 File Offset: 0x000021A3
		public PreviewPlacerFactory(PreviewShower previewShower, PreviewFactory previewFactory, BlockObjectValidationService blockObjectValidationService)
		{
			this._previewShower = previewShower;
			this._previewFactory = previewFactory;
			this._blockObjectValidationService = blockObjectValidationService;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00003FC0 File Offset: 0x000021C0
		public PreviewPlacer Create(PlaceableBlockObjectSpec placeableBlockObjectSpec)
		{
			int previewCount = placeableBlockObjectSpec.Layout.GetPreviewCount();
			Preview firstPreview = this.EagerlyCreateFirstPreview(placeableBlockObjectSpec);
			Lazy<Preview[]> previews = this.LazyCreateRemainingPreviews(placeableBlockObjectSpec, previewCount, firstPreview);
			bool treatPreviewsAsSingle = placeableBlockObjectSpec.Layout.ShouldShowAllPreviews();
			return new PreviewPlacer(this._previewShower, this._blockObjectValidationService, treatPreviewsAsSingle, previews);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0000400A File Offset: 0x0000220A
		public Preview EagerlyCreateFirstPreview(PlaceableBlockObjectSpec spec)
		{
			return this._previewFactory.Create(spec);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004018 File Offset: 0x00002218
		public Lazy<Preview[]> LazyCreateRemainingPreviews(PlaceableBlockObjectSpec spec, int amount, Preview firstPreview)
		{
			return new Lazy<Preview[]>(() => this.CreatePreviews(spec, amount, firstPreview));
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000404C File Offset: 0x0000224C
		public Preview[] CreatePreviews(PlaceableBlockObjectSpec spec, int amount, Preview firstPreview)
		{
			Preview[] array = new Preview[amount];
			array[0] = firstPreview;
			for (int i = 1; i < amount; i++)
			{
				Preview preview = this._previewFactory.Create(spec);
				array[i] = preview;
			}
			return array;
		}

		// Token: 0x0400007F RID: 127
		public readonly PreviewShower _previewShower;

		// Token: 0x04000080 RID: 128
		public readonly PreviewFactory _previewFactory;

		// Token: 0x04000081 RID: 129
		public readonly BlockObjectValidationService _blockObjectValidationService;
	}
}
