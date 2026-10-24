## Place your Assembly DLL plugin here

### How to create a plugin

---

Create a new `Class Libary (.NET Framework)` project.

> _You may remove the default Class1.cs file._

Add following Nuget packages:

    Install-Package Newtonsoft.Json
    Install-Package Microsoft.AspNet.WebApi

- Right-click your new Class project and add New Folder named "Controllers".
- Within the new "Controllers" folder, add a new Class.
  - **Be should name has suffix Controller. like "CalculationController.cs"**
- With new Class file open, include the namespace to the top of file
  - `Using System.Web.Http;`
- Inherit the ApiController and add route prefix

```c#
[RoutePrefix("api/calculation")]
public class CalculationController : ApiController
```

- Create a Route Action like so;

```c#
[AllowAnonymous]
[Route("sqrt")]
[HttpPost]
public IHttpActionResult SquareRoot_POST(long value = 0)
{
    double result = Math.Sqrt(value);
    return Ok(result);
}
```

### Build and copy into the Plugins directory

> NOTE: may also include the xml documentation in the plugin directory
