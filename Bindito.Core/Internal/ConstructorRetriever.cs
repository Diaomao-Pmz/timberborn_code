using System;
using System.Collections.Generic;
using System.Reflection;

namespace Bindito.Core.Internal
{
	// Token: 0x02000087 RID: 135
	public class ConstructorRetriever : IConstructorRetriever
	{
		// Token: 0x06000143 RID: 323 RVA: 0x00003550 File Offset: 0x00001750
		public ConstructorInfo GetEligibleConstructor(Type type)
		{
			ConstructorInfo result;
			if (this._cachedConstructors.TryGetValue(type, out result))
			{
				return result;
			}
			ConstructorInfo constructorInfo;
			ConstructorInfo constructorInfo2;
			ConstructorRetriever.GetConstructors(type, out constructorInfo, out constructorInfo2);
			if (constructorInfo != null && constructorInfo2 != null)
			{
				return null;
			}
			ConstructorInfo constructorInfo3 = constructorInfo ?? constructorInfo2;
			this._cachedConstructors.Add(type, constructorInfo3);
			return constructorInfo3;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x000035A4 File Offset: 0x000017A4
		private static void GetConstructors(Type type, out ConstructorInfo parameterlessConstructor, out ConstructorInfo singleParameterfulConstructor)
		{
			singleParameterfulConstructor = null;
			parameterlessConstructor = null;
			foreach (ConstructorInfo constructorInfo in type.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
			{
				if (constructorInfo.GetParameters().Length == 0)
				{
					parameterlessConstructor = constructorInfo;
				}
				else
				{
					if (!(singleParameterfulConstructor == null))
					{
						throw new BinditoException(TypeFormatting.Format(type) + " has more than one parameterful constructors.");
					}
					singleParameterfulConstructor = constructorInfo;
				}
			}
		}

		// Token: 0x04000085 RID: 133
		private readonly Dictionary<Type, ConstructorInfo> _cachedConstructors = new Dictionary<Type, ConstructorInfo>();
	}
}
