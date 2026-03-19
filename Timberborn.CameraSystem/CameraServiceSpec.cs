using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.CameraSystem
{
	// Token: 0x0200000D RID: 13
	public class CameraServiceSpec : ComponentSpec, IEquatable<CameraServiceSpec>
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002FE4 File Offset: 0x000011E4
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(CameraServiceSpec);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002FF0 File Offset: 0x000011F0
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00002FF8 File Offset: 0x000011F8
		[Serialize]
		public float HorizontalAngle { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00003001 File Offset: 0x00001201
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00003009 File Offset: 0x00001209
		[Serialize]
		public float VerticalAngle { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00003012 File Offset: 0x00001212
		// (set) Token: 0x0600005F RID: 95 RVA: 0x0000301A File Offset: 0x0000121A
		[Serialize]
		public FloatLimitsSpec VerticalAngleLimits { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00003023 File Offset: 0x00001223
		// (set) Token: 0x06000061 RID: 97 RVA: 0x0000302B File Offset: 0x0000122B
		[Serialize]
		public float ZoomLevel { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00003034 File Offset: 0x00001234
		// (set) Token: 0x06000063 RID: 99 RVA: 0x0000303C File Offset: 0x0000123C
		[Serialize]
		public float ZoomSpeed { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00003045 File Offset: 0x00001245
		// (set) Token: 0x06000065 RID: 101 RVA: 0x0000304D File Offset: 0x0000124D
		[Serialize]
		public float ZoomBase { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00003056 File Offset: 0x00001256
		// (set) Token: 0x06000067 RID: 103 RVA: 0x0000305E File Offset: 0x0000125E
		[Serialize]
		public float BaseDistance { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00003067 File Offset: 0x00001267
		// (set) Token: 0x06000069 RID: 105 RVA: 0x0000306F File Offset: 0x0000126F
		[Serialize]
		public FloatLimitsSpec DefaultZoomLimits { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00003078 File Offset: 0x00001278
		// (set) Token: 0x0600006B RID: 107 RVA: 0x00003080 File Offset: 0x00001280
		[Serialize]
		public FloatLimitsSpec UnlockedZoomLimits { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00003089 File Offset: 0x00001289
		// (set) Token: 0x0600006D RID: 109 RVA: 0x00003091 File Offset: 0x00001291
		[Serialize]
		public FloatLimitsSpec MapEditorZoomLimits { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600006E RID: 110 RVA: 0x0000309A File Offset: 0x0000129A
		// (set) Token: 0x0600006F RID: 111 RVA: 0x000030A2 File Offset: 0x000012A2
		[Serialize]
		public FloatLimitsSpec FreeModeZoomLimits { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000070 RID: 112 RVA: 0x000030AB File Offset: 0x000012AB
		// (set) Token: 0x06000071 RID: 113 RVA: 0x000030B3 File Offset: 0x000012B3
		[Serialize]
		public float MapMargin { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000072 RID: 114 RVA: 0x000030BC File Offset: 0x000012BC
		// (set) Token: 0x06000073 RID: 115 RVA: 0x000030C4 File Offset: 0x000012C4
		[Serialize]
		public float FreeModeMapMargin { get; set; }

		// Token: 0x06000074 RID: 116 RVA: 0x000030D0 File Offset: 0x000012D0
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("CameraServiceSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000075 RID: 117 RVA: 0x0000311C File Offset: 0x0000131C
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("HorizontalAngle = ");
			builder.Append(this.HorizontalAngle.ToString());
			builder.Append(", VerticalAngle = ");
			builder.Append(this.VerticalAngle.ToString());
			builder.Append(", VerticalAngleLimits = ");
			builder.Append(this.VerticalAngleLimits);
			builder.Append(", ZoomLevel = ");
			builder.Append(this.ZoomLevel.ToString());
			builder.Append(", ZoomSpeed = ");
			builder.Append(this.ZoomSpeed.ToString());
			builder.Append(", ZoomBase = ");
			builder.Append(this.ZoomBase.ToString());
			builder.Append(", BaseDistance = ");
			builder.Append(this.BaseDistance.ToString());
			builder.Append(", DefaultZoomLimits = ");
			builder.Append(this.DefaultZoomLimits);
			builder.Append(", UnlockedZoomLimits = ");
			builder.Append(this.UnlockedZoomLimits);
			builder.Append(", MapEditorZoomLimits = ");
			builder.Append(this.MapEditorZoomLimits);
			builder.Append(", FreeModeZoomLimits = ");
			builder.Append(this.FreeModeZoomLimits);
			builder.Append(", MapMargin = ");
			builder.Append(this.MapMargin.ToString());
			builder.Append(", FreeModeMapMargin = ");
			builder.Append(this.FreeModeMapMargin.ToString());
			return true;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000032F4 File Offset: 0x000014F4
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(CameraServiceSpec left, CameraServiceSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003300 File Offset: 0x00001500
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(CameraServiceSpec left, CameraServiceSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003314 File Offset: 0x00001514
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((((((((((((base.GetHashCode() * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<HorizontalAngle>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<VerticalAngle>k__BackingField)) * -1521134295 + EqualityComparer<FloatLimitsSpec>.Default.GetHashCode(this.<VerticalAngleLimits>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<ZoomLevel>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<ZoomSpeed>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<ZoomBase>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<BaseDistance>k__BackingField)) * -1521134295 + EqualityComparer<FloatLimitsSpec>.Default.GetHashCode(this.<DefaultZoomLimits>k__BackingField)) * -1521134295 + EqualityComparer<FloatLimitsSpec>.Default.GetHashCode(this.<UnlockedZoomLimits>k__BackingField)) * -1521134295 + EqualityComparer<FloatLimitsSpec>.Default.GetHashCode(this.<MapEditorZoomLimits>k__BackingField)) * -1521134295 + EqualityComparer<FloatLimitsSpec>.Default.GetHashCode(this.<FreeModeZoomLimits>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<MapMargin>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<FreeModeMapMargin>k__BackingField);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003452 File Offset: 0x00001652
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as CameraServiceSpec);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003460 File Offset: 0x00001660
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x0000346C File Offset: 0x0000166C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(CameraServiceSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<float>.Default.Equals(this.<HorizontalAngle>k__BackingField, other.<HorizontalAngle>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<VerticalAngle>k__BackingField, other.<VerticalAngle>k__BackingField) && EqualityComparer<FloatLimitsSpec>.Default.Equals(this.<VerticalAngleLimits>k__BackingField, other.<VerticalAngleLimits>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<ZoomLevel>k__BackingField, other.<ZoomLevel>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<ZoomSpeed>k__BackingField, other.<ZoomSpeed>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<ZoomBase>k__BackingField, other.<ZoomBase>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<BaseDistance>k__BackingField, other.<BaseDistance>k__BackingField) && EqualityComparer<FloatLimitsSpec>.Default.Equals(this.<DefaultZoomLimits>k__BackingField, other.<DefaultZoomLimits>k__BackingField) && EqualityComparer<FloatLimitsSpec>.Default.Equals(this.<UnlockedZoomLimits>k__BackingField, other.<UnlockedZoomLimits>k__BackingField) && EqualityComparer<FloatLimitsSpec>.Default.Equals(this.<MapEditorZoomLimits>k__BackingField, other.<MapEditorZoomLimits>k__BackingField) && EqualityComparer<FloatLimitsSpec>.Default.Equals(this.<FreeModeZoomLimits>k__BackingField, other.<FreeModeZoomLimits>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<MapMargin>k__BackingField, other.<MapMargin>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<FreeModeMapMargin>k__BackingField, other.<FreeModeMapMargin>k__BackingField));
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000035E4 File Offset: 0x000017E4
		[CompilerGenerated]
		protected CameraServiceSpec([Nullable(1)] CameraServiceSpec original) : base(original)
		{
			this.HorizontalAngle = original.<HorizontalAngle>k__BackingField;
			this.VerticalAngle = original.<VerticalAngle>k__BackingField;
			this.VerticalAngleLimits = original.<VerticalAngleLimits>k__BackingField;
			this.ZoomLevel = original.<ZoomLevel>k__BackingField;
			this.ZoomSpeed = original.<ZoomSpeed>k__BackingField;
			this.ZoomBase = original.<ZoomBase>k__BackingField;
			this.BaseDistance = original.<BaseDistance>k__BackingField;
			this.DefaultZoomLimits = original.<DefaultZoomLimits>k__BackingField;
			this.UnlockedZoomLimits = original.<UnlockedZoomLimits>k__BackingField;
			this.MapEditorZoomLimits = original.<MapEditorZoomLimits>k__BackingField;
			this.FreeModeZoomLimits = original.<FreeModeZoomLimits>k__BackingField;
			this.MapMargin = original.<MapMargin>k__BackingField;
			this.FreeModeMapMargin = original.<FreeModeMapMargin>k__BackingField;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003694 File Offset: 0x00001894
		public CameraServiceSpec()
		{
		}
	}
}
