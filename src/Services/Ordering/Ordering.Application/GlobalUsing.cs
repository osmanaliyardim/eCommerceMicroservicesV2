﻿global using eCommerceMicroservicesV2.BuildingBlocks.CQRS;
global using eCommerceMicroservicesV2.Ordering.Application.Dtos;
global using eCommerceMicroservicesV2.Ordering.Domain.Models;
global using FluentValidation;
global using eCommerceMicroservicesV2.Ordering.Application.Data;
global using eCommerceMicroservicesV2.Ordering.Domain.ValueObjects;
global using Microsoft.EntityFrameworkCore;
global using eCommerceMicroservicesV2.BuildingBlocks.Exceptions;
global using eCommerceMicroservicesV2.Ordering.Application.Exceptions;
global using MediatR;
global using eCommerceMicroservicesV2.Ordering.Domain.Events;
global using Microsoft.Extensions.Logging;
global using eCommerceMicroservicesV2.BuildingBlocks.Pagination;
global using eCommerceMicroservicesV2.BuildingBlocks.Constants;