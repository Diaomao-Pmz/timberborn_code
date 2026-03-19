using System;
using UnityEngine;

namespace Timberborn.ResourceCountingSystem
{
	// Token: 0x0200000A RID: 10
	public readonly struct ResourceCount
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002775 File Offset: 0x00000975
		public static ResourceCount Empty { get; } = ResourceCount.Create(0, 0, 0, 0, 0, 0, 0, 0);

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600002B RID: 43 RVA: 0x0000277C File Offset: 0x0000097C
		public int StockpiledStock { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002784 File Offset: 0x00000984
		public int BufferedOutputStock { get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002D RID: 45 RVA: 0x0000278C File Offset: 0x0000098C
		public int InputOutputCapacity { get; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002794 File Offset: 0x00000994
		public float FillRate { get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002F RID: 47 RVA: 0x0000279C File Offset: 0x0000099C
		public int CarriedToStockpilesStock { get; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000027A4 File Offset: 0x000009A4
		public int CarriedToProcessors { get; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000031 RID: 49 RVA: 0x000027AC File Offset: 0x000009AC
		public int StockUnderProcessing { get; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000027B4 File Offset: 0x000009B4
		public int BufferedInput { get; }

		// Token: 0x06000033 RID: 51 RVA: 0x000027BC File Offset: 0x000009BC
		public ResourceCount(int stockpiledStock, int bufferedOutputStock, int inputOutputCapacity, float fillRate, int outputCapacity, int carriedToStockpilesStock, int carriedToProcessors, int stockUnderProcessing, int bufferedInput)
		{
			this.StockpiledStock = stockpiledStock;
			this.BufferedOutputStock = bufferedOutputStock;
			this.InputOutputCapacity = inputOutputCapacity;
			this.FillRate = fillRate;
			this._outputCapacity = outputCapacity;
			this.CarriedToStockpilesStock = carriedToStockpilesStock;
			this.CarriedToProcessors = carriedToProcessors;
			this.StockUnderProcessing = stockUnderProcessing;
			this.BufferedInput = bufferedInput;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002810 File Offset: 0x00000A10
		public static ResourceCount Create(int inputOutputStock, int outputStock, int inputOutputCapacity, int outputCapacity, int availableCarriedStock, int reservedCarriedStock, int processedStock, int awaitingProcessStock)
		{
			return new ResourceCount(inputOutputStock, outputStock, inputOutputCapacity, ResourceCount.GetFillRate(inputOutputStock + outputStock + availableCarriedStock, inputOutputCapacity + outputCapacity), outputCapacity, availableCarriedStock, reservedCarriedStock, processedStock, awaitingProcessStock);
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000035 RID: 53 RVA: 0x0000283C File Offset: 0x00000A3C
		public int AvailableStock
		{
			get
			{
				return this.BufferedOutputStock + this.StockpiledStock + this.CarriedToStockpilesStock;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002852 File Offset: 0x00000A52
		public int AllStock
		{
			get
			{
				return this.AvailableStock + this.StockUnderProcessing + this.CarriedToProcessors + this.BufferedInput;
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002870 File Offset: 0x00000A70
		public static ResourceCount operator +(ResourceCount a, ResourceCount b)
		{
			return ResourceCount.Create(a.StockpiledStock + b.StockpiledStock, a.BufferedOutputStock + b.BufferedOutputStock, a.InputOutputCapacity + b.InputOutputCapacity, a._outputCapacity + b._outputCapacity, a.CarriedToStockpilesStock + b.CarriedToStockpilesStock, a.CarriedToProcessors + b.CarriedToProcessors, a.StockUnderProcessing + b.StockUnderProcessing, a.BufferedInput + b.BufferedInput);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000028F8 File Offset: 0x00000AF8
		public static float GetFillRate(int amount, int capacity)
		{
			if (amount == 0)
			{
				return 0f;
			}
			if (capacity == 0)
			{
				return 1f;
			}
			return Mathf.Clamp01((float)amount / (float)capacity);
		}

		// Token: 0x0400001F RID: 31
		public readonly int _outputCapacity;
	}
}
