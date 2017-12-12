namespace FeedDevs

open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.SpaServices.Webpack
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection

type Route = { 
        controller : string
        action : string }

type Startup private () =
    new (configuration: IConfiguration) as this =
        Startup() then
        this.Configuration <- configuration

    // This method gets called by the runtime. Use this method to add services to the container.
    member this.ConfigureServices(services: IServiceCollection) =
        // Add framework services.
        services.AddMvc() |> ignore

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    member this.Configure(app: IApplicationBuilder, env: IHostingEnvironment) =

        if (env.IsDevelopment()) then
            
            app.UseDeveloperExceptionPage() |> ignore
            app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions(HotModuleReplacement = true, ReactHotModuleReplacement = true))
        else
            app.UseExceptionHandler("/Home/Error") |> ignore

        app.UseStaticFiles() |> ignore

        app.UseMvc(fun routes ->
            routes.MapRoute(
                name = "default",
                template = "{controller=Home}/{action=Index}/{id?}") |> ignore
                        
            routes.MapSpaFallbackRoute(name = "spa-fallback", defaults = { controller = "Home"; action = "Index" } )
        ) |> ignore

    member val Configuration : IConfiguration = null with get, set
