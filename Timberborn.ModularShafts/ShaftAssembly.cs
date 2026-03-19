using System;

namespace Timberborn.ModularShafts
{
	// Token: 0x02000014 RID: 20
	public struct ShaftAssembly
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00003CB9 File Offset: 0x00001EB9
		// (set) Token: 0x060000AA RID: 170 RVA: 0x00003CC1 File Offset: 0x00001EC1
		public bool ShowMainGearSmall { readonly get; private set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00003CCA File Offset: 0x00001ECA
		// (set) Token: 0x060000AC RID: 172 RVA: 0x00003CD2 File Offset: 0x00001ED2
		public bool ShowGearInner { readonly get; private set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00003CDB File Offset: 0x00001EDB
		// (set) Token: 0x060000AE RID: 174 RVA: 0x00003CE3 File Offset: 0x00001EE3
		public bool ShowGearInnerLong { readonly get; private set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00003CEC File Offset: 0x00001EEC
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x00003CF4 File Offset: 0x00001EF4
		public bool ShowGearInnerOpposite { readonly get; private set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00003CFD File Offset: 0x00001EFD
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x00003D05 File Offset: 0x00001F05
		public bool ShowGearInnerThrough { readonly get; private set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00003D0E File Offset: 0x00001F0E
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x00003D16 File Offset: 0x00001F16
		public bool ShowAxleInnerLong { readonly get; private set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00003D1F File Offset: 0x00001F1F
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x00003D27 File Offset: 0x00001F27
		public bool ShowMainGearLarge { readonly get; private set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00003D30 File Offset: 0x00001F30
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x00003D38 File Offset: 0x00001F38
		public bool ShowBottomGearBase { readonly get; private set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00003D41 File Offset: 0x00001F41
		// (set) Token: 0x060000BA RID: 186 RVA: 0x00003D49 File Offset: 0x00001F49
		public bool ShowOppositeGearSmall { readonly get; private set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00003D52 File Offset: 0x00001F52
		// (set) Token: 0x060000BC RID: 188 RVA: 0x00003D5A File Offset: 0x00001F5A
		public bool ShowLeftGearSmall { readonly get; private set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00003D63 File Offset: 0x00001F63
		// (set) Token: 0x060000BE RID: 190 RVA: 0x00003D6B File Offset: 0x00001F6B
		public bool ShowLeftGearMedium { readonly get; private set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00003D74 File Offset: 0x00001F74
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x00003D7C File Offset: 0x00001F7C
		public bool ShowRightGearSmall { readonly get; private set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00003D85 File Offset: 0x00001F85
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x00003D8D File Offset: 0x00001F8D
		public bool ShowRightGearMedium { readonly get; private set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00003D96 File Offset: 0x00001F96
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x00003D9E File Offset: 0x00001F9E
		public bool ShowGearTopSmall { readonly get; private set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00003DA7 File Offset: 0x00001FA7
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x00003DAF File Offset: 0x00001FAF
		public bool ShowGearBottomSmall { readonly get; private set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00003DB8 File Offset: 0x00001FB8
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x00003DC0 File Offset: 0x00001FC0
		public bool ShowGearBottomLarge { readonly get; private set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00003DC9 File Offset: 0x00001FC9
		// (set) Token: 0x060000CA RID: 202 RVA: 0x00003DD1 File Offset: 0x00001FD1
		public bool ShowGearTopLarge { readonly get; private set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00003DDA File Offset: 0x00001FDA
		// (set) Token: 0x060000CC RID: 204 RVA: 0x00003DE2 File Offset: 0x00001FE2
		public bool ShowAxleVertical { readonly get; private set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00003DEB File Offset: 0x00001FEB
		// (set) Token: 0x060000CE RID: 206 RVA: 0x00003DF3 File Offset: 0x00001FF3
		public bool ShowAxleHorizontal { readonly get; private set; }

		// Token: 0x060000CF RID: 207 RVA: 0x00003DFC File Offset: 0x00001FFC
		public void ConnectLeft(bool isReversed)
		{
			if (isReversed)
			{
				this.ShowLeftGearMedium = true;
				this.ShowMainGearLarge = true;
				return;
			}
			this.ShowMainGearSmall = true;
			this.ShowLeftGearSmall = true;
			this.ShowBottomGearBase = true;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00003E25 File Offset: 0x00002025
		public void ConnectRight(bool isReversed)
		{
			if (isReversed)
			{
				this.ShowRightGearMedium = true;
				this.ShowMainGearLarge = true;
				return;
			}
			this.ShowMainGearSmall = true;
			this.ShowRightGearSmall = true;
			this.ShowBottomGearBase = true;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00003E4E File Offset: 0x0000204E
		public void ConnectTop(bool isReversed)
		{
			if (isReversed)
			{
				this.ShowMainGearSmall = true;
				this.ShowGearTopLarge = true;
				return;
			}
			this.ShowGearTopSmall = true;
			this.ShowGearInner = true;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00003E70 File Offset: 0x00002070
		public void ConnectBottom(bool isReversed)
		{
			if (isReversed)
			{
				this.ShowMainGearSmall = true;
				this.ShowGearBottomLarge = true;
				return;
			}
			this.ShowGearBottomSmall = true;
			this.ShowGearInner = true;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00003E92 File Offset: 0x00002092
		public void ConnectOpposite(bool isReversed)
		{
			if (isReversed)
			{
				this.ShowAxleInnerLong = true;
				return;
			}
			this.ShowMainGearSmall = true;
			this.ShowBottomGearBase = true;
			this.ShowOppositeGearSmall = true;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00003EB4 File Offset: 0x000020B4
		public void ConnectTopAndBottomOnly(bool isReversed)
		{
			if (isReversed)
			{
				this.ShowAxleVertical = true;
				return;
			}
			this.ShowGearTopLarge = true;
			this.ShowGearBottomLarge = true;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00003ECF File Offset: 0x000020CF
		public void ConnectBottomOnly()
		{
			this.ShowGearBottomLarge = true;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00003ED8 File Offset: 0x000020D8
		public void ConnectTopOnly()
		{
			this.ShowGearTopLarge = true;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00003EE4 File Offset: 0x000020E4
		public void Optimize()
		{
			if (this.ShowAxleInnerLong && !this.ShowGearInner && !this.ShowMainGearLarge && !this.ShowMainGearSmall)
			{
				this.ShowAxleHorizontal = true;
				this.ShowAxleInnerLong = false;
			}
			if (this.ShowGearInner && !this.ShowMainGearSmall && !this.ShowMainGearLarge)
			{
				this.ShowGearInner = false;
				if (this.ShowAxleInnerLong)
				{
					this.ShowAxleInnerLong = false;
					this.ShowGearInnerThrough = true;
				}
				else
				{
					this.ShowGearInnerLong = true;
				}
			}
			if (this.ShowGearInner && this.ShowAxleInnerLong)
			{
				this.ShowGearInner = false;
				this.ShowAxleInnerLong = false;
				this.ShowGearInnerOpposite = true;
			}
			if (this.ShowGearTopLarge && this.ShowGearBottomLarge && !this.ShowMainGearSmall && !this.ShowLeftGearSmall && !this.ShowRightGearSmall && !this.ShowOppositeGearSmall)
			{
				this.ShowMainGearSmall = true;
			}
		}
	}
}
