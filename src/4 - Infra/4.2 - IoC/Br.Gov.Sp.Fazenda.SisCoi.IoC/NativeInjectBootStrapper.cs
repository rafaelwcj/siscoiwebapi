using Br.Gov.Sp.Fazenda.SisCoi.Bus;
using Br.Gov.Sp.Fazenda.SisCoi.Data.Repositories;
using Br.Gov.Sp.Fazenda.SisCoi.Domain.Core.Bus;
using Br.Gov.Sp.Fazenda.SisCoi.Domain.Core.Notifications;
using Br.Gov.Sp.Fazenda.SisCoi.Domain.Interfaces.Repositories;
using Br.Gov.Sp.Fazenda.SisCoi.Domain.Interfaces.Services;
using Br.Gov.Sp.Fazenda.SisCoi.Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Br.Gov.Sp.Fazenda.SisCoi.IoC
{
    public class NativeInjectBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //Domain - Mediator
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            //Domain - Events
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            // Infra - Data
            services.AddScoped<ISolicitacaoBkpService, SolicitacaoBkpService>();
            services.AddScoped<ISolicitacaoBkpRepository, SolicitacaoBkpRepository>();
            services.AddScoped<ICmdbServicesService, CmdbServicesService>();
            services.AddScoped<ICmdbServicesRepository, CmdbServicesRepository>();
            services.AddScoped<IIncidentesService, IncidentesService>();
            services.AddScoped<IIncidentesRepository, IncidentesRepository>();
        }

        
    }
}
