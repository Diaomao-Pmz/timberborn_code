using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using Timberborn.LocalizationSerialization;
using UnityEngine;

namespace Timberborn.FactionSystem
{
	// Token: 0x02000007 RID: 7
	public class FactionSpec : ComponentSpec, IEquatable<FactionSpec>
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
				return typeof(FactionSpec);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8 RVA: 0x0000210A File Offset: 0x0000030A
		// (set) Token: 0x06000009 RID: 9 RVA: 0x00002112 File Offset: 0x00000312
		[Serialize]
		public string Id { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000A RID: 10 RVA: 0x0000211B File Offset: 0x0000031B
		// (set) Token: 0x0600000B RID: 11 RVA: 0x00002123 File Offset: 0x00000323
		[Serialize]
		public int Order { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000C RID: 12 RVA: 0x0000212C File Offset: 0x0000032C
		// (set) Token: 0x0600000D RID: 13 RVA: 0x00002134 File Offset: 0x00000334
		[Serialize("DisplayNameLocKey")]
		public LocalizedText DisplayName { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000E RID: 14 RVA: 0x0000213D File Offset: 0x0000033D
		// (set) Token: 0x0600000F RID: 15 RVA: 0x00002145 File Offset: 0x00000345
		[Serialize("DescriptionLocKey")]
		public LocalizedText Description { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000010 RID: 16 RVA: 0x0000214E File Offset: 0x0000034E
		// (set) Token: 0x06000011 RID: 17 RVA: 0x00002156 File Offset: 0x00000356
		[Serialize]
		public AssetRef<Sprite> Avatar { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000012 RID: 18 RVA: 0x0000215F File Offset: 0x0000035F
		// (set) Token: 0x06000013 RID: 19 RVA: 0x00002167 File Offset: 0x00000367
		[Serialize]
		public AssetRef<Sprite> ChildAvatar { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002170 File Offset: 0x00000370
		// (set) Token: 0x06000015 RID: 21 RVA: 0x00002178 File Offset: 0x00000378
		[Serialize]
		public AssetRef<Sprite> BotAvatar { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002181 File Offset: 0x00000381
		// (set) Token: 0x06000017 RID: 23 RVA: 0x00002189 File Offset: 0x00000389
		[Serialize]
		public AssetRef<Sprite> ContaminatedAdultAvatar { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002192 File Offset: 0x00000392
		// (set) Token: 0x06000019 RID: 25 RVA: 0x0000219A File Offset: 0x0000039A
		[Serialize]
		public AssetRef<Sprite> ContaminatedChildAvatar { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000021A3 File Offset: 0x000003A3
		// (set) Token: 0x0600001B RID: 27 RVA: 0x000021AB File Offset: 0x000003AB
		[Serialize]
		public AssetRef<Sprite> Logo { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000021B4 File Offset: 0x000003B4
		// (set) Token: 0x0600001D RID: 29 RVA: 0x000021BC File Offset: 0x000003BC
		[Serialize]
		public AssetRef<Sprite> NewGameFullAvatar { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000021C5 File Offset: 0x000003C5
		// (set) Token: 0x0600001F RID: 31 RVA: 0x000021CD File Offset: 0x000003CD
		[Serialize]
		public ImmutableArray<AssetRef<Texture2D>> Textures { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000021D6 File Offset: 0x000003D6
		// (set) Token: 0x06000021 RID: 33 RVA: 0x000021DE File Offset: 0x000003DE
		[Serialize]
		public ImmutableArray<AssetRef<Texture2D>> ChildTextures { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000021E7 File Offset: 0x000003E7
		// (set) Token: 0x06000023 RID: 35 RVA: 0x000021EF File Offset: 0x000003EF
		[Serialize]
		public ImmutableArray<string> MaterialCollectionIds { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000021F8 File Offset: 0x000003F8
		// (set) Token: 0x06000025 RID: 37 RVA: 0x00002200 File Offset: 0x00000400
		[Serialize]
		public ImmutableArray<string> TemplateCollectionIds { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002209 File Offset: 0x00000409
		// (set) Token: 0x06000027 RID: 39 RVA: 0x00002211 File Offset: 0x00000411
		[Serialize]
		public AssetRef<Material> PathMaterial { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000028 RID: 40 RVA: 0x0000221A File Offset: 0x0000041A
		// (set) Token: 0x06000029 RID: 41 RVA: 0x00002222 File Offset: 0x00000422
		[Serialize]
		public AssetRef<Material> BaseWoodMaterial { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600002A RID: 42 RVA: 0x0000222B File Offset: 0x0000042B
		// (set) Token: 0x0600002B RID: 43 RVA: 0x00002233 File Offset: 0x00000433
		[Serialize]
		public ImmutableArray<string> NeedCollectionIds { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600002C RID: 44 RVA: 0x0000223C File Offset: 0x0000043C
		// (set) Token: 0x0600002D RID: 45 RVA: 0x00002244 File Offset: 0x00000444
		[Serialize]
		public ImmutableArray<string> GoodCollectionIds { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600002E RID: 46 RVA: 0x0000224D File Offset: 0x0000044D
		// (set) Token: 0x0600002F RID: 47 RVA: 0x00002255 File Offset: 0x00000455
		[Serialize]
		public ImmutableArray<BlueprintModifierSpec> BlueprintModifiers { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000030 RID: 48 RVA: 0x0000225E File Offset: 0x0000045E
		// (set) Token: 0x06000031 RID: 49 RVA: 0x00002266 File Offset: 0x00000466
		[Serialize]
		public string StartingBuildingId { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000032 RID: 50 RVA: 0x0000226F File Offset: 0x0000046F
		// (set) Token: 0x06000033 RID: 51 RVA: 0x00002277 File Offset: 0x00000477
		[Serialize]
		public string SoundId { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002280 File Offset: 0x00000480
		// (set) Token: 0x06000035 RID: 53 RVA: 0x00002288 File Offset: 0x00000488
		[Serialize("GameOverMessageLocKey")]
		public LocalizedText GameOverMessage { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002291 File Offset: 0x00000491
		// (set) Token: 0x06000037 RID: 55 RVA: 0x00002299 File Offset: 0x00000499
		[Serialize("GameOverFlavorLocKey")]
		public LocalizedText GameOverFlavor { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000038 RID: 56 RVA: 0x000022A2 File Offset: 0x000004A2
		// (set) Token: 0x06000039 RID: 57 RVA: 0x000022AA File Offset: 0x000004AA
		[Serialize]
		private string DisplayNameLocKey { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600003A RID: 58 RVA: 0x000022B3 File Offset: 0x000004B3
		// (set) Token: 0x0600003B RID: 59 RVA: 0x000022BB File Offset: 0x000004BB
		[Serialize]
		private string DescriptionLocKey { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600003C RID: 60 RVA: 0x000022C4 File Offset: 0x000004C4
		// (set) Token: 0x0600003D RID: 61 RVA: 0x000022CC File Offset: 0x000004CC
		[Serialize]
		private string GameOverMessageLocKey { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600003E RID: 62 RVA: 0x000022D5 File Offset: 0x000004D5
		// (set) Token: 0x0600003F RID: 63 RVA: 0x000022DD File Offset: 0x000004DD
		[Serialize]
		private string GameOverFlavorLocKey { get; set; }

		// Token: 0x06000040 RID: 64 RVA: 0x000022E8 File Offset: 0x000004E8
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("FactionSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002334 File Offset: 0x00000534
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("Id = ");
			builder.Append(this.Id);
			builder.Append(", Order = ");
			builder.Append(this.Order.ToString());
			builder.Append(", DisplayName = ");
			builder.Append(this.DisplayName);
			builder.Append(", Description = ");
			builder.Append(this.Description);
			builder.Append(", Avatar = ");
			builder.Append(this.Avatar);
			builder.Append(", ChildAvatar = ");
			builder.Append(this.ChildAvatar);
			builder.Append(", BotAvatar = ");
			builder.Append(this.BotAvatar);
			builder.Append(", ContaminatedAdultAvatar = ");
			builder.Append(this.ContaminatedAdultAvatar);
			builder.Append(", ContaminatedChildAvatar = ");
			builder.Append(this.ContaminatedChildAvatar);
			builder.Append(", Logo = ");
			builder.Append(this.Logo);
			builder.Append(", NewGameFullAvatar = ");
			builder.Append(this.NewGameFullAvatar);
			builder.Append(", Textures = ");
			builder.Append(this.Textures.ToString());
			builder.Append(", ChildTextures = ");
			builder.Append(this.ChildTextures.ToString());
			builder.Append(", MaterialCollectionIds = ");
			builder.Append(this.MaterialCollectionIds.ToString());
			builder.Append(", TemplateCollectionIds = ");
			builder.Append(this.TemplateCollectionIds.ToString());
			builder.Append(", PathMaterial = ");
			builder.Append(this.PathMaterial);
			builder.Append(", BaseWoodMaterial = ");
			builder.Append(this.BaseWoodMaterial);
			builder.Append(", NeedCollectionIds = ");
			builder.Append(this.NeedCollectionIds.ToString());
			builder.Append(", GoodCollectionIds = ");
			builder.Append(this.GoodCollectionIds.ToString());
			builder.Append(", BlueprintModifiers = ");
			builder.Append(this.BlueprintModifiers.ToString());
			builder.Append(", StartingBuildingId = ");
			builder.Append(this.StartingBuildingId);
			builder.Append(", SoundId = ");
			builder.Append(this.SoundId);
			builder.Append(", GameOverMessage = ");
			builder.Append(this.GameOverMessage);
			builder.Append(", GameOverFlavor = ");
			builder.Append(this.GameOverFlavor);
			return true;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000261F File Offset: 0x0000081F
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(FactionSpec left, FactionSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000262B File Offset: 0x0000082B
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(FactionSpec left, FactionSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002640 File Offset: 0x00000840
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((((((((((((((((((((((((((base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Id>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Order>k__BackingField)) * -1521134295 + EqualityComparer<LocalizedText>.Default.GetHashCode(this.<DisplayName>k__BackingField)) * -1521134295 + EqualityComparer<LocalizedText>.Default.GetHashCode(this.<Description>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<Sprite>>.Default.GetHashCode(this.<Avatar>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<Sprite>>.Default.GetHashCode(this.<ChildAvatar>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<Sprite>>.Default.GetHashCode(this.<BotAvatar>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<Sprite>>.Default.GetHashCode(this.<ContaminatedAdultAvatar>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<Sprite>>.Default.GetHashCode(this.<ContaminatedChildAvatar>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<Sprite>>.Default.GetHashCode(this.<Logo>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<Sprite>>.Default.GetHashCode(this.<NewGameFullAvatar>k__BackingField)) * -1521134295 + EqualityComparer<ImmutableArray<AssetRef<Texture2D>>>.Default.GetHashCode(this.<Textures>k__BackingField)) * -1521134295 + EqualityComparer<ImmutableArray<AssetRef<Texture2D>>>.Default.GetHashCode(this.<ChildTextures>k__BackingField)) * -1521134295 + EqualityComparer<ImmutableArray<string>>.Default.GetHashCode(this.<MaterialCollectionIds>k__BackingField)) * -1521134295 + EqualityComparer<ImmutableArray<string>>.Default.GetHashCode(this.<TemplateCollectionIds>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<Material>>.Default.GetHashCode(this.<PathMaterial>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<Material>>.Default.GetHashCode(this.<BaseWoodMaterial>k__BackingField)) * -1521134295 + EqualityComparer<ImmutableArray<string>>.Default.GetHashCode(this.<NeedCollectionIds>k__BackingField)) * -1521134295 + EqualityComparer<ImmutableArray<string>>.Default.GetHashCode(this.<GoodCollectionIds>k__BackingField)) * -1521134295 + EqualityComparer<ImmutableArray<BlueprintModifierSpec>>.Default.GetHashCode(this.<BlueprintModifiers>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<StartingBuildingId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<SoundId>k__BackingField)) * -1521134295 + EqualityComparer<LocalizedText>.Default.GetHashCode(this.<GameOverMessage>k__BackingField)) * -1521134295 + EqualityComparer<LocalizedText>.Default.GetHashCode(this.<GameOverFlavor>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DisplayNameLocKey>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DescriptionLocKey>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<GameOverMessageLocKey>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<GameOverFlavorLocKey>k__BackingField);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000028D7 File Offset: 0x00000AD7
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as FactionSpec);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000028E5 File Offset: 0x00000AE5
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000028F0 File Offset: 0x00000AF0
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(FactionSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<Order>k__BackingField, other.<Order>k__BackingField) && EqualityComparer<LocalizedText>.Default.Equals(this.<DisplayName>k__BackingField, other.<DisplayName>k__BackingField) && EqualityComparer<LocalizedText>.Default.Equals(this.<Description>k__BackingField, other.<Description>k__BackingField) && EqualityComparer<AssetRef<Sprite>>.Default.Equals(this.<Avatar>k__BackingField, other.<Avatar>k__BackingField) && EqualityComparer<AssetRef<Sprite>>.Default.Equals(this.<ChildAvatar>k__BackingField, other.<ChildAvatar>k__BackingField) && EqualityComparer<AssetRef<Sprite>>.Default.Equals(this.<BotAvatar>k__BackingField, other.<BotAvatar>k__BackingField) && EqualityComparer<AssetRef<Sprite>>.Default.Equals(this.<ContaminatedAdultAvatar>k__BackingField, other.<ContaminatedAdultAvatar>k__BackingField) && EqualityComparer<AssetRef<Sprite>>.Default.Equals(this.<ContaminatedChildAvatar>k__BackingField, other.<ContaminatedChildAvatar>k__BackingField) && EqualityComparer<AssetRef<Sprite>>.Default.Equals(this.<Logo>k__BackingField, other.<Logo>k__BackingField) && EqualityComparer<AssetRef<Sprite>>.Default.Equals(this.<NewGameFullAvatar>k__BackingField, other.<NewGameFullAvatar>k__BackingField) && EqualityComparer<ImmutableArray<AssetRef<Texture2D>>>.Default.Equals(this.<Textures>k__BackingField, other.<Textures>k__BackingField) && EqualityComparer<ImmutableArray<AssetRef<Texture2D>>>.Default.Equals(this.<ChildTextures>k__BackingField, other.<ChildTextures>k__BackingField) && EqualityComparer<ImmutableArray<string>>.Default.Equals(this.<MaterialCollectionIds>k__BackingField, other.<MaterialCollectionIds>k__BackingField) && EqualityComparer<ImmutableArray<string>>.Default.Equals(this.<TemplateCollectionIds>k__BackingField, other.<TemplateCollectionIds>k__BackingField) && EqualityComparer<AssetRef<Material>>.Default.Equals(this.<PathMaterial>k__BackingField, other.<PathMaterial>k__BackingField) && EqualityComparer<AssetRef<Material>>.Default.Equals(this.<BaseWoodMaterial>k__BackingField, other.<BaseWoodMaterial>k__BackingField) && EqualityComparer<ImmutableArray<string>>.Default.Equals(this.<NeedCollectionIds>k__BackingField, other.<NeedCollectionIds>k__BackingField) && EqualityComparer<ImmutableArray<string>>.Default.Equals(this.<GoodCollectionIds>k__BackingField, other.<GoodCollectionIds>k__BackingField) && EqualityComparer<ImmutableArray<BlueprintModifierSpec>>.Default.Equals(this.<BlueprintModifiers>k__BackingField, other.<BlueprintModifiers>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<StartingBuildingId>k__BackingField, other.<StartingBuildingId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<SoundId>k__BackingField, other.<SoundId>k__BackingField) && EqualityComparer<LocalizedText>.Default.Equals(this.<GameOverMessage>k__BackingField, other.<GameOverMessage>k__BackingField) && EqualityComparer<LocalizedText>.Default.Equals(this.<GameOverFlavor>k__BackingField, other.<GameOverFlavor>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<DisplayNameLocKey>k__BackingField, other.<DisplayNameLocKey>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<DescriptionLocKey>k__BackingField, other.<DescriptionLocKey>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<GameOverMessageLocKey>k__BackingField, other.<GameOverMessageLocKey>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<GameOverFlavorLocKey>k__BackingField, other.<GameOverFlavorLocKey>k__BackingField));
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002BFC File Offset: 0x00000DFC
		[CompilerGenerated]
		protected FactionSpec([Nullable(1)] FactionSpec original) : base(original)
		{
			this.Id = original.<Id>k__BackingField;
			this.Order = original.<Order>k__BackingField;
			this.DisplayName = original.<DisplayName>k__BackingField;
			this.Description = original.<Description>k__BackingField;
			this.Avatar = original.<Avatar>k__BackingField;
			this.ChildAvatar = original.<ChildAvatar>k__BackingField;
			this.BotAvatar = original.<BotAvatar>k__BackingField;
			this.ContaminatedAdultAvatar = original.<ContaminatedAdultAvatar>k__BackingField;
			this.ContaminatedChildAvatar = original.<ContaminatedChildAvatar>k__BackingField;
			this.Logo = original.<Logo>k__BackingField;
			this.NewGameFullAvatar = original.<NewGameFullAvatar>k__BackingField;
			this.Textures = original.<Textures>k__BackingField;
			this.ChildTextures = original.<ChildTextures>k__BackingField;
			this.MaterialCollectionIds = original.<MaterialCollectionIds>k__BackingField;
			this.TemplateCollectionIds = original.<TemplateCollectionIds>k__BackingField;
			this.PathMaterial = original.<PathMaterial>k__BackingField;
			this.BaseWoodMaterial = original.<BaseWoodMaterial>k__BackingField;
			this.NeedCollectionIds = original.<NeedCollectionIds>k__BackingField;
			this.GoodCollectionIds = original.<GoodCollectionIds>k__BackingField;
			this.BlueprintModifiers = original.<BlueprintModifiers>k__BackingField;
			this.StartingBuildingId = original.<StartingBuildingId>k__BackingField;
			this.SoundId = original.<SoundId>k__BackingField;
			this.GameOverMessage = original.<GameOverMessage>k__BackingField;
			this.GameOverFlavor = original.<GameOverFlavor>k__BackingField;
			this.DisplayNameLocKey = original.<DisplayNameLocKey>k__BackingField;
			this.DescriptionLocKey = original.<DescriptionLocKey>k__BackingField;
			this.GameOverMessageLocKey = original.<GameOverMessageLocKey>k__BackingField;
			this.GameOverFlavorLocKey = original.<GameOverFlavorLocKey>k__BackingField;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002D60 File Offset: 0x00000F60
		public FactionSpec()
		{
		}
	}
}
