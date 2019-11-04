using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Humans.API.Infrastructure;
using Humans.Business.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Humans.API
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }
		public IContainer ApplicationContainer { get; private set; }


		// This method gets called by the runtime. Use this method to add services to the container.
		public IServiceProvider ConfigureServices(IServiceCollection services)
		{
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

			//services.Configure<ConnectionStrings>(Configuration.GetSection("ConnectionString"));

			var containerBuilder = new ContainerBuilder();
			containerBuilder.RegisterModule(new AutofacModuleBL());
			containerBuilder.RegisterModule(new AutofacModuleAPI(Configuration));
			//containerBuilder.Populate(services);
			ApplicationContainer = containerBuilder.Build();
			return new AutofacServiceProvider(ApplicationContainer);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseMvc();
		}
	}
}
