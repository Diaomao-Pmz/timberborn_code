using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Illumination;

namespace Timberborn.EnterableSystem
{
	// Token: 0x0200000A RID: 10
	public class EnterableIlluminator : BaseComponent, IAwakableComponent, IStartableComponent
	{
		// Token: 0x06000039 RID: 57 RVA: 0x000027A6 File Offset: 0x000009A6
		public void Awake()
		{
			this._enterable = base.GetComponent<Enterable>();
			this._blockObject = base.GetComponent<BlockObject>();
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000027C0 File Offset: 0x000009C0
		public void Start()
		{
			this._illuminatorToggle = base.GetComponent<Illuminator>().CreateToggle();
			this._enterable.EntererAdded += this.OnEntererAdded;
			this._enterable.EntererRemoved += this.OnEntererRemoved;
			this.UpdateIlluminator();
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002812 File Offset: 0x00000A12
		public void OnEntererAdded(object sender, EntererAddedEventArgs e)
		{
			this.UpdateIlluminator();
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002812 File Offset: 0x00000A12
		public void OnEntererRemoved(object sender, EntererRemovedEventArgs e)
		{
			this.UpdateIlluminator();
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000281A File Offset: 0x00000A1A
		public void UpdateIlluminator()
		{
			if (this._blockObject.IsFinished && this._enterable.NumberOfEnterersInside > 0)
			{
				this._illuminatorToggle.TurnOn();
				return;
			}
			this._illuminatorToggle.TurnOff();
		}

		// Token: 0x04000014 RID: 20
		public Enterable _enterable;

		// Token: 0x04000015 RID: 21
		public BlockObject _blockObject;

		// Token: 0x04000016 RID: 22
		public IlluminatorToggle _illuminatorToggle;
	}
}
