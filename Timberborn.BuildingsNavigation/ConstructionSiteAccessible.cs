using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockObjectAccesses;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.MapStateSystem;
using Timberborn.Navigation;

namespace Timberborn.BuildingsNavigation
{
	// Token: 0x02000012 RID: 18
	public class ConstructionSiteAccessible : BaseComponent, IAwakableComponent, IUnfinishedStateListener, IPreviewSelectionListener, INavMeshListener, IAccessibleNeeder
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000065 RID: 101 RVA: 0x0000323E File Offset: 0x0000143E
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00003246 File Offset: 0x00001446
		public Accessible Accessible { get; private set; }

		// Token: 0x06000067 RID: 103 RVA: 0x0000324F File Offset: 0x0000144F
		public ConstructionSiteAccessible(INavMeshListenerEntityRegistry navMeshListenerEntityRegistry, MapSize mapSize)
		{
			this._navMeshListenerEntityRegistry = navMeshListenerEntityRegistry;
			this._mapSize = mapSize;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00003265 File Offset: 0x00001465
		public string AccessibleComponentName
		{
			get
			{
				return "ConstructionSite";
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x0000326C File Offset: 0x0000146C
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this._blockObjectAccessGenerator = base.GetComponent<BlockObjectAccessGenerator>();
			this._preview = base.GetComponent<Preview>();
			this._constructionSiteAccessProvider = base.GetComponent<IConstructionSiteAccessProvider>();
			base.DisableComponent();
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000032A4 File Offset: 0x000014A4
		public void SetAccessible(Accessible accessible)
		{
			this.Accessible = accessible;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000032AD File Offset: 0x000014AD
		public void OnEnterUnfinishedState()
		{
			this.UpdateAccesses();
			this._bounds = this._blockObjectAccessGenerator.GenerateAccessBounds(this.MinZ, this.MaxZ);
			this._navMeshListenerEntityRegistry.RegisterNavMeshListener(this);
			base.EnableComponent();
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000032E4 File Offset: 0x000014E4
		public void OnExitUnfinishedState()
		{
			this.DisableAccesses();
			this._navMeshListenerEntityRegistry.UnregisterNavMeshListener(this);
			base.DisableComponent();
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003300 File Offset: 0x00001500
		public void OnPreviewSelect()
		{
			if (this._preview.PreviewState.IsSingle)
			{
				this.UpdateAccesses();
				return;
			}
			this.DisableAccesses();
		}

		// Token: 0x0600006E RID: 110 RVA: 0x0000332F File Offset: 0x0000152F
		public void OnPreviewUnselect()
		{
			this.DisableAccesses();
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003338 File Offset: 0x00001538
		public void OnNavMeshUpdated(NavMeshUpdate navMeshUpdate)
		{
			BoundingBox bounds = navMeshUpdate.Bounds;
			if (this._bounds.Intersects(bounds))
			{
				this.UpdateAccesses();
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00003364 File Offset: 0x00001564
		public int MinZ
		{
			get
			{
				return this._blockObject.CoordinatesAtBaseZ.z - 1;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00003388 File Offset: 0x00001588
		public int MaxZ
		{
			get
			{
				return this._mapSize.TotalSize.z - 1;
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000033AC File Offset: 0x000015AC
		public void UpdateAccesses()
		{
			this.Accessible.SetAccesses((this._constructionSiteAccessProvider != null) ? this._constructionSiteAccessProvider.GetAccesses() : this._blockObjectAccessGenerator.GenerateAccesses(this.MinZ, this.MaxZ), null);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000033F9 File Offset: 0x000015F9
		public void DisableAccesses()
		{
			this.Accessible.ClearAccesses();
		}

		// Token: 0x0400003E RID: 62
		public readonly INavMeshListenerEntityRegistry _navMeshListenerEntityRegistry;

		// Token: 0x0400003F RID: 63
		public readonly MapSize _mapSize;

		// Token: 0x04000040 RID: 64
		public BlockObject _blockObject;

		// Token: 0x04000041 RID: 65
		public BlockObjectAccessGenerator _blockObjectAccessGenerator;

		// Token: 0x04000042 RID: 66
		public Preview _preview;

		// Token: 0x04000043 RID: 67
		public BoundingBox _bounds;

		// Token: 0x04000044 RID: 68
		public IConstructionSiteAccessProvider _constructionSiteAccessProvider;
	}
}
