﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SwaggerTestAPI2.Extensions;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SwaggerTestAPI2.Filters
{
    public class ApiVersionOperationFilter:IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            var actionApiVersionModel = context.ApiDescription.ActionDescriptor?.GetApiVersion();
            if (actionApiVersionModel == null)
            {
                return;
            }

            if (actionApiVersionModel.DeclaredApiVersions.Any())
            {
                operation.Produces = operation.Produces
                    .SelectMany(p => actionApiVersionModel.DeclaredApiVersions
                            .Select(version => $"{p};b={version.ToString()}")
                        )
                    .ToList();
            }
            else
            {
                operation.Produces = operation.Produces
                    .SelectMany(p => actionApiVersionModel.ImplementedApiVersions.OrderByDescending(v => v)
                        .Select(version=>$"{p};v={version.ToString()}"))
                    .ToList();
            }
        }
    }
}