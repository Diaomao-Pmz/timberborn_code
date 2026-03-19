using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Bots;
using Timberborn.Illumination;

namespace Timberborn.TubeSystem
{
	// Token: 0x0200000A RID: 10
	public class TubeIlluminator : BaseComponent, IAwakableComponent, IFinishedStateListener
	{
		// Token: 0x0600001E RID: 30 RVA: 0x00002415 File Offset: 0x00000615
		public TubeIlluminator(BotColors botColors)
		{
			this._botColors = botColors;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002424 File Offset: 0x00000624
		public void Awake()
		{
			this._tube = base.GetComponent<Tube>();
			this._blockObject = base.GetComponent<BlockObject>();
			Illuminator component = base.GetComponent<Illuminator>();
			this._illuminatorToggle = component.CreateToggle();
			this._illuminatorColorizer = component.CreateColorizer(40);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000246A File Offset: 0x0000066A
		public void OnEnterFinishedState()
		{
			this._tube.VisitorsChanged += this.UpdateIlluminator;
			this.UpdateIlluminator();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002489 File Offset: 0x00000689
		public void OnExitFinishedState()
		{
			this._tube.VisitorsChanged -= this.UpdateIlluminator;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000024A2 File Offset: 0x000006A2
		public void UpdateIlluminator(object sender, EventArgs eventArgs)
		{
			this.UpdateIlluminator();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000024AC File Offset: 0x000006AC
		public void UpdateIlluminator()
		{
			if (this._blockObject.IsFinished)
			{
				if (this._tube.HasAnyVisitor)
				{
					if (this._tube.HasBotVisitor)
					{
						this._illuminatorColorizer.SetColor(this._botColors.BotIlluminationColor);
					}
					else
					{
						this._illuminatorColorizer.ClearColor();
					}
					this._illuminatorToggle.TurnOn();
					return;
				}
				this._illuminatorToggle.TurnOff();
			}
		}

		// Token: 0x04000014 RID: 20
		public readonly BotColors _botColors;

		// Token: 0x04000015 RID: 21
		public Tube _tube;

		// Token: 0x04000016 RID: 22
		public BlockObject _blockObject;

		// Token: 0x04000017 RID: 23
		public Illuminator _illuminator;

		// Token: 0x04000018 RID: 24
		public IlluminatorToggle _illuminatorToggle;

		// Token: 0x04000019 RID: 25
		public IlluminatorColorizer _illuminatorColorizer;
	}
}
