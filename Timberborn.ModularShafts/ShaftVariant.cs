using System;
using Timberborn.Coordinates;

namespace Timberborn.ModularShafts
{
	// Token: 0x0200001A RID: 26
	public readonly struct ShaftVariant : IEquatable<ShaftVariant>
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00004B06 File Offset: 0x00002D06
		public byte Down { get; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00004B0E File Offset: 0x00002D0E
		public byte Left { get; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00004B16 File Offset: 0x00002D16
		public byte Up { get; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00004B1E File Offset: 0x00002D1E
		public byte Right { get; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00004B26 File Offset: 0x00002D26
		public byte Bottom { get; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00004B2E File Offset: 0x00002D2E
		public byte Top { get; }

		// Token: 0x06000108 RID: 264 RVA: 0x00004B36 File Offset: 0x00002D36
		public ShaftVariant(byte down, byte left, byte up, byte right, byte bottom, byte top)
		{
			this.Down = down;
			this.Left = left;
			this.Up = up;
			this.Right = right;
			this.Bottom = bottom;
			this.Top = top;
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00004B65 File Offset: 0x00002D65
		public static ShaftVariant CreateHorizontal(byte down, byte left, byte up, byte right)
		{
			return new ShaftVariant(down, left, up, right, 0, 0);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00004B72 File Offset: 0x00002D72
		public ShaftVariant ToFacingTop()
		{
			return new ShaftVariant(this.Down, this.Left, this.Up, this.Right, 0, 1);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00004B93 File Offset: 0x00002D93
		public ShaftVariant ToFacingTopReversed()
		{
			return new ShaftVariant(this.Down, this.Left, this.Up, this.Right, 0, 2);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00004BB4 File Offset: 0x00002DB4
		public ShaftVariant ToFacingBottom()
		{
			return new ShaftVariant(this.Down, this.Left, this.Up, this.Right, 1, 0);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00004BD5 File Offset: 0x00002DD5
		public ShaftVariant ToFacingBottomReversed()
		{
			return new ShaftVariant(this.Down, this.Left, this.Up, this.Right, 2, 0);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00004BF6 File Offset: 0x00002DF6
		public ShaftVariant ToFacingTopAndBottom(bool reverseBottom, bool reverseTop)
		{
			return new ShaftVariant(this.Down, this.Left, this.Up, this.Right, reverseBottom ? 2 : 1, reverseTop ? 2 : 1);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00004C28 File Offset: 0x00002E28
		public ShaftVariant Rotate(Orientation orientation)
		{
			ShaftVariant result;
			switch (orientation)
			{
			case Orientation.Cw0:
				result = new ShaftVariant(this.Down, this.Left, this.Up, this.Right, this.Bottom, this.Top);
				break;
			case Orientation.Cw90:
				result = new ShaftVariant(this.Right, this.Down, this.Left, this.Up, this.Bottom, this.Top);
				break;
			case Orientation.Cw180:
				result = new ShaftVariant(this.Up, this.Right, this.Down, this.Left, this.Bottom, this.Top);
				break;
			case Orientation.Cw270:
				result = new ShaftVariant(this.Left, this.Up, this.Right, this.Down, this.Bottom, this.Top);
				break;
			default:
				throw new ArgumentOutOfRangeException("orientation", orientation, null);
			}
			return result;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00004D16 File Offset: 0x00002F16
		public ShaftVariant AddSymmetryRight()
		{
			return new ShaftVariant(this.Down, this.Left, this.Up, (this.Left == 1) ? 2 : 1, this.Bottom, this.Top);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00004D49 File Offset: 0x00002F49
		public ShaftVariant AddSymmetryLeft()
		{
			return new ShaftVariant(this.Down, (this.Right == 1) ? 2 : 1, this.Up, this.Right, this.Bottom, this.Top);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00004D7C File Offset: 0x00002F7C
		public ShaftVariant AddSymmetryUp()
		{
			return new ShaftVariant((this.Down == 1) ? 2 : 1, this.Left, this.Up, this.Right, this.Bottom, this.Top);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00004DAF File Offset: 0x00002FAF
		public ShaftVariant AddSymmetryDown()
		{
			return new ShaftVariant(this.Down, this.Left, (this.Up == 1) ? 2 : 1, this.Right, this.Bottom, this.Top);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00004DE4 File Offset: 0x00002FE4
		public byte GetRotation(Direction3D direction)
		{
			byte result;
			switch (direction)
			{
			case Direction3D.Down:
				result = this.Down;
				break;
			case Direction3D.Left:
				result = this.Left;
				break;
			case Direction3D.Up:
				result = this.Up;
				break;
			case Direction3D.Right:
				result = this.Right;
				break;
			case Direction3D.Bottom:
				result = this.Bottom;
				break;
			case Direction3D.Top:
				result = this.Top;
				break;
			default:
				throw new ArgumentOutOfRangeException("direction", direction, null);
			}
			return result;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00004E5C File Offset: 0x0000305C
		public string GetName()
		{
			return string.Concat(new string[]
			{
				ShaftVariant.Value(this.Down),
				ShaftVariant.Value(this.Left),
				ShaftVariant.Value(this.Up),
				ShaftVariant.Value(this.Right),
				ShaftVariant.Value(this.Bottom),
				ShaftVariant.Value(this.Top)
			});
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00004EC8 File Offset: 0x000030C8
		public bool Equals(ShaftVariant other)
		{
			return this.Up == other.Up && this.Down == other.Down && this.Left == other.Left && this.Right == other.Right && this.Bottom == other.Bottom && this.Top == other.Top;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00004F34 File Offset: 0x00003134
		public override bool Equals(object obj)
		{
			if (obj is ShaftVariant)
			{
				ShaftVariant other = (ShaftVariant)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00004F59 File Offset: 0x00003159
		public override int GetHashCode()
		{
			return HashCode.Combine<byte, byte, byte, byte, byte, byte>(this.Up, this.Down, this.Left, this.Right, this.Bottom, this.Top);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00004F84 File Offset: 0x00003184
		public static bool operator ==(ShaftVariant left, ShaftVariant right)
		{
			return left.Equals(right);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00004F8E File Offset: 0x0000318E
		public static bool operator !=(ShaftVariant left, ShaftVariant right)
		{
			return !left.Equals(right);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00004F9B File Offset: 0x0000319B
		public static string Value(byte state)
		{
			return state.ToString();
		}
	}
}
