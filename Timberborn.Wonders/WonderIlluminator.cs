using System;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;
using Timberborn.Illumination;

namespace Timberborn.Wonders
{
	// Token: 0x02000017 RID: 23
	public class WonderIlluminator : BaseComponent, IAwakableComponent, IInitializableEntity
	{
		// Token: 0x06000074 RID: 116 RVA: 0x00002DD4 File Offset: 0x00000FD4
		public void Awake()
		{
			this._illuminatorToggle = base.GetComponent<Illuminator>().CreateToggle();
			this._wonder = base.GetComponent<Wonder>();
			this._wonder.WonderActivated += delegate(object _, EventArgs _)
			{
				this.UpdateIlluminator();
			};
			this._wonder.WonderDeactivated += delegate(object _, EventArgs _)
			{
				this.UpdateIlluminator();
			};
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002E2C File Offset: 0x0000102C
		public void InitializeEntity()
		{
			this.UpdateIlluminator();
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002E34 File Offset: 0x00001034
		public void UpdateIlluminator()
		{
			if (this._wonder.IsActive)
			{
				this._illuminatorToggle.TurnOn();
				return;
			}
			this._illuminatorToggle.TurnOff();
		}

		// Token: 0x04000034 RID: 52
		public IlluminatorToggle _illuminatorToggle;

		// Token: 0x04000035 RID: 53
		public Wonder _wonder;
	}
}
