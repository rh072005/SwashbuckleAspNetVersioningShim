﻿using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SwashbuckleAspNetVersioningShim
{
    internal class CorrectOperationIdsOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.OperationId.ToLower().Contains("v{api-version"))
            {
                var operationId = context?.ApiDescription?.FriendlyId();
                if (operationId != null)
                {
                    operation.OperationId = operationId;
                }
            }
        }
    }
}
