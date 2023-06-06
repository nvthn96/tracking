using Autofac;

namespace tracking.DI
{
	public class DIContainer
	{
		private DIContainer() { }
		private static IContainer? instance;
		private static ContainerBuilder builder = new();

		public static void Add<Service>() where Service : class
		{
			builder.RegisterType<Service>();
		}

		public static void AddSingleton<Service>() where Service : class
		{
			builder.RegisterType<Service>().SingleInstance();
		}

		public static void Add<Interface, Service>() where Interface : class where Service : class
		{
			builder.RegisterType<Service>().As<Interface>();
		}

		public static void Build()
		{
			instance = builder.Build();
		}

		public static void Clear()
		{
			builder = new();
		}

		public static T Get<T>() where T : class
		{
#pragma warning disable CS8604 // Possible null reference argument.
			return instance.Resolve<T>();
#pragma warning restore CS8604 // Possible null reference argument.
		}
	}
}
