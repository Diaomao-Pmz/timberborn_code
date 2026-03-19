using System;
using Timberborn.BlockSystem;
using Timberborn.Coordinates;

namespace Timberborn.BlockObjectTools
{
	// Token: 0x02000024 RID: 36
	public class PreviewPlacement
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x000037F3 File Offset: 0x000019F3
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x000037FB File Offset: 0x000019FB
		public Orientation Orientation { get; private set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00003804 File Offset: 0x00001A04
		public FlipMode FlipMode
		{
			get
			{
				if (!this._flippingEnabled)
				{
					return FlipMode.Unflipped;
				}
				return this._flipMode;
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000381A File Offset: 0x00001A1A
		public void RotateClockwise()
		{
			this.Orientation = this.Orientation.RotateClockwise();
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x0000382D File Offset: 0x00001A2D
		public void RotateCounterclockwise()
		{
			this.Orientation = this.Orientation.RotateCounterclockwise();
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003840 File Offset: 0x00001A40
		public void Flip()
		{
			this._flipMode = this.FlipMode.Flip();
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003861 File Offset: 0x00001A61
		public void EnableFlipping()
		{
			this._flippingEnabled = true;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x0000386A File Offset: 0x00001A6A
		public void DisableFlipping()
		{
			this._flippingEnabled = false;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003873 File Offset: 0x00001A73
		public void CopyFrom(BlockObject blockObject)
		{
			this.Orientation = blockObject.Orientation;
			this._flipMode = blockObject.FlipMode;
		}

		// Token: 0x0400006D RID: 109
		public FlipMode _flipMode;

		// Token: 0x0400006E RID: 110
		public bool _flippingEnabled;
	}
}
