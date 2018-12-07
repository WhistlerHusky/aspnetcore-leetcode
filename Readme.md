# 1. Unable to configure HTTPS endpoint. No server certificate was specified
    - http://www.waynethompson.com.au/blog/dotnet-dev-certs-https/
    Steps to fix the problem
    1. Close your browsers so that they do not cache the certificate because that will cause other issues.
    2. On the commandline run
        dotnet dev-certs https --clean
    3. run
        dotnet dev-certs https -t

# 2. launchSettings.json 파일 손봐서 port 바꿀것 (난 22742 익숙한 포트로...)

# 3. nswag 12.04 버전인지 12버전인지 부터 아래 처럼 사용해야함

ConfigureServices 에는 아래 코드
```c#
services.AddSwaggerDocument();
```

Configure에는

```c#
app.UseSwagger(config => config.PostProcess = (document, request) =>
{
    if (request.Headers.ContainsKey("X-External-Host")) 
    {
        // Change document server settings to public
        document.Host = request.Headers["X-External-Host"].First();
        document.BasePath = request.Headers["X-External-Path"].First();
    }
});

app.UseSwaggerUi3(config => config.TransformToExternalPath = (internalUiRoute, request) =>
{
    // The header X-External-Path is set in the nginx.conf file
    var externalPath = request.Headers.ContainsKey("X-External-Path") ? request.Headers["X-External-Path"].First() : "";
    return externalPath + internalUiRoute;
});
```

# 4. swagger 시작페이지로 
먼저 HomeController 만들어주고
```c#
[Route("api/[controller]")]
[ApiController]
public class HomeController : ControllerBase
{
    public IActionResult Index()
    {
        return Redirect("/swagger");
    }
}
```
Routes에 등록
```c#
app.UseMvc(routes =>
{
    routes.MapRoute(
    name: "defaultWithArea",
    template: "{area}/{controller=Home}/{action=Index}/{id?}");
    routes.MapRoute(
    name: "default",
    template: "{controller=Home}/{action=Index}/{id?}");
});
```


# 5. NullInjectorError: No provider for HttpClient!
App.Module.ts 에 아래 코드 추가
```ts
import { HttpClientModule } from '@angular/common/http';
```

