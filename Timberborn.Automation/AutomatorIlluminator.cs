using System;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;
using Timberborn.Illumination;

namespace Timberborn.Automation
{
	// Token: 0x02000012 RID: 18
	public class AutomatorIlluminator : BaseComponent, IAwakableComponent, IPostInitializableEntity, IAutomatorListener
	{
		// Token: 0x060000AA RID: 170 RVA: 0x00004092 File Offset: 0x00002292
		public void Awake()
		{
			this._automator = base.GetComponent<Automator>();
			this._illuminatorToggle = base.GetComponent<Illuminator>().CreateToggle();
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000040B1 File Offset: 0x000022B1
		public void PostInitializeEntity()
		{
			if (this._automator.State == AutomatorState.On)
			{
				this._illuminatorToggle.TurnOn();
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000040CC File Offset: 0x000022CC
		public void OnAutomatorStateChanged()
		{
			if (this._automator.State == AutomatorState.On)
			{
				this._illuminatorToggle.TurnOn();
				return;
			}
			this._illuminatorToggle.TurnOff();
		}

		// Token: 0x04000052 RID: 82
		public Automator _automator;

		// Token: 0x04000053 RID: 83
		public IlluminatorToggle _illuminatorToggle;
	}
}
