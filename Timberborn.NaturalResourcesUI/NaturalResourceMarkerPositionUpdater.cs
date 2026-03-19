using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.EntitySystem;
using Timberborn.NaturalResources;
using Timberborn.NaturalResourcesModelSystem;
using Timberborn.Rendering;

namespace Timberborn.NaturalResourcesUI
{
	// Token: 0x02000007 RID: 7
	public class NaturalResourceMarkerPositionUpdater : BaseComponent, IAwakableComponent
	{
		// Token: 0x06000015 RID: 21 RVA: 0x000022AB File Offset: 0x000004AB
		public void Awake()
		{
			this._coordinatesOffsetter = base.GetComponent<CoordinatesOffsetter>();
			this._markerPosition = base.GetComponent<MarkerPosition>();
			this._entityComponent = base.GetComponent<EntityComponent>();
			base.GetComponent<NaturalResourceModel>().ModelChanged += this.OnModelChanged;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000022E8 File Offset: 0x000004E8
		public void OnModelChanged(object sender, EventArgs e)
		{
			if (!this._entityComponent.Deleted)
			{
				this._markerPosition.UpdatePosition(this._coordinatesOffsetter.CoordinatesOffset.XYZ());
			}
		}

		// Token: 0x04000012 RID: 18
		public CoordinatesOffsetter _coordinatesOffsetter;

		// Token: 0x04000013 RID: 19
		public MarkerPosition _markerPosition;

		// Token: 0x04000014 RID: 20
		public EntityComponent _entityComponent;
	}
}
