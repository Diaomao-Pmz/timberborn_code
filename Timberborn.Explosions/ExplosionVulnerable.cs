using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Characters;
using Timberborn.Localization;
using Timberborn.MortalSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.Explosions
{
	// Token: 0x02000014 RID: 20
	public class ExplosionVulnerable : BaseComponent, IAwakableComponent
	{
		// Token: 0x0600007C RID: 124 RVA: 0x000038ED File Offset: 0x00001AED
		public ExplosionVulnerable(ILoc loc, EventBus eventBus)
		{
			this._loc = loc;
			this._eventBus = eventBus;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003903 File Offset: 0x00001B03
		public void Awake()
		{
			this._character = base.GetComponent<Character>();
			this._mortal = base.GetComponent<Mortal>();
		}

		// Token: 0x0600007E RID: 126 RVA: 0x0000391D File Offset: 0x00001B1D
		public void DieFromExplosion(BaseComponent source)
		{
			this._mortal.DieInstantly(this._loc.T<string>(ExplosionVulnerable.BlownInExplosionLocKey, this._character.FirstName));
			this._eventBus.Post(new MortalDiedFromExplosionEvent(source));
		}

		// Token: 0x04000056 RID: 86
		public static readonly string BlownInExplosionLocKey = "Explosions.BlownInExplosionMessage";

		// Token: 0x04000057 RID: 87
		public readonly ILoc _loc;

		// Token: 0x04000058 RID: 88
		public readonly EventBus _eventBus;

		// Token: 0x04000059 RID: 89
		public Character _character;

		// Token: 0x0400005A RID: 90
		public Mortal _mortal;
	}
}
