using System;

namespace Timberborn.TemplateInstantiation
{
	// Token: 0x02000006 RID: 6
	public class DecoratorDefinition
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002133 File Offset: 0x00000333
		public Type DecoratorType { get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000C RID: 12 RVA: 0x0000213B File Offset: 0x0000033B
		public Action<object, object> Initializer { get; }

		// Token: 0x0600000D RID: 13 RVA: 0x00002143 File Offset: 0x00000343
		public DecoratorDefinition(Type decoratorType, Action<object, object> initializer)
		{
			this.DecoratorType = decoratorType;
			this.Initializer = initializer;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002159 File Offset: 0x00000359
		public static DecoratorDefinition CreateSingleton(Type decoratorType)
		{
			return new DecoratorDefinition(decoratorType, null);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002162 File Offset: 0x00000362
		public static DecoratorDefinition CreateDedicated(Type decoratorType, Action<object, object> initializer)
		{
			return new DecoratorDefinition(decoratorType, initializer);
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000010 RID: 16 RVA: 0x0000216B File Offset: 0x0000036B
		public bool Dedicated
		{
			get
			{
				return this.Initializer != null;
			}
		}
	}
}
