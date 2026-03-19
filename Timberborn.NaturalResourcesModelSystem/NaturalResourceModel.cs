using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Coordinates;
using Timberborn.Cutting;
using Timberborn.EntitySystem;
using Timberborn.Gathering;
using Timberborn.Growing;
using Timberborn.NaturalResourcesLifecycle;
using Timberborn.TransformControl;
using UnityEngine;

namespace Timberborn.NaturalResourcesModelSystem
{
	// Token: 0x02000008 RID: 8
	public class NaturalResourceModel : BaseComponent, IAwakableComponent, IStartableComponent, IRegisteredComponent, IPostInitializableEntity
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600000A RID: 10 RVA: 0x00002154 File Offset: 0x00000354
		// (remove) Token: 0x0600000B RID: 11 RVA: 0x0000218C File Offset: 0x0000038C
		public event EventHandler ModelChanged;

		// Token: 0x0600000C RID: 12 RVA: 0x000021C1 File Offset: 0x000003C1
		public void Awake()
		{
			this._growable = base.GetComponent<Growable>();
			this._cuttable = base.GetComponent<Cuttable>();
			this._gatherable = base.GetComponent<Gatherable>();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021E8 File Offset: 0x000003E8
		public void Start()
		{
			base.GetComponent<TransformController>().AddPositionModifier().Set(NaturalResourceModel.GroundCenterWorld);
			for (int i = 0; i < base.Transform.childCount; i++)
			{
				base.Transform.GetChild(i).localPosition -= NaturalResourceModel.GroundCenterWorld;
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002241 File Offset: 0x00000441
		public void PostInitializeEntity()
		{
			this.SubscribeToEvents();
			this.ShowCurrentModel();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000224F File Offset: 0x0000044F
		public void Show()
		{
			this.SetVisibility(true);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002258 File Offset: 0x00000458
		public void Hide()
		{
			this.SetVisibility(false);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002264 File Offset: 0x00000464
		public void SubscribeToEvents()
		{
			if (this._growable)
			{
				this._growable.HasGrown += delegate(object _, EventArgs _)
				{
					this.ShowCurrentModel();
				};
			}
			if (this._cuttable)
			{
				this._cuttable.WasCut += delegate(object _, EventArgs _)
				{
					this.ShowCurrentModel();
				};
			}
			if (this._gatherable)
			{
				this._gatherable.Yielder.YieldAdded += delegate(object _, EventArgs _)
				{
					this.ShowCurrentModel();
				};
				this._gatherable.Gathered += delegate(object _, EventArgs _)
				{
					this.ShowCurrentModel();
				};
			}
			LivingNaturalResource component = base.GetComponent<LivingNaturalResource>();
			component.Died += delegate(object _, EventArgs _)
			{
				this.ShowCurrentModel();
			};
			component.ReversedDeath += delegate(object _, EventArgs _)
			{
				this.ShowCurrentModel();
			};
			DyingNaturalResource component2 = base.GetComponent<DyingNaturalResource>();
			component2.StartedDying += delegate(object _, EventArgs _)
			{
				this.ShowCurrentModel();
			};
			component2.StoppedDying += delegate(object _, EventArgs _)
			{
				this.ShowCurrentModel();
			};
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000234C File Offset: 0x0000054C
		public void ShowCurrentModel()
		{
			this.HideModels();
			if (this._growable && !this._growable.IsGrown)
			{
				this._growable.ShowSeedlingModel();
			}
			else if (this._cuttable && this._cuttable.Yielder.IsYieldRemoved)
			{
				this._cuttable.ShowLeftoverModel();
			}
			else
			{
				this._growable.ShowMatureModel();
			}
			Gatherable gatherable = this._gatherable;
			if (gatherable != null)
			{
				gatherable.UpdateModel();
			}
			EventHandler modelChanged = this.ModelChanged;
			if (modelChanged == null)
			{
				return;
			}
			modelChanged(this, EventArgs.Empty);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000023E4 File Offset: 0x000005E4
		public void HideModels()
		{
			if (this._growable)
			{
				this._growable.HideModel();
			}
			if (this._cuttable)
			{
				this._cuttable.HideLeftoverModel();
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002418 File Offset: 0x00000618
		public void SetVisibility(bool visible)
		{
			MeshRenderer[] componentsInChildren = base.GameObject.GetComponentsInChildren<MeshRenderer>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].enabled = visible;
			}
		}

		// Token: 0x0400000A RID: 10
		public static readonly Vector3 GroundCenterWorld = CoordinateSystem.GridToWorld(new Vector3(0.5f, 0.5f, 0f));

		// Token: 0x0400000C RID: 12
		public Growable _growable;

		// Token: 0x0400000D RID: 13
		public Cuttable _cuttable;

		// Token: 0x0400000E RID: 14
		public Gatherable _gatherable;
	}
}
