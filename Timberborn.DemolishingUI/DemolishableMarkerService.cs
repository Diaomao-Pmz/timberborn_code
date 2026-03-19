using System;
using System.Collections.Generic;
using Timberborn.BlockObjectModelSystem;
using Timberborn.BlueprintSystem;
using Timberborn.CameraSystem;
using Timberborn.Demolishing;
using Timberborn.Rendering;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.DemolishingUI
{
	// Token: 0x02000008 RID: 8
	public class DemolishableMarkerService : ILoadableSingleton, IUpdatableSingleton
	{
		// Token: 0x06000010 RID: 16 RVA: 0x00002481 File Offset: 0x00000681
		public DemolishableMarkerService(EventBus eventBus, MeshDrawerFactory meshDrawerFactory, CameraService cameraService, ISpecService specService)
		{
			this._eventBus = eventBus;
			this._meshDrawerFactory = meshDrawerFactory;
			this._cameraService = cameraService;
			this._specService = specService;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000024B4 File Offset: 0x000006B4
		public void Load()
		{
			this._eventBus.Register(this);
			DemolishableMarkerServiceSpec singleSpec = this._specService.GetSingleSpec<DemolishableMarkerServiceSpec>();
			this._meshDrawer = this._meshDrawerFactory.Create(singleSpec.Mesh.Asset, singleSpec.Material.Asset);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002500 File Offset: 0x00000700
		public void UpdateSingleton()
		{
			this.DrawMarkers();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002508 File Offset: 0x00000708
		[OnEvent]
		public void OnDemolishableMarked(DemolishableMarkedEvent demolishableMarkedEvent)
		{
			Demolishable demolishable = demolishableMarkedEvent.Demolishable;
			this._marked.Add(demolishable.GetComponent<MarkerPosition>());
			BlockObjectModelController component = demolishable.GetComponent<BlockObjectModelController>();
			if (component != null)
			{
				component.ModelsUpdated += this.OnModelsUpdated;
				if (!component.IsAnyModelShown || component.IsUncoveredModelShown)
				{
					this._marked.Remove(demolishable.GetComponent<MarkerPosition>());
				}
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000256C File Offset: 0x0000076C
		[OnEvent]
		public void OnDemolishableUnmarked(DemolishableUnmarkedEvent demolishableUnmarkedEvent)
		{
			Demolishable demolishable = demolishableUnmarkedEvent.Demolishable;
			this._marked.Remove(demolishable.GetComponent<MarkerPosition>());
			BlockObjectModelController component = demolishable.GetComponent<BlockObjectModelController>();
			if (component != null)
			{
				component.ModelsUpdated -= this.OnModelsUpdated;
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000025B0 File Offset: 0x000007B0
		public void DrawMarkers()
		{
			Matrix4x4 matrix = Matrix4x4.TRS(Vector3.zero, this._cameraService.FacingCamera, DemolishableMarkerService.IconScale);
			for (int i = 0; i < this._marked.Count; i++)
			{
				Vector3 position = this._marked[i].Position;
				matrix.SetColumn(3, new Vector4(position.x, position.y, position.z, 1f));
				this._meshDrawer.Draw(matrix);
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002630 File Offset: 0x00000830
		public void OnModelsUpdated(object sender, EventArgs args)
		{
			BlockObjectModelController blockObjectModelController = (BlockObjectModelController)sender;
			MarkerPosition component = blockObjectModelController.GetComponent<MarkerPosition>();
			bool flag = blockObjectModelController.IsAnyModelShown && !blockObjectModelController.IsUncoveredModelShown;
			if (flag && !this._marked.Contains(component))
			{
				this._marked.Add(component);
				return;
			}
			if (!flag)
			{
				this._marked.Remove(component);
			}
		}

		// Token: 0x0400001C RID: 28
		public static readonly Vector3 IconScale = new Vector3(0.3f, 0.3f, 0.3f);

		// Token: 0x0400001D RID: 29
		public readonly EventBus _eventBus;

		// Token: 0x0400001E RID: 30
		public readonly MeshDrawerFactory _meshDrawerFactory;

		// Token: 0x0400001F RID: 31
		public readonly CameraService _cameraService;

		// Token: 0x04000020 RID: 32
		public readonly ISpecService _specService;

		// Token: 0x04000021 RID: 33
		public MeshDrawer _meshDrawer;

		// Token: 0x04000022 RID: 34
		public readonly List<MarkerPosition> _marked = new List<MarkerPosition>();
	}
}
