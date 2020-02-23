## `IApplicationBuilderExtensions`

> Package: `Extensions.AspNetCore`

Extensions for `Microsoft.AspNetCore.Builder.IApplicationBuilder`
```csharp
public static class Microsoft.AspNetCore.Builder.IApplicationBuilderExtensions

```

Static Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `IApplicationBuilder` | UseStandardHeaders(this `IApplicationBuilder` app) | Add standard security headers to all HTTP responses | 
| `IApplicationBuilder` | UseStandardMetrics(this `IApplicationBuilder` app) | Add a /metrics endpoint publishing standard app metrics in Prometheus format | 
| `IApplicationBuilder` | UseStandardPipeline(this `IApplicationBuilder` app, `Boolean` isDevelopment) | Configure a standard ASP.NET Core pipeline, with security headers, metrics, error page and static files | 


