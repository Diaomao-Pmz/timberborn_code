using System;
using Timberborn.BaseComponentSystem;
using Timberborn.NaturalResourcesLifecycle;

namespace Timberborn.NaturalResourcesReproduction
{
	// Token: 0x02000009 RID: 9
	public class LivingReproducible : BaseComponent, IAwakableComponent
	{
		// Token: 0x0600000C RID: 12 RVA: 0x00002180 File Offset: 0x00000380
		public void Awake()
		{
			LivingNaturalResource component = base.GetComponent<LivingNaturalResource>();
			Reproducible reproducible = base.GetComponent<Reproducible>();
			component.Died += delegate(object _, EventArgs _)
			{
				reproducible.BlockReproduction(this);
			};
			component.ReversedDeath += delegate(object _, EventArgs _)
			{
				reproducible.UnblockReproduction(this);
			};
		}
	}
}
