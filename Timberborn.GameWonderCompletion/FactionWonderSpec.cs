using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using Timberborn.LocalizationSerialization;
using UnityEngine;

namespace Timberborn.GameWonderCompletion
{
	// Token: 0x02000007 RID: 7
	public class FactionWonderSpec : ComponentSpec, IEquatable<FactionWonderSpec>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020FE File Offset: 0x000002FE
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(FactionWonderSpec);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8 RVA: 0x0000210A File Offset: 0x0000030A
		// (set) Token: 0x06000009 RID: 9 RVA: 0x00002112 File Offset: 0x00000312
		[Serialize("WonderCompletionFlavorLocKey")]
		public LocalizedText WonderCompletionFlavor { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000A RID: 10 RVA: 0x0000211B File Offset: 0x0000031B
		// (set) Token: 0x0600000B RID: 11 RVA: 0x00002123 File Offset: 0x00000323
		[Serialize("WonderCompletionMessageLocKey")]
		public LocalizedText WonderCompletionMessage { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000C RID: 12 RVA: 0x0000212C File Offset: 0x0000032C
		// (set) Token: 0x0600000D RID: 13 RVA: 0x00002134 File Offset: 0x00000334
		[Serialize]
		public AssetRef<Sprite> WonderCompletionImage { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000E RID: 14 RVA: 0x0000213D File Offset: 0x0000033D
		// (set) Token: 0x0600000F RID: 15 RVA: 0x00002145 File Offset: 0x00000345
		[Serialize]
		public string WonderLaunchSound { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000010 RID: 16 RVA: 0x0000214E File Offset: 0x0000034E
		// (set) Token: 0x06000011 RID: 17 RVA: 0x00002156 File Offset: 0x00000356
		[Serialize]
		private string WonderCompletionFlavorLocKey { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000012 RID: 18 RVA: 0x0000215F File Offset: 0x0000035F
		// (set) Token: 0x06000013 RID: 19 RVA: 0x00002167 File Offset: 0x00000367
		[Serialize]
		private string WonderCompletionMessageLocKey { get; set; }

		// Token: 0x06000014 RID: 20 RVA: 0x00002170 File Offset: 0x00000370
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("FactionWonderSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000021BC File Offset: 0x000003BC
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("WonderCompletionFlavor = ");
			builder.Append(this.WonderCompletionFlavor);
			builder.Append(", WonderCompletionMessage = ");
			builder.Append(this.WonderCompletionMessage);
			builder.Append(", WonderCompletionImage = ");
			builder.Append(this.WonderCompletionImage);
			builder.Append(", WonderLaunchSound = ");
			builder.Append(this.WonderLaunchSound);
			return true;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002243 File Offset: 0x00000443
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(FactionWonderSpec left, FactionWonderSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000224F File Offset: 0x0000044F
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(FactionWonderSpec left, FactionWonderSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002264 File Offset: 0x00000464
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((((base.GetHashCode() * -1521134295 + EqualityComparer<LocalizedText>.Default.GetHashCode(this.<WonderCompletionFlavor>k__BackingField)) * -1521134295 + EqualityComparer<LocalizedText>.Default.GetHashCode(this.<WonderCompletionMessage>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<Sprite>>.Default.GetHashCode(this.<WonderCompletionImage>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<WonderLaunchSound>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<WonderCompletionFlavorLocKey>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<WonderCompletionMessageLocKey>k__BackingField);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002301 File Offset: 0x00000501
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as FactionWonderSpec);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000230F File Offset: 0x0000050F
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002318 File Offset: 0x00000518
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(FactionWonderSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<LocalizedText>.Default.Equals(this.<WonderCompletionFlavor>k__BackingField, other.<WonderCompletionFlavor>k__BackingField) && EqualityComparer<LocalizedText>.Default.Equals(this.<WonderCompletionMessage>k__BackingField, other.<WonderCompletionMessage>k__BackingField) && EqualityComparer<AssetRef<Sprite>>.Default.Equals(this.<WonderCompletionImage>k__BackingField, other.<WonderCompletionImage>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<WonderLaunchSound>k__BackingField, other.<WonderLaunchSound>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<WonderCompletionFlavorLocKey>k__BackingField, other.<WonderCompletionFlavorLocKey>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<WonderCompletionMessageLocKey>k__BackingField, other.<WonderCompletionMessageLocKey>k__BackingField));
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000023D4 File Offset: 0x000005D4
		[CompilerGenerated]
		protected FactionWonderSpec([Nullable(1)] FactionWonderSpec original) : base(original)
		{
			this.WonderCompletionFlavor = original.<WonderCompletionFlavor>k__BackingField;
			this.WonderCompletionMessage = original.<WonderCompletionMessage>k__BackingField;
			this.WonderCompletionImage = original.<WonderCompletionImage>k__BackingField;
			this.WonderLaunchSound = original.<WonderLaunchSound>k__BackingField;
			this.WonderCompletionFlavorLocKey = original.<WonderCompletionFlavorLocKey>k__BackingField;
			this.WonderCompletionMessageLocKey = original.<WonderCompletionMessageLocKey>k__BackingField;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002430 File Offset: 0x00000630
		public FactionWonderSpec()
		{
		}
	}
}
