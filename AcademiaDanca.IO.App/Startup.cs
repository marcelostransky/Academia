﻿using AcademiaDanca.IO.App.Filtros;
using AcademiaDanca.IO.App.Models;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.FinanceiroComando.Entrada.Com_Matricula;
using AcademiaDanca.IO.Dominio.Contexto.Manipuladores;
using AcademiaDanca.IO.Dominio.Contexto.Manipuladores.Acesso;
using AcademiaDanca.IO.Dominio.Contexto.Manipuladores.Aluno;
using AcademiaDanca.IO.Dominio.Contexto.Manipuladores.Configuracao;
using AcademiaDanca.IO.Dominio.Contexto.Manipuladores.DadosExternos;
using AcademiaDanca.IO.Dominio.Contexto.Manipuladores.Financeiro;
using AcademiaDanca.IO.Dominio.Contexto.Manipuladores.Financeiro.FIN_Matricula;
using AcademiaDanca.IO.Dominio.Contexto.Manipuladores.Financeiro.Fin_Mensalidade;
using AcademiaDanca.IO.Dominio.Contexto.Manipuladores.TurmaContexto;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using AcademiaDanca.IO.Infra;
using AcademiaDanca.IO.Infra.Repositorio;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AcademiaDanca.IO.App
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                   .AddCookie(options =>
                   {
                       options.LoginPath = "/Login/Autenticar";
                       options.CookieName = "Academia";

                   });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddScoped<AcademiaContexto, AcademiaContexto>();
            services.AddTransient<ITipoTelefoneRepositorio, TipoTelefoneRepositorio>();
            services.AddTransient<IFinanceiroRepositorio, FinanceiroRepositorio>();
            services.AddTransient<ILoginRepositorio, LoginRepositorio>();
            services.AddTransient<IPerfilRepositorio, PerfilRepositorio>();
            services.AddTransient<ITipoFiliacaoRepositorio, TipoFiliacaoRepositorio>();
            services.AddTransient<ITurmaRepositorio, TurmaRepositorio>();
            services.AddTransient<IAgendaRepositorio, AgendaRepositorio>();
            services.AddTransient<ITurmaTipoRepositorio, TurmaTipoRepositorio>();
            services.AddTransient<ISalaRepositorio, SalaRepositorio>();
            services.AddTransient<IAlunoRepositorio, AlunoRepositorio>();
            services.AddTransient<IFuncionarioRepositorio, FuncionarioRepositorio>();
            services.AddTransient<IEnderecoRepositorio, EnderecoRepositorio>();
            services.AddTransient<IDashBoardRepositorio, DashBoardRepositorio>();
            services.AddTransient<IAcessoRepositorio, AcessoRepositorio>();
            services.AddTransient<IConfiguracaoRepositorio, ConfiguracaoRepositorio>();
            services.AddTransient<IMatriculaRepositorio, MatriculaRepositorio>();
            services.AddTransient<IMensalidadeRepositorio, MensalidadeRepositorio>();
            services.AddTransient<IRelatorioRepositorio, RelatorioRepositorio>();
            services.AddTransient<IParametroRepositorio, ParametroRepositorio>();
            services.AddTransient<TipoTelefoneManipulador, TipoTelefoneManipulador>();
            services.AddTransient<ItemMatriculaManipulador, ItemMatriculaManipulador>();
            services.AddTransient<TipoFiliacaoManipulador, TipoFiliacaoManipulador>();
            services.AddTransient<AlunoManipulador, AlunoManipulador>();
            services.AddTransient<CriarFuncionarioManipulador, CriarFuncionarioManipulador>();
            services.AddTransient<CriarTurmaManipulador, CriarTurmaManipulador>();
            services.AddTransient<EditarFuncionarioManipulador, EditarFuncionarioManipulador>();
            services.AddTransient<EditarFotoFuncionarioManipulador, EditarFotoFuncionarioManipulador>();
            services.AddTransient<EditarAcessoFuncionarioManipulador, EditarAcessoFuncionarioManipulador>();
            services.AddTransient<AgendarTurmaManipulador, AgendarTurmaManipulador>();
            services.AddTransient<AddEnderecoManipulador, AddEnderecoManipulador>();
            services.AddTransient<DeletarAgendaTurmaManipulador, DeletarAgendaTurmaManipulador>();
            services.AddTransient<DelTurmaAlunoManipulador, DelTurmaAlunoManipulador>();
            services.AddTransient<EditarFotoAlunoManipulador, EditarFotoAlunoManipulador>();
            services.AddTransient<AddResponsavelManipulador, AddResponsavelManipulador>();
            services.AddTransient<AddTurmaAlunoManipulador, AddTurmaAlunoManipulador>();
            services.AddTransient<DelPermissaoManipulador, DelPermissaoManipulador>();
            services.AddTransient<MatricularManipulador, MatricularManipulador>();
            services.AddTransient<RegistrarPagamentoMensalidadeManipulador, RegistrarPagamentoMensalidadeManipulador>();
            services.AddTransient<AddPaginaManipulador, AddPaginaManipulador>();
            services.AddTransient<AddPerfilManipulador, AddPerfilManipulador>();
            services.AddTransient<AddPermissaoManipulador, AddPermissaoManipulador>();
            services.AddTransient<EditarPerfilManipulador, EditarPerfilManipulador>();
            services.AddTransient<DelPaginaManipulador, DelPaginaManipulador>();
            services.AddTransient<EditarPaginaManipulador, EditarPaginaManipulador>();
            services.AddTransient<DelPerfilManipulador, DelPerfilManipulador>();
            services.AddTransient<EditarPermissaoManipulador, EditarPermissaoManipulador>();
            services.AddTransient<EditarTurmaManipulador, EditarTurmaManipulador>();
            services.AddTransient<DeletarTurmaManipulador, DeletarTurmaManipulador>();
            services.AddTransient<EditarAlunoManipulador, EditarAlunoManipulador>();
            services.AddTransient<DelMatriculaItemManipulador, DelMatriculaItemManipulador>();
            services.AddTransient<DadosExternosManipulador, DadosExternosManipulador>();
            services.AddTransient<EditMatriculaManipulador, EditMatriculaManipulador>();
            services.AddTransient<EstornarMensalidadeManipulador, EstornarMensalidadeManipulador>();
            services.AddTransient<EditarParametroManipulador, EditarParametroManipulador>();
            

            services.AddScoped<RegrasAcessoModel>();

            //Settings.ConnectionString = $"{Configuration["connectionString"]}";
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseDeveloperExceptionPage();
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                 name: "areas",
                 template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
               );

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });


        }
    }
}
