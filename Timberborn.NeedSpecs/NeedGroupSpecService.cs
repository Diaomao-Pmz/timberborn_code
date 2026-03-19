using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.BlueprintSystem;
using Timberborn.Common;
using Timberborn.SingletonSystem;

namespace Timberborn.NeedSpecs
{
	// Token: 0x0200000D RID: 13
	public class NeedGroupSpecService : ILoadableSingleton
	{
		// Token: 0x06000062 RID: 98 RVA: 0x00002C7D File Offset: 0x00000E7D
		public NeedGroupSpecService(ISpecService specService)
		{
			this._specService = specService;
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002C8C File Offset: 0x00000E8C
		public IEnumerable<NeedGroupSpec> NeedGroups
		{
			get
			{
				return this._needGroups.AsReadOnlyEnumerable<NeedGroupSpec>();
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002C9E File Offset: 0x00000E9E
		public void Load()
		{
			this._needGroups = (from needGroupSpec in this._specService.GetSpecs<NeedGroupSpec>()
			orderby needGroupSpec.Order
			select needGroupSpec).ToImmutableArray<NeedGroupSpec>();
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002CDC File Offset: 0x00000EDC
		public bool IsValidGroup(string needGroupId)
		{
			return this._needGroups.Any((NeedGroupSpec spec) => spec.Id == needGroupId);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002D10 File Offset: 0x00000F10
		public NeedGroupSpec GetNeedGroup(string needGroupId)
		{
			NeedGroupSpec needGroupSpec2 = this._needGroups.SingleOrDefault((NeedGroupSpec needGroupSpec) => needGroupSpec.Id == needGroupId);
			if (needGroupSpec2 != null)
			{
				return needGroupSpec2;
			}
			throw new InvalidOperationException("NeedGroupSpec with id " + needGroupId + " not found or multiple specs found");
		}

		// Token: 0x0400001E RID: 30
		public readonly ISpecService _specService;

		// Token: 0x0400001F RID: 31
		public ImmutableArray<NeedGroupSpec> _needGroups;
	}
}
