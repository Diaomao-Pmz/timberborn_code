using System;
using System.Text;
using Timberborn.Coordinates;

namespace Timberborn.TerrainSystemRendering
{
	// Token: 0x0200000B RID: 11
	public readonly struct SurfaceBlockShape : IEquatable<SurfaceBlockShape>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000023D3 File Offset: 0x000005D3
		public short Index { get; }

		// Token: 0x0600000F RID: 15 RVA: 0x000023DC File Offset: 0x000005DC
		public SurfaceBlockShape(RelativeHeight height11, RelativeHeight height10, RelativeHeight height00, RelativeHeight height01)
		{
			this._height11 = height11;
			this._height10 = height10;
			this._height00 = height00;
			this._height01 = height01;
			this.Index = (short)(this._height00 | this._height01 << 3 | this._height10 << 6 | this._height11 << 9);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002430 File Offset: 0x00000630
		public static SurfaceBlockShape FromModelName(string modelName)
		{
			if (modelName.Length < 4 || (modelName.Length > 4 && modelName[4] != '-'))
			{
				throw new ArgumentException("Invalid model name: " + modelName, "modelName");
			}
			return new SurfaceBlockShape(RelativeHeightExtensions.FromModelNameCharacter(modelName[0]), RelativeHeightExtensions.FromModelNameCharacter(modelName[1]), RelativeHeightExtensions.FromModelNameCharacter(modelName[2]), RelativeHeightExtensions.FromModelNameCharacter(modelName[3]));
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000024A5 File Offset: 0x000006A5
		public bool IsVisible
		{
			get
			{
				return !this.FullyUnderground && !this.FullyAboveGround;
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000024BC File Offset: 0x000006BC
		public SurfaceBlockShape Rotate(Orientation orientation)
		{
			switch (orientation)
			{
			case Orientation.Cw0:
				return this;
			case Orientation.Cw90:
				return new SurfaceBlockShape(this._height01, this._height11, this._height10, this._height00);
			case Orientation.Cw180:
				return new SurfaceBlockShape(this._height00, this._height01, this._height11, this._height10);
			case Orientation.Cw270:
				return new SurfaceBlockShape(this._height10, this._height00, this._height01, this._height11);
			default:
				throw new ArgumentOutOfRangeException("orientation", orientation, null);
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002553 File Offset: 0x00000753
		public bool Equals(SurfaceBlockShape other)
		{
			return this._height11 == other._height11 && this._height10 == other._height10 && this._height00 == other._height00 && this._height01 == other._height01;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002590 File Offset: 0x00000790
		public override bool Equals(object obj)
		{
			if (obj is SurfaceBlockShape)
			{
				SurfaceBlockShape other = (SurfaceBlockShape)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000025B5 File Offset: 0x000007B5
		public override int GetHashCode()
		{
			return (((int)this._height11 * 397 ^ (int)this._height10) * 397 ^ (int)this._height00) * 397 ^ (int)this._height01;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000025E4 File Offset: 0x000007E4
		public override string ToString()
		{
			return this.ToModelName();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000025EC File Offset: 0x000007EC
		public static bool operator ==(SurfaceBlockShape left, SurfaceBlockShape right)
		{
			return left.Equals(right);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000025F6 File Offset: 0x000007F6
		public static bool operator !=(SurfaceBlockShape left, SurfaceBlockShape right)
		{
			return !left.Equals(right);
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002604 File Offset: 0x00000804
		public bool FullyUnderground
		{
			get
			{
				return (this._height11 == RelativeHeight.Lower || this._height11 == RelativeHeight.Empty) && (this._height10 == RelativeHeight.Lower || this._height10 == RelativeHeight.Empty) && (this._height00 == RelativeHeight.Lower || this._height00 == RelativeHeight.Empty) && (this._height01 == RelativeHeight.Lower || this._height01 == RelativeHeight.Empty);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001A RID: 26 RVA: 0x0000265C File Offset: 0x0000085C
		public bool FullyAboveGround
		{
			get
			{
				return (this._height11 == RelativeHeight.Higher || this._height11 == RelativeHeight.Empty) && (this._height10 == RelativeHeight.Higher || this._height10 == RelativeHeight.Empty) && (this._height00 == RelativeHeight.Higher || this._height00 == RelativeHeight.Empty) && (this._height01 == RelativeHeight.Higher || this._height01 == RelativeHeight.Empty);
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000026B8 File Offset: 0x000008B8
		public string ToModelName()
		{
			StringBuilder stringBuilder = new StringBuilder(4);
			stringBuilder.Append(RelativeHeightExtensions.ToModelNameCharacter(this._height11));
			stringBuilder.Append(RelativeHeightExtensions.ToModelNameCharacter(this._height10));
			stringBuilder.Append(RelativeHeightExtensions.ToModelNameCharacter(this._height00));
			stringBuilder.Append(RelativeHeightExtensions.ToModelNameCharacter(this._height01));
			return stringBuilder.ToString();
		}

		// Token: 0x04000011 RID: 17
		public readonly RelativeHeight _height11;

		// Token: 0x04000012 RID: 18
		public readonly RelativeHeight _height10;

		// Token: 0x04000013 RID: 19
		public readonly RelativeHeight _height00;

		// Token: 0x04000014 RID: 20
		public readonly RelativeHeight _height01;
	}
}
