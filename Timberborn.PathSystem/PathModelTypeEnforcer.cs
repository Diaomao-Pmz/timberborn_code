using System;
using Timberborn.BaseComponentSystem;

namespace Timberborn.PathSystem
{
	// Token: 0x02000016 RID: 22
	public class PathModelTypeEnforcer : BaseComponent, IAwakableComponent
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000078 RID: 120 RVA: 0x0000351C File Offset: 0x0000171C
		public PathModelType PathModelType
		{
			get
			{
				return this._pathModelTypeEnforcerSpec.PathModelType;
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003529 File Offset: 0x00001729
		public void Awake()
		{
			this._pathModelTypeEnforcerSpec = base.GetComponent<PathModelTypeEnforcerSpec>();
		}

		// Token: 0x0400004A RID: 74
		public PathModelTypeEnforcerSpec _pathModelTypeEnforcerSpec;
	}
}
