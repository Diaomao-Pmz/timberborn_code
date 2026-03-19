using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;

namespace Timberborn.SpriteOperations
{
	// Token: 0x02000007 RID: 7
	public class FlippedSprite : IEquatable<FlippedSprite>
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020FE File Offset: 0x000002FE
		public FlippedSprite(Sprite Value)
		{
			this.Value = Value;
			base..ctor();
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000008 RID: 8 RVA: 0x0000210D File Offset: 0x0000030D
		[Nullable(1)]
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(FlippedSprite);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002119 File Offset: 0x00000319
		// (set) Token: 0x0600000A RID: 10 RVA: 0x00002121 File Offset: 0x00000321
		public Sprite Value { get; set; }

		// Token: 0x0600000B RID: 11 RVA: 0x0000212C File Offset: 0x0000032C
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("FlippedSprite");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002178 File Offset: 0x00000378
		[NullableContext(1)]
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("Value = ");
			builder.Append(this.Value);
			return true;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002199 File Offset: 0x00000399
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(FlippedSprite left, FlippedSprite right)
		{
			return !(left == right);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021A5 File Offset: 0x000003A5
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(FlippedSprite left, FlippedSprite right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021B9 File Offset: 0x000003B9
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<Sprite>.Default.GetHashCode(this.<Value>k__BackingField);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021E2 File Offset: 0x000003E2
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as FlippedSprite);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000021F0 File Offset: 0x000003F0
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(FlippedSprite other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<Sprite>.Default.Equals(this.<Value>k__BackingField, other.<Value>k__BackingField));
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000222E File Offset: 0x0000042E
		[CompilerGenerated]
		protected FlippedSprite([Nullable(1)] FlippedSprite original)
		{
			this.Value = original.<Value>k__BackingField;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002242 File Offset: 0x00000442
		[CompilerGenerated]
		public void Deconstruct(out Sprite Value)
		{
			Value = this.Value;
		}
	}
}
