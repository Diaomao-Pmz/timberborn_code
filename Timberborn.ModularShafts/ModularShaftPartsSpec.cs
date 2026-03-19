using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.ModularShafts
{
	// Token: 0x02000010 RID: 16
	public class ModularShaftPartsSpec : ComponentSpec, IEquatable<ModularShaftPartsSpec>
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002D0F File Offset: 0x00000F0F
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(ModularShaftPartsSpec);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002D1B File Offset: 0x00000F1B
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00002D23 File Offset: 0x00000F23
		[Serialize]
		public AssetRef<GameObject> ShaftBase { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002D2C File Offset: 0x00000F2C
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00002D34 File Offset: 0x00000F34
		[Serialize]
		public AssetRef<GameObject> ShaftLowerFrame { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002D3D File Offset: 0x00000F3D
		// (set) Token: 0x0600005F RID: 95 RVA: 0x00002D45 File Offset: 0x00000F45
		[Serialize]
		public AssetRef<GameObject> ShaftSupport { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00002D4E File Offset: 0x00000F4E
		// (set) Token: 0x06000061 RID: 97 RVA: 0x00002D56 File Offset: 0x00000F56
		[Serialize]
		public AssetRef<GameObject> ShaftFrame { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002D5F File Offset: 0x00000F5F
		// (set) Token: 0x06000063 RID: 99 RVA: 0x00002D67 File Offset: 0x00000F67
		[Serialize]
		public AssetRef<GameObject> GearSmall { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002D70 File Offset: 0x00000F70
		// (set) Token: 0x06000065 RID: 101 RVA: 0x00002D78 File Offset: 0x00000F78
		[Serialize]
		public AssetRef<GameObject> GearMedium { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002D81 File Offset: 0x00000F81
		// (set) Token: 0x06000067 RID: 103 RVA: 0x00002D89 File Offset: 0x00000F89
		[Serialize]
		public AssetRef<GameObject> GearLarge { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00002D92 File Offset: 0x00000F92
		// (set) Token: 0x06000069 RID: 105 RVA: 0x00002D9A File Offset: 0x00000F9A
		[Serialize]
		public AssetRef<GameObject> GearBottomBase { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00002DA3 File Offset: 0x00000FA3
		// (set) Token: 0x0600006B RID: 107 RVA: 0x00002DAB File Offset: 0x00000FAB
		[Serialize]
		public AssetRef<GameObject> GearBottomSmall { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00002DB4 File Offset: 0x00000FB4
		// (set) Token: 0x0600006D RID: 109 RVA: 0x00002DBC File Offset: 0x00000FBC
		[Serialize]
		public AssetRef<GameObject> GearBottomLarge { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00002DC5 File Offset: 0x00000FC5
		// (set) Token: 0x0600006F RID: 111 RVA: 0x00002DCD File Offset: 0x00000FCD
		[Serialize]
		public AssetRef<GameObject> GearTopSmall { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00002DD6 File Offset: 0x00000FD6
		// (set) Token: 0x06000071 RID: 113 RVA: 0x00002DDE File Offset: 0x00000FDE
		[Serialize]
		public AssetRef<GameObject> GearTopLarge { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00002DE7 File Offset: 0x00000FE7
		// (set) Token: 0x06000073 RID: 115 RVA: 0x00002DEF File Offset: 0x00000FEF
		[Serialize]
		public AssetRef<GameObject> GearInner { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00002DF8 File Offset: 0x00000FF8
		// (set) Token: 0x06000075 RID: 117 RVA: 0x00002E00 File Offset: 0x00001000
		[Serialize]
		public AssetRef<GameObject> GearInnerLong { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00002E09 File Offset: 0x00001009
		// (set) Token: 0x06000077 RID: 119 RVA: 0x00002E11 File Offset: 0x00001011
		[Serialize]
		public AssetRef<GameObject> GearInnerOpposite { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00002E1A File Offset: 0x0000101A
		// (set) Token: 0x06000079 RID: 121 RVA: 0x00002E22 File Offset: 0x00001022
		[Serialize]
		public AssetRef<GameObject> GearInnerThrough { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00002E2B File Offset: 0x0000102B
		// (set) Token: 0x0600007B RID: 123 RVA: 0x00002E33 File Offset: 0x00001033
		[Serialize]
		public AssetRef<GameObject> AxleInnerLong { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00002E3C File Offset: 0x0000103C
		// (set) Token: 0x0600007D RID: 125 RVA: 0x00002E44 File Offset: 0x00001044
		[Serialize]
		public AssetRef<GameObject> AxleVertical { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00002E4D File Offset: 0x0000104D
		// (set) Token: 0x0600007F RID: 127 RVA: 0x00002E55 File Offset: 0x00001055
		[Serialize]
		public AssetRef<GameObject> AxleHorizontal { get; set; }

		// Token: 0x06000080 RID: 128 RVA: 0x00002E60 File Offset: 0x00001060
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ModularShaftPartsSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00002EAC File Offset: 0x000010AC
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("ShaftBase = ");
			builder.Append(this.ShaftBase);
			builder.Append(", ShaftLowerFrame = ");
			builder.Append(this.ShaftLowerFrame);
			builder.Append(", ShaftSupport = ");
			builder.Append(this.ShaftSupport);
			builder.Append(", ShaftFrame = ");
			builder.Append(this.ShaftFrame);
			builder.Append(", GearSmall = ");
			builder.Append(this.GearSmall);
			builder.Append(", GearMedium = ");
			builder.Append(this.GearMedium);
			builder.Append(", GearLarge = ");
			builder.Append(this.GearLarge);
			builder.Append(", GearBottomBase = ");
			builder.Append(this.GearBottomBase);
			builder.Append(", GearBottomSmall = ");
			builder.Append(this.GearBottomSmall);
			builder.Append(", GearBottomLarge = ");
			builder.Append(this.GearBottomLarge);
			builder.Append(", GearTopSmall = ");
			builder.Append(this.GearTopSmall);
			builder.Append(", GearTopLarge = ");
			builder.Append(this.GearTopLarge);
			builder.Append(", GearInner = ");
			builder.Append(this.GearInner);
			builder.Append(", GearInnerLong = ");
			builder.Append(this.GearInnerLong);
			builder.Append(", GearInnerOpposite = ");
			builder.Append(this.GearInnerOpposite);
			builder.Append(", GearInnerThrough = ");
			builder.Append(this.GearInnerThrough);
			builder.Append(", AxleInnerLong = ");
			builder.Append(this.AxleInnerLong);
			builder.Append(", AxleVertical = ");
			builder.Append(this.AxleVertical);
			builder.Append(", AxleHorizontal = ");
			builder.Append(this.AxleHorizontal);
			return true;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000030AA File Offset: 0x000012AA
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ModularShaftPartsSpec left, ModularShaftPartsSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000030B6 File Offset: 0x000012B6
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ModularShaftPartsSpec left, ModularShaftPartsSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000030CC File Offset: 0x000012CC
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((((((((((((((((((base.GetHashCode() * -1521134295 + EqualityComparer<AssetRef<GameObject>>.Default.GetHashCode(this.<ShaftBase>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<GameObject>>.Default.GetHashCode(this.<ShaftLowerFrame>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<GameObject>>.Default.GetHashCode(this.<ShaftSupport>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<GameObject>>.Default.GetHashCode(this.<ShaftFrame>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<GameObject>>.Default.GetHashCode(this.<GearSmall>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<GameObject>>.Default.GetHashCode(this.<GearMedium>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<GameObject>>.Default.GetHashCode(this.<GearLarge>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<GameObject>>.Default.GetHashCode(this.<GearBottomBase>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<GameObject>>.Default.GetHashCode(this.<GearBottomSmall>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<GameObject>>.Default.GetHashCode(this.<GearBottomLarge>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<GameObject>>.Default.GetHashCode(this.<GearTopSmall>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<GameObject>>.Default.GetHashCode(this.<GearTopLarge>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<GameObject>>.Default.GetHashCode(this.<GearInner>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<GameObject>>.Default.GetHashCode(this.<GearInnerLong>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<GameObject>>.Default.GetHashCode(this.<GearInnerOpposite>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<GameObject>>.Default.GetHashCode(this.<GearInnerThrough>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<GameObject>>.Default.GetHashCode(this.<AxleInnerLong>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<GameObject>>.Default.GetHashCode(this.<AxleVertical>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<GameObject>>.Default.GetHashCode(this.<AxleHorizontal>k__BackingField);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003294 File Offset: 0x00001494
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ModularShaftPartsSpec);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00002742 File Offset: 0x00000942
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000032A4 File Offset: 0x000014A4
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ModularShaftPartsSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<AssetRef<GameObject>>.Default.Equals(this.<ShaftBase>k__BackingField, other.<ShaftBase>k__BackingField) && EqualityComparer<AssetRef<GameObject>>.Default.Equals(this.<ShaftLowerFrame>k__BackingField, other.<ShaftLowerFrame>k__BackingField) && EqualityComparer<AssetRef<GameObject>>.Default.Equals(this.<ShaftSupport>k__BackingField, other.<ShaftSupport>k__BackingField) && EqualityComparer<AssetRef<GameObject>>.Default.Equals(this.<ShaftFrame>k__BackingField, other.<ShaftFrame>k__BackingField) && EqualityComparer<AssetRef<GameObject>>.Default.Equals(this.<GearSmall>k__BackingField, other.<GearSmall>k__BackingField) && EqualityComparer<AssetRef<GameObject>>.Default.Equals(this.<GearMedium>k__BackingField, other.<GearMedium>k__BackingField) && EqualityComparer<AssetRef<GameObject>>.Default.Equals(this.<GearLarge>k__BackingField, other.<GearLarge>k__BackingField) && EqualityComparer<AssetRef<GameObject>>.Default.Equals(this.<GearBottomBase>k__BackingField, other.<GearBottomBase>k__BackingField) && EqualityComparer<AssetRef<GameObject>>.Default.Equals(this.<GearBottomSmall>k__BackingField, other.<GearBottomSmall>k__BackingField) && EqualityComparer<AssetRef<GameObject>>.Default.Equals(this.<GearBottomLarge>k__BackingField, other.<GearBottomLarge>k__BackingField) && EqualityComparer<AssetRef<GameObject>>.Default.Equals(this.<GearTopSmall>k__BackingField, other.<GearTopSmall>k__BackingField) && EqualityComparer<AssetRef<GameObject>>.Default.Equals(this.<GearTopLarge>k__BackingField, other.<GearTopLarge>k__BackingField) && EqualityComparer<AssetRef<GameObject>>.Default.Equals(this.<GearInner>k__BackingField, other.<GearInner>k__BackingField) && EqualityComparer<AssetRef<GameObject>>.Default.Equals(this.<GearInnerLong>k__BackingField, other.<GearInnerLong>k__BackingField) && EqualityComparer<AssetRef<GameObject>>.Default.Equals(this.<GearInnerOpposite>k__BackingField, other.<GearInnerOpposite>k__BackingField) && EqualityComparer<AssetRef<GameObject>>.Default.Equals(this.<GearInnerThrough>k__BackingField, other.<GearInnerThrough>k__BackingField) && EqualityComparer<AssetRef<GameObject>>.Default.Equals(this.<AxleInnerLong>k__BackingField, other.<AxleInnerLong>k__BackingField) && EqualityComparer<AssetRef<GameObject>>.Default.Equals(this.<AxleVertical>k__BackingField, other.<AxleVertical>k__BackingField) && EqualityComparer<AssetRef<GameObject>>.Default.Equals(this.<AxleHorizontal>k__BackingField, other.<AxleHorizontal>k__BackingField));
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000034C0 File Offset: 0x000016C0
		[CompilerGenerated]
		protected ModularShaftPartsSpec([Nullable(1)] ModularShaftPartsSpec original) : base(original)
		{
			this.ShaftBase = original.<ShaftBase>k__BackingField;
			this.ShaftLowerFrame = original.<ShaftLowerFrame>k__BackingField;
			this.ShaftSupport = original.<ShaftSupport>k__BackingField;
			this.ShaftFrame = original.<ShaftFrame>k__BackingField;
			this.GearSmall = original.<GearSmall>k__BackingField;
			this.GearMedium = original.<GearMedium>k__BackingField;
			this.GearLarge = original.<GearLarge>k__BackingField;
			this.GearBottomBase = original.<GearBottomBase>k__BackingField;
			this.GearBottomSmall = original.<GearBottomSmall>k__BackingField;
			this.GearBottomLarge = original.<GearBottomLarge>k__BackingField;
			this.GearTopSmall = original.<GearTopSmall>k__BackingField;
			this.GearTopLarge = original.<GearTopLarge>k__BackingField;
			this.GearInner = original.<GearInner>k__BackingField;
			this.GearInnerLong = original.<GearInnerLong>k__BackingField;
			this.GearInnerOpposite = original.<GearInnerOpposite>k__BackingField;
			this.GearInnerThrough = original.<GearInnerThrough>k__BackingField;
			this.AxleInnerLong = original.<AxleInnerLong>k__BackingField;
			this.AxleVertical = original.<AxleVertical>k__BackingField;
			this.AxleHorizontal = original.<AxleHorizontal>k__BackingField;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00002791 File Offset: 0x00000991
		public ModularShaftPartsSpec()
		{
		}
	}
}
