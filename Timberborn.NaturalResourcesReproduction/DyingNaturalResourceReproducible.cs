using System;
using Timberborn.BaseComponentSystem;
using Timberborn.NaturalResourcesLifecycle;

namespace Timberborn.NaturalResourcesReproduction
{
	// Token: 0x02000007 RID: 7
	public class DyingNaturalResourceReproducible : BaseComponent, IAwakableComponent
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public void Awake()
		{
			DyingNaturalResource component = base.GetComponent<DyingNaturalResource>();
			Reproducible reproducible = base.GetComponent<Reproducible>();
			component.StartedDying += delegate(object _, EventArgs _)
			{
				reproducible.BlockReproduction(this);
			};
			component.StoppedDying += delegate(object _, EventArgs _)
			{
				reproducible.UnblockReproduction(this);
			};
		}
	}
}
