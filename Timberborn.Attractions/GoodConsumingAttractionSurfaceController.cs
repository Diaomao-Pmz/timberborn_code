using System;
using System.Collections.Immutable;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.EnterableSystem;
using Timberborn.EntitySystem;
using Timberborn.GoodConsumingBuildingSystem;
using Timberborn.Particles;
using Timberborn.TickSystem;
using UnityEngine;

namespace Timberborn.Attractions
{
	// Token: 0x02000012 RID: 18
	public class GoodConsumingAttractionSurfaceController : TickableComponent, IAwakableComponent, IInitializableEntity, IFinishedStateListener
	{
		// Token: 0x06000073 RID: 115 RVA: 0x00002FCC File Offset: 0x000011CC
		public void Awake()
		{
			this._goodConsumingBuilding = base.GetComponent<GoodConsumingBuilding>();
			this._enterable = base.GetComponent<Enterable>();
			this._goodConsumingAttractionSurfaceControllerSpec = base.GetComponent<GoodConsumingAttractionSurfaceControllerSpec>();
			this._surface = base.GameObject.FindChild(this._goodConsumingAttractionSurfaceControllerSpec.SurfaceName);
			base.DisableComponent();
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003020 File Offset: 0x00001220
		public void InitializeEntity()
		{
			ImmutableArray<string> attachmentIds = this._goodConsumingAttractionSurfaceControllerSpec.AttachmentIds;
			if (attachmentIds.Length > 0)
			{
				this._particlesRunner = base.GetComponent<ParticlesCache>().GetParticlesRunner(attachmentIds);
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x0000305A File Offset: 0x0000125A
		public override void Tick()
		{
			this.UpdateSurface();
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003062 File Offset: 0x00001262
		public void OnEnterFinishedState()
		{
			this.UpdateSurface();
			base.EnableComponent();
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002256 File Offset: 0x00000456
		public void OnExitFinishedState()
		{
			base.DisableComponent();
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003070 File Offset: 0x00001270
		public void UpdateSurface()
		{
			bool canUse = this._goodConsumingBuilding.CanUse;
			this._surface.SetActive(canUse);
			this.UpdateParticles(canUse);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x0000309C File Offset: 0x0000129C
		public void UpdateParticles(bool visible)
		{
			if (this._particlesRunner != null)
			{
				if (visible && this._enterable.NumberOfEnterersInside > 0)
				{
					this._particlesRunner.Enable();
					this._particlesRunner.Play();
					return;
				}
				this._particlesRunner.Stop();
				this._particlesRunner.Disable();
			}
		}

		// Token: 0x04000032 RID: 50
		public GoodConsumingBuilding _goodConsumingBuilding;

		// Token: 0x04000033 RID: 51
		public Enterable _enterable;

		// Token: 0x04000034 RID: 52
		public GoodConsumingAttractionSurfaceControllerSpec _goodConsumingAttractionSurfaceControllerSpec;

		// Token: 0x04000035 RID: 53
		public GameObject _surface;

		// Token: 0x04000036 RID: 54
		public ParticlesRunner _particlesRunner;
	}
}
