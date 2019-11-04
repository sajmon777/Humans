using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Humans.API.Infrastructure
{
	public class AutofacModuleAPI : Autofac.Module
	{
		public AutofacModuleAPI(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		


		protected override void Load(ContainerBuilder builder)
		{
			//SqlClientFactory
			builder.Register(c => new SqlConnection(Configuration.GetSection("ConnectionString").Value)).As<IDbConnection>().InstancePerLifetimeScope();
		}
	}
}
