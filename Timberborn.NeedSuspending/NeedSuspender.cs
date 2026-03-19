using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BaseComponentSystem;
using Timberborn.BlueprintSystem;
using Timberborn.NeedSystem;

namespace Timberborn.NeedSuspending
{
	// Token: 0x02000009 RID: 9
	public class NeedSuspender : IEquatable<NeedSuspender>
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000022ED File Offset: 0x000004ED
		[Nullable(1)]
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(NeedSuspender);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000022F9 File Offset: 0x000004F9
		// (set) Token: 0x0600001D RID: 29 RVA: 0x00002301 File Offset: 0x00000501
		[Serialize]
		public ImmutableArray<string> SuspendableNeedIds { get; set; }

		// Token: 0x0600001E RID: 30 RVA: 0x0000230A File Offset: 0x0000050A
		public void SuspendNeeds(BaseComponent component)
		{
			this.UpdateNeedSuspensions(component, true);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002314 File Offset: 0x00000514
		public void ResumeNeeds(BaseComponent component)
		{
			this.UpdateNeedSuspensions(component, false);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002320 File Offset: 0x00000520
		public void UpdateNeedSuspensions(BaseComponent component, bool shouldSuspend)
		{
			NeedManager component2 = component.GetComponent<NeedManager>();
			if (component2 != null)
			{
				foreach (string needId in this.SuspendableNeedIds)
				{
					if (shouldSuspend)
					{
						component2.DisableUpdate(needId);
					}
					else
					{
						component2.EnableUpdate(needId);
					}
				}
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000236C File Offset: 0x0000056C
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("NeedSuspender");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000023B8 File Offset: 0x000005B8
		[NullableContext(1)]
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("SuspendableNeedIds = ");
			builder.Append(this.SuspendableNeedIds.ToString());
			return true;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000023F2 File Offset: 0x000005F2
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(NeedSuspender left, NeedSuspender right)
		{
			return !(left == right);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000023FE File Offset: 0x000005FE
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(NeedSuspender left, NeedSuspender right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002412 File Offset: 0x00000612
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<ImmutableArray<string>>.Default.GetHashCode(this.<SuspendableNeedIds>k__BackingField);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000243B File Offset: 0x0000063B
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as NeedSuspender);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002449 File Offset: 0x00000649
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(NeedSuspender other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<ImmutableArray<string>>.Default.Equals(this.<SuspendableNeedIds>k__BackingField, other.<SuspendableNeedIds>k__BackingField));
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002487 File Offset: 0x00000687
		[CompilerGenerated]
		protected NeedSuspender([Nullable(1)] NeedSuspender original)
		{
			this.SuspendableNeedIds = original.<SuspendableNeedIds>k__BackingField;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000020F3 File Offset: 0x000002F3
		public NeedSuspender()
		{
		}
	}
}
