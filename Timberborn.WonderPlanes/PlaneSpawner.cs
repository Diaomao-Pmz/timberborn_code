using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlueprintSystem;
using Timberborn.Common;
using Timberborn.EntitySystem;
using Timberborn.Rendering;
using Timberborn.TemplateSystem;
using UnityEngine;

namespace Timberborn.WonderPlanes
{
	// Token: 0x02000010 RID: 16
	public class PlaneSpawner : BaseComponent, IAwakableComponent
	{
		// Token: 0x06000075 RID: 117 RVA: 0x000034FF File Offset: 0x000016FF
		public PlaneSpawner(TemplateService templateService, EntityService entityService, MaterialColorer materialColorer)
		{
			this._templateService = templateService;
			this._entityService = entityService;
			this._materialColorer = materialColorer;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000076 RID: 118 RVA: 0x0000351C File Offset: 0x0000171C
		public Vector3 SpawnPosition
		{
			get
			{
				return this._spawnPoint.position;
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x0000352C File Offset: 0x0000172C
		public void Awake()
		{
			this._planeTemplate = this._templateService.GetSingle<PlaneSpec>().Blueprint;
			string spawnPointName = base.GetComponent<PlaneSpawnerSpec>().SpawnPointName;
			this._spawnPoint = base.GameObject.FindChildTransform(spawnPointName);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003570 File Offset: 0x00001770
		public Plane SpawnPlane(Pilot pilot)
		{
			Plane component = this._entityService.Instantiate(this._planeTemplate).GetComponent<Plane>();
			this._materialColorer.EnableLighting(component, null);
			component.Initialize(this._spawnPoint);
			pilot.AssignPlane(component);
			return component;
		}

		// Token: 0x04000055 RID: 85
		public readonly TemplateService _templateService;

		// Token: 0x04000056 RID: 86
		public readonly EntityService _entityService;

		// Token: 0x04000057 RID: 87
		public readonly MaterialColorer _materialColorer;

		// Token: 0x04000058 RID: 88
		public Blueprint _planeTemplate;

		// Token: 0x04000059 RID: 89
		public Transform _spawnPoint;
	}
}
