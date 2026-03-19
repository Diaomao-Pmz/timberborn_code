using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.EntitySystem;
using UnityEngine;

namespace Timberborn.FireworkSystem
{
	// Token: 0x0200000A RID: 10
	public class FireworkLauncherModel : BaseComponent, IAwakableComponent, IStartableComponent, IPostPlacementChangeListener, IPostInitializableEntity
	{
		// Token: 0x06000039 RID: 57 RVA: 0x00002C48 File Offset: 0x00000E48
		public void Awake()
		{
			FireworkLauncherSpec component = base.GetComponent<FireworkLauncherSpec>();
			this._fireworkLauncher = base.GetComponent<FireworkLauncher>();
			this._turret = base.GameObject.FindChildTransform(component.Turret);
			this._barrel = base.GameObject.FindChildTransform(component.Barrel);
			this.UpdateModel();
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002C9C File Offset: 0x00000E9C
		public void Start()
		{
			this.UpdateModel();
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002CA4 File Offset: 0x00000EA4
		public void PostInitializeEntity()
		{
			this._fireworkLauncher.AnglesChanged += delegate(object _, EventArgs _)
			{
				this.UpdateModel();
			};
			this.UpdateModel();
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002C9C File Offset: 0x00000E9C
		public void OnPostPlacementChanged()
		{
			this.UpdateModel();
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002CC3 File Offset: 0x00000EC3
		public Transform GetBarrelTransform()
		{
			return this._barrel.transform;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002CD0 File Offset: 0x00000ED0
		public void UpdateModel()
		{
			this._turret.localRotation = Quaternion.Euler(0f, (float)this._fireworkLauncher.Heading, 0f);
			this._barrel.localRotation = Quaternion.Euler((float)(-(float)this._fireworkLauncher.Pitch - 90), 0f, 0f);
		}

		// Token: 0x0400003D RID: 61
		public FireworkLauncher _fireworkLauncher;

		// Token: 0x0400003E RID: 62
		public Transform _turret;

		// Token: 0x0400003F RID: 63
		public Transform _barrel;
	}
}
