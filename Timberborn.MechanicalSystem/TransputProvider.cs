using System;
using System.Collections.Immutable;
using Timberborn.BaseComponentSystem;

namespace Timberborn.MechanicalSystem
{
	// Token: 0x02000022 RID: 34
	public class TransputProvider : BaseComponent, IAwakableComponent
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00004835 File Offset: 0x00002A35
		public ImmutableArray<TransputSpec> TransputSpecs
		{
			get
			{
				return this._transputProviderSpec.Transputs;
			}
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00004842 File Offset: 0x00002A42
		public void Awake()
		{
			this._transputProviderSpec = base.GetComponent<TransputProviderSpec>();
		}

		// Token: 0x0400006D RID: 109
		public TransputProviderSpec _transputProviderSpec;
	}
}
