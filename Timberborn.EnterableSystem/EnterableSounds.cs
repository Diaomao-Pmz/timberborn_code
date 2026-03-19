using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Buildings;

namespace Timberborn.EnterableSystem
{
	// Token: 0x0200000E RID: 14
	public class EnterableSounds : BaseComponent, IAwakableComponent
	{
		// Token: 0x06000061 RID: 97 RVA: 0x00002B30 File Offset: 0x00000D30
		public void Awake()
		{
			this._buildingSounds = base.GetComponent<BuildingSounds>();
			this._enterable = base.GetComponent<Enterable>();
			this._enterable.EntererAdded += delegate(object _, EntererAddedEventArgs _)
			{
				this.UpdateSounds();
			};
			this._enterable.EntererRemoved += delegate(object _, EntererRemovedEventArgs _)
			{
				this.UpdateSounds();
			};
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002B84 File Offset: 0x00000D84
		public void UpdateSounds()
		{
			bool start = this._enterable.NumberOfEnterersInside > 0;
			this._buildingSounds.ToggleSound(start);
		}

		// Token: 0x0400001A RID: 26
		public BuildingSounds _buildingSounds;

		// Token: 0x0400001B RID: 27
		public Enterable _enterable;
	}
}
