﻿using System;
using Grpc.Core;
using Grpc.Core.Interceptors;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Backend_Done.Authenticate
{
    public class LogerInterceptor
    {
        public class LoggerInterceptor : Interceptor
        {
            private readonly ILogger<LoggerInterceptor> _logger;

            public LoggerInterceptor(ILogger<LoggerInterceptor> logger)
            {
                _logger = logger;
            }

            public async override Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
                TRequest request,
                ServerCallContext context,
                UnaryServerMethod<TRequest, TResponse> continuation)
            {
                LogCall(context);
                try
                {
                    return await continuation(request, context);
                }
                catch (SqlException e)
                {
                    _logger.LogError(e, $"An SQL error occured when calling {context.Method}");
                    Status status;

                    if (e.Number == -2)
                    {
                        status = new Status(StatusCode.DeadlineExceeded, "SQL timeout");
                    }
                    else
                    {
                        status = new Status(StatusCode.Internal, "SQL error");
                    }
                    throw new RpcException(status);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, $"An error occured when calling {context.Method}");
                    throw new RpcException(Status.DefaultCancelled, e.Message);
                }

            }

            private void LogCall(ServerCallContext context)
            {
                var httpContext = context.GetHttpContext();
                _logger.LogDebug($"Starting call. Request: {httpContext.Request.Path}");
            }
        }
    }
}
