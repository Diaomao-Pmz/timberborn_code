using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;

namespace Timberborn.SpriteOperations
{
	// Token: 0x0200000F RID: 15
	public class UISprite : IEquatable<UISprite>
	{
		// Token: 0x06000032 RID: 50 RVA: 0x0000273B File Offset: 0x0000093B
		public UISprite(Sprite Value)
		{
			this.Value = Value;
			base..ctor();
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000033 RID: 51 RVA: 0x0000274A File Offset: 0x0000094A
		[Nullable(1)]
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(UISprite);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002756 File Offset: 0x00000956
		// (set) Token: 0x06000035 RID: 53 RVA: 0x0000275E File Offset: 0x0000095E
		public Sprite Value { get; set; }

		// Token: 0x06000036 RID: 54 RVA: 0x00002768 File Offset: 0x00000968
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("UISprite");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000027B4 File Offset: 0x000009B4
		[NullableContext(1)]
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("Value = ");
			builder.Append(this.Value);
			return true;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000027D5 File Offset: 0x000009D5
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(UISprite left, UISprite right)
		{
			return !(left == right);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000027E1 File Offset: 0x000009E1
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(UISprite left, UISprite right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000027F5 File Offset: 0x000009F5
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<Sprite>.Default.GetHashCode(this.<Value>k__BackingField);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000281E File Offset: 0x00000A1E
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as UISprite);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000282C File Offset: 0x00000A2C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(UISprite other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<Sprite>.Default.Equals(this.<Value>k__BackingField, other.<Value>k__BackingField));
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000286A File Offset: 0x00000A6A
		[CompilerGenerated]
		protected UISprite([Nullable(1)] UISprite original)
		{
			this.Value = original.<Value>k__BackingField;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000287E File Offset: 0x00000A7E
		[CompilerGenerated]
		public void Deconstruct(out Sprite Value)
		{
			Value = this.Value;
		}
	}
}
