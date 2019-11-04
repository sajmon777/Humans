using Autofac;
using Humans.Business.Repository;
using Humans.Business.RepositoryInterfaces;
using Humans.Business.Service;
using Humans.Business.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Humans.Business.Infrastructure
{
	public class AutofacModuleBL : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			// Repository
			builder.RegisterType<CityRepository>().As<ICityRepository>();
			builder.RegisterType<PersonRepository>().As<IPersonRepository>();

			// Service
			builder.RegisterType<CityService>().As<ICityService>();
			builder.RegisterType<PersonService>().As<IPersonService>();
		}
	}
}
