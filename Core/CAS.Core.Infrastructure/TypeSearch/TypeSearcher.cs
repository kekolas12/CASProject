using System.Diagnostics;
using System.Reflection;

namespace CAS.Core.Infrastructure.TypeSearch
{
	public class TypeSearcher : ITypeSearcher
	{
		public IEnumerable<Type> ClassesOfType<T>(bool onlyConcreteClasses = true)
		{
			return ClassesOfType(typeof(T), onlyConcreteClasses);
		}

		public IEnumerable<Type> ClassesOfType(Type assignTypeFrom, bool onlyConcreteClasses = true)
		{
			return ClassesOfType(assignTypeFrom, GetAssemblies(), onlyConcreteClasses);
		}

		public IEnumerable<Type> ClassesOfType(Type assignTypeFrom, IEnumerable<Assembly> assemblies,
			bool onlyConcreteClasses = true)
		{
			var result = new List<Type>();
			try
			{
				foreach (var a in assemblies)
				{
					Type[] types = a.GetTypes();
					foreach (var t in types)
					{
						if (!assignTypeFrom.IsAssignableFrom(t) && (!assignTypeFrom.IsGenericTypeDefinition ||
																	!DoesTypeImplementOpenGeneric(t, assignTypeFrom)))
							continue;

						if (t.IsInterface)
							continue;

						if (onlyConcreteClasses)
						{
							if (t.IsClass && !t.IsAbstract)
							{
								result.Add(t);
							}
						}
						else
						{
							result.Add(t);
						}
					}
				}
			}
			catch (ReflectionTypeLoadException ex)
			{
				var msg = ex.LoaderExceptions.Aggregate(string.Empty,
					(current, e) => current + e!.Message + Environment.NewLine);

				var fail = new Exception(msg, ex);
				Debug.WriteLine(fail.Message, fail);

				throw fail;
			}

			return result;
		}
		private static bool DoesTypeImplementOpenGeneric(Type type, Type openGeneric)
		{
			try
			{
				var genericTypeDefinition = openGeneric.GetGenericTypeDefinition();
				return (from implementedInterface in type.FindInterfaces((_, _) => true, null)
						where implementedInterface.IsGenericType
						select genericTypeDefinition.IsAssignableFrom(implementedInterface.GetGenericTypeDefinition()))
					.FirstOrDefault();
			}
			catch
			{
				return false;
			}
		}
		public virtual IList<Assembly> GetAssemblies()
		{
			return AssembliesInAppDomain();
		}
        private static IList<Assembly> AssembliesInAppDomain()
        {
            var addedAssemblyNames = new List<string>();
            var assemblies = new List<Assembly>();
            var currentAssem = Assembly.GetExecutingAssembly();
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var product = assembly.GetCustomAttribute<AssemblyProductAttribute>();
                var referencedAssemblies = assembly.GetReferencedAssemblies().ToList();
                if (referencedAssemblies.All(x => x.FullName != currentAssem.FullName) &&
                    product?.Product != "CAS") continue;
                if (addedAssemblyNames.Contains(assembly.FullName)) continue;
                assemblies.Add(assembly);
                addedAssemblyNames.Add(assembly.FullName);
            }
            return assemblies;
        }
    }
}
