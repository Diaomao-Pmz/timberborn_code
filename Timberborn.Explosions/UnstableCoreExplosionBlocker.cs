using System;
using Timberborn.BaseComponentSystem;
using Timberborn.MapStateSystem;

namespace Timberborn.Explosions
{
	// Token: 0x0200001C RID: 28
	public class UnstableCoreExplosionBlocker : BaseComponent
	{
		// Token: 0x060000C9 RID: 201 RVA: 0x00004458 File Offset: 0x00002658
		public UnstableCoreExplosionBlocker(MapEditorMode mapEditorMode)
		{
			this._mapEditorMode = mapEditorMode;
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00004467 File Offset: 0x00002667
		public bool ExplosionBlocked
		{
			get
			{
				return base.Enabled && (this._explosionBlocked || this._mapEditorMode.IsMapEditor);
			}
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00004488 File Offset: 0x00002688
		public void BlockExplosion()
		{
			this._explosionBlocked = true;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00004491 File Offset: 0x00002691
		public void Disable()
		{
			base.DisableComponent();
		}

		// Token: 0x04000081 RID: 129
		public readonly MapEditorMode _mapEditorMode;

		// Token: 0x04000082 RID: 130
		public bool _explosionBlocked;
	}
}
