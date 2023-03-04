using ElVegetarianoFurio.Data;
using ElVegetarianoFurio.Store;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace ElVegetarianoFurio {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddCors(options => {
                options.AddDefaultPolicy(
                    policy => {
                        policy.AllowAnyOrigin();
                    });
            });

            services.AddControllers();
            services.AddMvc();

            services.AddDbContext<ApplicationContext>(options => {
                options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]);
            });
            services.AddScoped<IDishRepository, EFDishRepository>();
            services.AddScoped<ICategoryRepository, EFCategoryRepository>();

            services.AddSwaggerGen(options => {
                options.SwaggerDoc("v0", new OpenApiInfo {
                    Version = "v0",
                    Title = "El Vegetariano Furio",
                    Description = "vegetarische spanische Gerichte",
                    Contact = new OpenApiContact {
                        Name = "Linus Englert",
                        Url = new Uri("https://chabbay.de"),
                        Email = "info@chabbay.de"
                    }
                });
                options.EnableAnnotations();

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
                options.SchemaFilter<SwaggerSchemaFilter>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationContext context) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            DbInitializer.Initialize(context, env);

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI(options => {
                options.SwaggerEndpoint("/swagger/v0/swagger.json", "My API V0");
            });

            app.UseCors(builder => {
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}